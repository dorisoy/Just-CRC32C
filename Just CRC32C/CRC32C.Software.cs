﻿namespace JustCRC32C;

using System.Runtime.CompilerServices;

public static partial class Crc32C
{
    /**
     * Taken from https://github.com/force-net/Crc32.NET/blob/26c5a818a5c7a3d6a622c92d3cd08dba586c263c/Crc32.NET/SafeProxy.cs#L38
     * Crc32.NET - Released under MIT license
     * Optimised loop by replacing it with a switch statement
     */
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [SkipLocalsInit]
    internal static uint CalculateSoftware(Span<byte> input)
    {
        int offset = 0;
        int length = input.Length;
        uint crc = uint.MaxValue ^ 0;

        uint[] table = Table;
        while (length >= 16)
        {
            unchecked
            {
                uint a = table[(3 * 256) + input[offset + 12]]
                       ^ table[(2 * 256) + input[offset + 13]]
                       ^ table[(1 * 256) + input[offset + 14]]
                       ^ table[(0 * 256) + input[offset + 15]];

                uint b = table[(7 * 256) + input[offset + 8]]
                       ^ table[(6 * 256) + input[offset + 9]]
                       ^ table[(5 * 256) + input[offset + 10]]
                       ^ table[(4 * 256) + input[offset + 11]];

                uint c = table[(11 * 256) + input[offset + 4]]
                       ^ table[(10 * 256) + input[offset + 5]]
                       ^ table[(9 * 256) + input[offset + 6]]
                       ^ table[(8 * 256) + input[offset + 7]];

                uint d = table[(15 * 256) + ((byte)crc ^ input[offset])]
                       ^ table[(14 * 256) + ((byte)(crc >> 8) ^ input[offset + 1])]
                       ^ table[(13 * 256) + ((byte)(crc >> 16) ^ input[offset + 2])]
                       ^ table[(12 * 256) + ((crc >> 24) ^ input[offset + 3])];
                crc = d ^ c ^ b ^ a;
                offset += 16;
                length -= 16;
            }
        }

        switch (length)
        {
            case 15: crc = table[(byte)(crc ^ input[offset])] ^ crc >> 8; ++offset; goto case 14;
            case 14: crc = table[(byte)(crc ^ input[offset])] ^ crc >> 8; ++offset; goto case 13;
            case 13: crc = table[(byte)(crc ^ input[offset])] ^ crc >> 8; ++offset; goto case 12;
            case 12: crc = table[(byte)(crc ^ input[offset])] ^ crc >> 8; ++offset; goto case 11;
            case 11: crc = table[(byte)(crc ^ input[offset])] ^ crc >> 8; ++offset; goto case 10;
            case 10: crc = table[(byte)(crc ^ input[offset])] ^ crc >> 8; ++offset; goto case 09;
            case 09: crc = table[(byte)(crc ^ input[offset])] ^ crc >> 8; ++offset; goto case 08;
            case 08: crc = table[(byte)(crc ^ input[offset])] ^ crc >> 8; ++offset; goto case 07;
            case 07: crc = table[(byte)(crc ^ input[offset])] ^ crc >> 8; ++offset; goto case 06;
            case 06: crc = table[(byte)(crc ^ input[offset])] ^ crc >> 8; ++offset; goto case 05;
            case 05: crc = table[(byte)(crc ^ input[offset])] ^ crc >> 8; ++offset; goto case 04;
            case 04: crc = table[(byte)(crc ^ input[offset])] ^ crc >> 8; ++offset; goto case 03;
            case 03: crc = table[(byte)(crc ^ input[offset])] ^ crc >> 8; ++offset; goto case 02;
            case 02: crc = table[(byte)(crc ^ input[offset])] ^ crc >> 8; ++offset; goto case 01;
            case 01: crc = table[(byte)(crc ^ input[offset])] ^ crc >> 8; ++offset; break;
        }

        return crc ^ uint.MaxValue;
    }
}