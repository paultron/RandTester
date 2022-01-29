/*  Written in 2019 by David Blackman and Sebastiano Vigna (vigna@acm.org)

To the extent possible under law, the author has dedicated all copyright
and related and neighboring rights to this software to the public domain
worldwide. This software is distributed without any warranty.

See <http://creativecommons.org/publicdomain/zero/1.0/>. */

/* This is xoshiro256++ 1.0, one of our all-purpose, rock-solid generators.
   It has excellent (sub-ns) speed, a state (256 bits) that is large
   enough for any parallel application, and it passes all tests we are
   aware of.

   For generating just floating-point numbers, xoshiro256+ is even faster.

   The state must be seeded so that it is not everywhere zero. If you have
   a 64-bit seed, we suggest to seed a splitmix64 generator and use its
   output to fill s. */

using System;

namespace XoRand
{
    public class XoRand256 : Random
    {
        public uint[] StateX32 = new uint[4];
        public ulong[] StateX64 = new ulong[4];

        private const ulong UBIG = ulong.MaxValue;

        private static readonly ulong[] SHORTJ64 = { 0x180EC6D33CFD0ABA, 0xd5a61266f0c9392c, 0xa9582618e03fc9aa, 0x39abdc4529b1661c };
        private static readonly ulong[] LONGJ64 = { 0x76e15d3efefdcbbf, 0xc5004e441c522fb3, 0x77710069854ee241, 0x39109bb02acbe635 };

        /// <summary>
        /// Create with no inital state.
        /// TODO: Generate state.
        /// </summary>
        public XoRand256()
            : this((ulong)Environment.TickCount)
        {
        }

        /// <summary>
        /// Create and specify inital state.
        /// </summary>
        /// <param name="InitSeed"></param>
        public XoRand256(ulong InitSeed)
        {
            //ulong[] s = new ulong[4];
            // SplitMix64 sm64 = new SplitMix64(InitSeed);
            // sm64.FillArray64(State);
        }

        /// <summary>
        /// Returns the next whole number.
        /// </summary>
        /// <returns>Random integer</returns>
        public ulong Xoro64Next()
        {
            ulong Result = Rotl64(StateX64[0] + StateX64[3], 23) + StateX64[0];
            
            ulong t = StateX64[1] << 17;

            StateX64[2] ^= StateX64[0];
            StateX64[3] ^= StateX64[1];
            StateX64[1] ^= StateX64[2];
            StateX64[0] ^= StateX64[3];

            StateX64[2] ^= t;

            StateX64[3] = Rotl64(StateX64[3], 45);

            return Result;
        }
        public uint Xoro32Next()
        {
            uint Result = Rotl32(StateX32[0] + StateX32[3], 7) + StateX32[0];

            uint t = StateX32[1] << 9;

            StateX32[2] ^= StateX32[0];
            StateX32[3] ^= StateX32[1];
            StateX32[1] ^= StateX32[2];
            StateX32[0] ^= StateX32[3];

            StateX32[2] ^= t;

            StateX32[3] = Rotl32(StateX32[3], 11);

            return Result;
        }
        public override int Next()
        {
            return (int)(Xoro32Next() % int.MaxValue);
        }
        public override int Next(int minValue, int maxValue)
        {
            int range = (int)(maxValue - minValue);

            return (int)(Xoro32Next() * range) + minValue;
        }
        public override void NextBytes(byte[] buffer)
        {
            //if (buffer == null) throw new ArgumentNullException("buffer");
            //Contract.EndContractBlock();
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = (byte)(Next() % (byte.MaxValue + 1));
            }
        }
        protected override double Sample()
        {
            double r = (Xoro64Next() >> 11) * (1.0 / ((ulong)1 << 53));
            return r;
            //return XoroNext() * (1.0 / MBIG);
            //return (XoroNext() & ((1UL << 53) - 1)) * (1.00 / (1UL << 53));
        }
        public override double NextDouble()
        {
            return Sample();
            //return (XoroNext() & ((1L << 53) - 1)) * (1.00 / (1L << 53));
        }
        public ulong NextLong() => Xoro64Next();

        /// <summary>
        ///This is the jump function for the generator. It is equivalent
        ///to 2^128 calls to Next(); it can be used to generate 2^128
        ///non-overlapping subsequences for parallel computations.
        ///	</summary>
        public void Jump64(bool Long)
        {
            _ = new ulong[4];
            ulong[] JUMP = Long ? SHORTJ64 : LONGJ64;

            ulong s0 = 0, s1 = 0, s2 = 0, s3 = 0;

            for (int i = 0; i < 4; i++)
            {
                for (int b = 0; b < 64; b++)
                {
                    if ((JUMP[i] & 1UL << b) > 0)
                    {
                        s0 ^= StateX64[0];
                        s1 ^= StateX64[1];
                        s2 ^= StateX64[2];
                        s3 ^= StateX64[3];
                    }
                    _ = Xoro64Next();
                }

                StateX64[0] = s0;
                StateX64[1] = s1;
                StateX64[2] = s2;
                StateX64[3] = s3;
            }
        }

        public static ulong Rotl64(in ulong x, int k) => (x << k) | (x >> (64 - k));
        public static ulong Rotr64(in ulong x, int k) => (x >> k) | (x << (64 - k));
        public static uint Rotl32(in uint x, int k) => (x << k) | (x >> (32 - k));
        public static uint Rotr32(in uint x, int k) => (x >> k) | (x << (32 - k));

        public static ulong XorR64(in ulong Input, int Amt) => Input ^ (Input >> Amt);

        public static ulong SplitMix64(ulong Input)
        {
            Input += 0x9E3779B97f4A7C15;

            Input = XorR64(Input, 30) * 0xBF58476D1CE4E5B9;
            Input = XorR64(Input, 27) * 0x94D049BB133111EB;
            Input = XorR64(Input, 31);

            return Input;
        }

        public static ulong MurmurHash64(ulong Input)
        {
            Input = XorR64(Input, 33) * 0xff51afd7ed558ccd;
            Input = XorR64(Input, 33) * 0xc4ceb9fe1a85ec53;
            Input = XorR64(Input, 33);

            return Input;
        }

        public static ulong RrMxMx64(ulong Input)
        {
            Input ^= Rotr64(Input, 49) ^ Rotr64(Input, 24);

            // Original Version:
            //Output *= 0x9FB21C651E98DF25;
            //Output = XorR64(Output, 28);
            //Output *= 0x9FB21C651E98DF25;
            //Output = XorR64(Output, 28);

            // V2: Possible for loop
            //Output = XorR64(Output * 0x9FB21C651E98DF25, 28);
            //Output = XorR64(Output * 0x9FB21C651E98DF25, 28);

            // V3: 
            Input = XorR64(XorR64(Input * 0x9FB21C651E98DF25, 28) * 0x9FB21C651E98DF25, 28);

            return Input;
        }

        public static void MixAllSM64(ulong[] State)
        {
            for (int i = 0; i < State.Length; i++)
            {
                State[i] = SplitMix64(State[i]);
            }
        }


    }
}