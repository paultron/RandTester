/*  Written in 2019 by David Blackman and Sebastiano Vigna (vigna@acm.org)

To the extent possible under law, the author has dedicated all copyright
and related and neighboring rights to this software to the public domain
worldwide. This software is distributed without any warranty.

See <http://creativecommons.org/publicdomain/zero/1.0/>. */

using System.Runtime.CompilerServices;
using System;

namespace XoRand.Base
{
    public abstract class XoBase : Random
    {
        // Base states and constant variables
        public uint[] StateX32 = new uint[4];
        public ulong[] StateX64 = new ulong[4];

        private static readonly ulong[] SHORTJ64 = {
            0x180EC6D33CFD0ABA, 0xd5a61266f0c9392c, 0xa9582618e03fc9aa, 0x39abdc4529b1661c };
        private static readonly ulong[] LONGJ64 = {
            0x76e15d3efefdcbbf, 0xc5004e441c522fb3, 0x77710069854ee241, 0x39109bb02acbe635 };

        private static readonly uint[] SHORTJ32 = {
            0x8764000b, 0xf542d2d3, 0x6fa035c3, 0x77f2db5b };
        private static readonly uint[] LONGJ32 = {
            0xb523952e, 0x0b6f099f, 0xccf5a0ef, 0x1c580662 };

        // Abstract methods
        public abstract override int Next();
        public abstract override int Next(int minValue, int maxValue);
        public abstract override void NextBytes(byte[] buffer);
        public abstract override double NextDouble();
        //public virtual ulong NextuLong();
        protected abstract override double Sample();

        // Base mixing methods. Maybe move to classes

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
        public static ulong SplitMix64(ulong Input)
        {
            Input += 0x9E3779B97f4A7C15;

            Input = XorR64(Input, 30) * 0xBF58476D1CE4E5B9;
            Input = XorR64(Input, 27) * 0x94D049BB133111EB;
            Input = XorR64(Input, 31);

            return Input;
        }

        public static void MixAllSM64(ulong[] State)
        {
            for (int i = 0; i < State.Length; i++)
            {
                State[i] = SplitMix64(State[i]);
            }
        }

        // Bit shifting and XOR methods
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Rotl32(in uint x, int k) => (x << k) | (x >> (32 - k));
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong Rotl64(in ulong x, int k) => (x << k) | (x >> (64 - k));
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Rotr32(in uint x, int k) => (x >> k) | (x << (32 - k));
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong Rotr64(in ulong x, int k) => (x >> k) | (x << (64 - k));
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong XorR64(in ulong Input, int Amt) => Input ^ (Input >> Amt);

        /// <summary>
        ///2^128 calls to <c>Next()</c>.  it can be used to generate 2^128
        ///non-overlapping subsequences for parallel computations.
        ///	</summary><param name="Long"><c>True</c> if long jump.</param>
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
                    _ = Next();
                }

                StateX64[0] = s0;
                StateX64[1] = s1;
                StateX64[2] = s2;
                StateX64[3] = s3;
            }
        }
        public void Jump32(bool Long)
        {
            _ = new ulong[4];
            uint[] JUMP = Long ? SHORTJ32 : LONGJ32;

            uint s0 = 0, s1 = 0, s2 = 0, s3 = 0;

            for (int i = 0; i < 4; i++)
            {
                for (int b = 0; b < 32; b++)
                {
                    if ((JUMP[i] & 1UL << b) > 0)
                    {
                        s0 ^= StateX32[0];
                        s1 ^= StateX32[1];
                        s2 ^= StateX32[2];
                        s3 ^= StateX32[3];
                    }
                    _ = Next();
                }

                StateX32[0] = s0;
                StateX32[1] = s1;
                StateX32[2] = s2;
                StateX32[3] = s3;
            }
        }
    }
}