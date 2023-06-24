namespace JustCRC32C.TestNF
{
    using System;
    using System.Runtime.InteropServices;
    using JustCRC32C;
    using NUnit.Framework;

    [TestFixture]
    public class Tests
    {
        [Test]
        public unsafe void TestCorrectValueBig()
        {
            if (!Crc32C.HasSSE42() || RuntimeInformation.ProcessArchitecture != Architecture.X64)
            {
                Assert.Ignore("SSE42 not supported or not running in x64 mode!");
                return;
            }
            byte[] ba = new byte[1024];
            new Random().NextBytes(ba);
            uint a, b;
            fixed (byte* ptrA = &ba[0])
            {
                a = (uint)Crc32C.CRC32_SSE42_Native_x64(ptrA, ba.Length);
                b = Crc32C.CRC32_SSE42_Native_x32(ptrA, ba.Length);
            }
            uint c = Crc32C.CalculateSoftware(ba);
            Assert.That(a == b);
            Assert.That(b == c);
        }

        [Test]
        public unsafe void TestCorrectValueSmall()
        {
            if (!Crc32C.HasSSE42() || RuntimeInformation.ProcessArchitecture != Architecture.X64)
            {
                Assert.Ignore("SSE42 not supported or not running in x64 mode!");
                return;
            }
            const uint result = 3808858755;
            var aArray = new byte[] { 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39 };
            var bArray = new byte[] { 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39 };
            uint a;
            uint b;
            fixed (byte* ptrA = &aArray[0])
                a = (uint)Crc32C.CRC32_SSE42_Native_x64(ptrA, aArray.Length);
            fixed (byte* ptrB = &bArray[0])
                b = Crc32C.CRC32_SSE42_Native_x32(ptrB, aArray.Length);
            uint c = Crc32C.CalculateSoftware(new byte[] { 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39 });
            Assert.That(a == result);
            Assert.That(b == result);
            Assert.That(c == result);
            Assert.True(true);
        }
    }
}