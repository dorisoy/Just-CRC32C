namespace JustCRC32C.Test;

using System;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.Intrinsics.X86;
using NUnit.Framework;

[TestFixture]
public class Crc32Test
{
    [Test]
    public void TestCorrectValueSmall()
    {
        if (!Sse42.IsSupported || RuntimeInformation.ProcessArchitecture != Architecture.X64)
        {
            Assert.Ignore("SSE42 not supported or not running in x64 mode!");
            return;
        }
        
        const uint result = 3808858755;
        uint a = Crc32C.CalculateHardware(new byte[]{0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39});
        uint b = Crc32C.CalculateSoftware(new byte[]{0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39});
        uint c = Crc32C.CalculateHardwareX64(new byte[]{0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39});
        Assert.That(a == result);
        Assert.That(b == result);
        Assert.That(c == result);
    }
    
    [Test]
    public void TestCorrectValueBig()
    {
        if (!Sse42.IsSupported || RuntimeInformation.ProcessArchitecture != Architecture.X64)
        {
            Assert.Ignore("SSE42 not supported or not running in x64 mode!");
            return;
        }
        
        byte[] ba = new byte[1024];
        Random.Shared.NextBytes(ba);
        uint result = Crc32C.CalculateHardware(ba);
        uint a = Crc32C.CalculateSoftware(ba);
        uint b = Crc32C.CalculateHardwareX64(ba);
        Assert.That(a == result);
        Assert.That(a == b);
    }
    
    [Test]
    public void TestCorrectValueSmallArm()
    {
        if (!Crc32.Arm64.IsSupported || !Crc32.IsSupported)
        {
            Assert.Ignore("Non-Arm Architecture or ARM32/64 CRC32C not supported");
            return;
        }
        
        const uint result = 3808858755;
        uint a = Crc32C.CalculateHardwareArm(new byte[]{0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39});
        uint b = Crc32C.CalculateSoftware(new byte[]{0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39});
        uint c = Crc32C.CalculateHardwareArm64(new byte[]{0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39});
        Assert.That(a == result);
        Assert.That(b == result);
        Assert.That(c == result);
    }
    
    [Test]
    public void TestCorrectValueBigArm()
    {
        if (!Crc32.Arm64.IsSupported || !Crc32.IsSupported)
        {
            Assert.Ignore("Non-Arm Architecture or ARM32/64 CRC32C not supported");
            return;
        }
        
        byte[] ba = new byte[1024];
        Random.Shared.NextBytes(ba);
        uint result = Crc32C.CalculateHardwareArm(ba);
        uint a = Crc32C.CalculateSoftware(ba);
        uint b = Crc32C.CalculateHardwareArm64(ba);
        Assert.That(a == result);
        Assert.That(a == b);
    }
}