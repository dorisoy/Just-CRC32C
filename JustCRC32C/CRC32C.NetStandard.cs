#if NETSTANDARD
namespace JustCRC32C;
using System.Runtime.InteropServices;
public static partial class Crc32C
{
    internal unsafe static uint CalculateHardwareX64(Span<byte> data)
    {
        fixed (byte* ptr = &data.GetPinnableReference())
            return (uint)CRC32_SSE42_Native_x64(ptr, data.Length);
    }
    internal unsafe static uint CalculateHardware(Span<byte> data)
    {
        fixed (byte* ptr = &data.GetPinnableReference())
            return CRC32_SSE42_Native_x32(ptr, data.Length);
    }
    static Crc32C()
    {
        //.netstandard2.0 will only support windows hardware intrinsics.
        bool isAnyWindows = (Environment.OSVersion.Platform == PlatformID.Win32NT || Environment.OSVersion.Platform == PlatformID.Win32Windows || Environment.OSVersion.Platform == PlatformID.WinCE);
        if (!isAnyWindows || !HasSSE42())
        {
            ToUse = CalculateSoftware;
        }
        else
        {
            if (RuntimeInformation.ProcessArchitecture == Architecture.X64)
            {
                ToUse = CalculateHardwareX64;
            }
            else
            {
                ToUse = CalculateHardware;
            }
        }
    }
    
    [DllImport("libJustCRC32C_Native.dll", CallingConvention = CallingConvention.Cdecl)]
    internal extern static bool HasSSE42();
    
    [DllImport("libJustCRC32C_Native.dll", CallingConvention = CallingConvention.Cdecl)]
    internal extern unsafe static ulong CRC32_SSE42_Native_x64(byte* ptr, int ptr_length);

    [DllImport("libJustCRC32C_Native.dll", CallingConvention = CallingConvention.Cdecl)]
    internal extern unsafe static uint CRC32_SSE42_Native_x32(byte* ptr, int ptr_length);
}
#endif