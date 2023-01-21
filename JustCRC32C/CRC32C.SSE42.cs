namespace JustCRC32C;

using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;

public static partial class Crc32C
{
    /**
     *  Calculate the CRC32 checksum using the SSE4.2 instruction set
     */
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NET5_0_OR_GREATER
    [SkipLocalsInit]
#endif
    internal static uint CalculateHardware(Span<byte> data)
    {
        uint crc = 0xFFFFFFFF;
        int i = 0;
        int length = data.Length;
        
        // Process the data in blocks of 16 bytes
        while (length >= 16)
        {
            uint block = Unsafe.ReadUnaligned<uint>(ref data[i]);
            crc = Sse42.Crc32(crc, block);
            block = Unsafe.ReadUnaligned<uint>(ref data[i + 4]);
            crc = Sse42.Crc32(crc, block);
            block = Unsafe.ReadUnaligned<uint>(ref data[i + 8]);
            crc = Sse42.Crc32(crc, block);
            block = Unsafe.ReadUnaligned<uint>(ref data[i + 12]);
            crc = Sse42.Crc32(crc, block);
            i += 16;
            length -= 16;
        }

        // Process the remaining data in 🍝 fashion
    remaining://no loop here. 🍝
        switch (length)
        {
            case >= 4:
            {
                uint block = Unsafe.ReadUnaligned<uint>(ref data[i]);
                crc = Sse42.Crc32(crc, block);
                i += 4;
                length -= 4;
                goto remaining; //🍝
            }
            case >= 2:
            {
                ushort block = Unsafe.ReadUnaligned<ushort>(ref data[i]);
                crc = Sse42.Crc32(crc, block);
                i += 2;
                length -= 2;
                goto remaining; //🍝
            }
            case >= 1:
            {
                crc = Sse42.Crc32(crc, data[i]);
                ++i;
                --length;
                goto remaining; //🍝
            }
            default:
                return ~crc;
        }
    }

    /**
     *  Calculate the CRC32 checksum using the SSE4.2 instruction set for x64 Processors
     */
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NET5_0_OR_GREATER
    [SkipLocalsInit]
#endif
    internal static uint CalculateHardwareX64(Span<byte> data)
    {
        ulong crc = 0x00000000_FFFFFFFF;
        int i = 0;
        int length = data.Length;
        
        // Process the data in blocks of 32 bytes
        while (length >= 32)
        {
            ulong block = Unsafe.ReadUnaligned<ulong>(ref data[i]);
            crc = Sse42.X64.Crc32(crc, block);
            block = Unsafe.ReadUnaligned<ulong>(ref data[i + 8]);
            crc = Sse42.X64.Crc32(crc, block);
            block = Unsafe.ReadUnaligned<ulong>(ref data[i + 16]);
            crc = Sse42.X64.Crc32(crc, block);
            block = Unsafe.ReadUnaligned<ulong>(ref data[i + 24]);
            crc = Sse42.X64.Crc32(crc, block);
            i += 32;
            length -= 32;
        }
        
        // Process the remaining data in 🍝 fashion
    remaining://no loop here. 🍝
        switch (length)
        {
            case >= 8:
            {
                ulong block = Unsafe.ReadUnaligned<ulong>(ref data[i]);
                crc = Sse42.X64.Crc32(crc, block);
                i += 8;
                length -= 8;
                goto remaining; //🍝
            }
            case >= 4:
            {
                uint block = Unsafe.ReadUnaligned<uint>(ref data[i]);
                crc = Sse42.Crc32((uint) crc, block);
                i += 4;
                length -= 4;
                goto remaining; //🍝
            }
            case >= 2:
            {
                ushort block = Unsafe.ReadUnaligned<ushort>(ref data[i]);
                crc = Sse42.Crc32((uint) crc, block);
                i += 2;
                length -= 2;
                goto remaining; //🍝
            }
            case >= 1:
            {
                crc = Sse42.Crc32((uint) crc, data[i]);
                ++i;
                --length;
                goto remaining; //🍝
            }
            default:
                return ~(uint) crc;
        }
    }
}