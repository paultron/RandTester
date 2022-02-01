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
using XoRand.Base;

namespace XoRand.X64
{
    public class Xo256pp : XoBase
    {
        private const ulong UMAX = ulong.MaxValue;

        /// <summary>
        /// Create with no inital state.
        /// TODO: Generate state.
        /// </summary>
        public Xo256pp()
            : this((ulong)Environment.TickCount)
        {
        }

        /// <summary>
        /// Create and specify inital state.
        /// </summary>
        /// <param name="InitSeed"></param>
        public Xo256pp(ulong InitSeed)
        {
            //ulong[] s = new ulong[4];
            // SplitMix64 sm64 = new SplitMix64(InitSeed);
            // sm64.FillArray64(State);
        }

        /// <summary>
        /// Returns the next whole number. xoro256++
        /// </summary>
        /// <returns>Random integer</returns>
        public ulong Xoro64Next()
        {
            // 256++
            ulong Result = Rotl64(StateX64[0] + StateX64[3], 23) + StateX64[0];
            // 256**
            //ulong Result = Rotl64(StateX64[1] * 5, 7) * 9;

            ulong t = StateX64[1] << 17;

            StateX64[2] ^= StateX64[0];
            StateX64[3] ^= StateX64[1];
            StateX64[1] ^= StateX64[2];
            StateX64[0] ^= StateX64[3];

            StateX64[2] ^= t;

            StateX64[3] = Rotl64(StateX64[3], 45);

            return Result;
        }
        public override int Next()
        {
            return (int)(Sample() * int.MaxValue);
        }
        public override int Next(int minValue, int maxValue)
        {
            int range = maxValue - minValue;

            return (int)(Next() * range) + minValue;
        }
        public override void NextBytes(byte[] buffer)
        {
            if (buffer == null) throw new ArgumentNullException("buffer");
            //Contract.EndContractBlock();
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = (byte)(Next() % (byte.MaxValue + 1));
            }
        }
        protected override double Sample()
        {
            return Xoro64Next() * (1.0 / UMAX);
        }
        public double SampleShifted()
        {
            return (Xoro64Next() >> 11) * (1.0 / ((ulong)1 << 53));
            //return (XoroNext() & ((1UL << 53) - 1)) * (1.00 / (1UL << 53));
        }
        public override double NextDouble()
        {
            return Sample();
            //return (XoroNext() & ((1L << 53) - 1)) * (1.00 / (1L << 53));
        }
        public ulong NextuLong() => Xoro64Next();
    }
}