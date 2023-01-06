namespace JustCRC32C;

using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;

public static partial class Crc32C
{
    /**
     *  Calculate the CRC32 checksum using the ARM Crc32 instruction set
     */
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [SkipLocalsInit]
    internal static uint CalculateHardwareArm(Span<byte> data)
    {
        uint crc = 0xFFFFFFFF;
        int i = 0;
        int length = data.Length;
        
        // Process the data in blocks of 16 bytes
        while (length >= 16)
        {
            uint block = Unsafe.ReadUnaligned<uint>(ref data[i]);
            crc = Crc32.ComputeCrc32C(crc, block);
            block = Unsafe.ReadUnaligned<uint>(ref data[i + 4]);
            crc = Crc32.ComputeCrc32C(crc, block);
            block = Unsafe.ReadUnaligned<uint>(ref data[i + 8]);
            crc = Crc32.ComputeCrc32C(crc, block);
            block = Unsafe.ReadUnaligned<uint>(ref data[i + 12]);
            crc = Crc32.ComputeCrc32C(crc, block);
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
                crc = Crc32.ComputeCrc32C(crc, block);
                i += 4;
                length -= 4;
                goto remaining; //🍝
            }
            case >= 2:
            {
                ushort block = Unsafe.ReadUnaligned<ushort>(ref data[i]);
                crc = Crc32.ComputeCrc32C(crc, block);
                i += 2;
                length -= 2;
                goto remaining; //🍝
            }
            case >= 1:
            {
                crc = Crc32.ComputeCrc32C(crc, data[i]);
                ++i;
                --length;
                goto remaining; //🍝
            }
            default:
                return ~crc;
        }
    }
    
    /**
     *  Calculate the CRC32 checksum using the ARM64 Crc32 instruction set
     */
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [SkipLocalsInit]
    internal static uint CalculateHardwareArm64(Span<byte> data)
    {
        uint crc = 0xFFFFFFFF;
        int i = 0;
        int length = data.Length;
        
        // Process the data in blocks of 32 bytes
        while (length >= 32)
        {
            ulong block = Unsafe.ReadUnaligned<ulong>(ref data[i]);
            crc = Crc32.Arm64.ComputeCrc32C(crc, block);
            block = Unsafe.ReadUnaligned<ulong>(ref data[i + 8]);
            crc = Crc32.Arm64.ComputeCrc32C(crc, block);
            block = Unsafe.ReadUnaligned<ulong>(ref data[i + 16]);
            crc = Crc32.Arm64.ComputeCrc32C(crc, block);
            block = Unsafe.ReadUnaligned<ulong>(ref data[i + 24]);
            crc = Crc32.Arm64.ComputeCrc32C(crc, block);
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
                crc = Crc32.Arm64.ComputeCrc32C(crc, block);
                i += 8;
                length -= 8;
                goto remaining; //🍝
            }
            case >= 4:
            {
                uint block = Unsafe.ReadUnaligned<uint>(ref data[i]);
                crc = Crc32.ComputeCrc32C(crc, block);
                i += 4;
                length -= 4;
                goto remaining; //🍝
            }
            case >= 2:
            {
                ushort block = Unsafe.ReadUnaligned<ushort>(ref data[i]);
                crc = Crc32.ComputeCrc32C(crc, block);
                i += 2;
                length -= 2;
                goto remaining; //🍝
            }
            case >= 1:
            {
                crc = Crc32.ComputeCrc32C(crc, data[i]);
                ++i;
                --length;
                goto remaining; //🍝
            }
            default:
                return ~crc;
        }
    }
}