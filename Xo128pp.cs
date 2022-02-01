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

namespace XoRand.X32
{
    public class Xo128pp : XoBase
    {
        private const int IMAX = int.MaxValue;

        /// <summary>
        /// Create with no inital state.
        /// TODO: Generate state.
        /// </summary>
        public Xo128pp()
            : this((ulong)Environment.TickCount)
        {
        }

        /// <summary>
        /// Create and specify inital state.
        /// </summary>
        /// <param name="InitSeed"></param>
        public Xo128pp(ulong InitSeed)
        {
            //ulong[] s = new ulong[4];
            // SplitMix64 sm64 = new SplitMix64(InitSeed);
            // sm64.FillArray64(State);
        }

        /// <summary>
        /// Returns the next whole number. xoro128++
        /// </summary>
        /// <returns>Random integer</returns>
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
            int range = maxValue - minValue;

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
            //double r = (Xoro64Next() >> 11) * (1.0 / ((ulong)1 << 53));
            //return r;
            return Xoro32Next() * (1.0 / IMAX);
            //return (XoroNext() & ((1UL << 53) - 1)) * (1.00 / (1UL << 53));
        }
        public override double NextDouble()
        {
            return Sample();
            //return (XoroNext() & ((1L << 53) - 1)) * (1.00 / (1L << 53));
        }
        //public override ulong NextuLong() => Xoro64Next();
    }
}