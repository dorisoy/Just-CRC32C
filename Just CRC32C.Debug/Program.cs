byte[] ba = new byte[1024];
Random.Shared.NextBytes(ba);
var crc = JustCRC32C.Crc32C.Calculate(ba);
Console.WriteLine(crc);