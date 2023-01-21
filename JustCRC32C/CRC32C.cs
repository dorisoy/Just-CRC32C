namespace JustCRC32C;

using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;
#if NET5_0_OR_GREATER
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;
#endif

public static partial class Crc32C
{
    static Crc32C()
    {
        // Check if the CPU supports the SSE4.2 instruction set
        if (Sse42.IsSupported)
        {
            // Check if the process is x64
            if (RuntimeInformation.ProcessArchitecture == Architecture.X64)
            {
                ToUse = CalculateHardwareX64;
            }
            else
            {
                ToUse = CalculateHardware;
            }
        }
#if NET5_0_OR_GREATER
        // Check if the CPU supports the Arm64.Crc32 instruction set
        else if (Crc32.Arm64.IsSupported)
        {
            ToUse = CalculateHardwareArm64;
        }
        // Check if the CPU supports the Arm32.Crc32 instruction set
        else if (Crc32.IsSupported)
        {
            ToUse = CalculateHardwareArm;
        }
#endif
        else
        {
            ToUse = CalculateSoftware;
        }
    }

    private delegate uint ToUseDefinition(Span<byte> data);
    private static readonly ToUseDefinition ToUse;
#if NET5_0_OR_GREATER
    [SkipLocalsInit]
#endif
    public static uint Calculate(Span<byte> data)
    {
        return ToUse(data);
    }
    
#if NET5_0_OR_GREATER
    [SkipLocalsInit]
#endif
    public static void Calculate(out uint crc, Span<byte> data)
    {
        crc = ToUse(data);
    }
}