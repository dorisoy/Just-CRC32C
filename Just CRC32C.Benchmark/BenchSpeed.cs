namespace Just_CRC32.Benchmark;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;
using Force.Crc32;
using JustCRC32C;

[SimpleJob(RunStrategy.Throughput, RuntimeMoniker.Net60)]
public class BenchSpeed
{
    [ParamsSource(nameof(ValuesForA))]
    public List<byte[]> Arrays;

    public static IEnumerable<List<byte[]>> ValuesForA()
    {
        var r = new Random();
        // var tinyArrays = new List<byte[]>();
        // for (int i = 4; i < 250; i++)
        // {
        //     tinyArrays.Add(new byte[i]);
        //     r.NextBytes(tinyArrays[i - 4]);
        // }
        // yield return tinyArrays;
        //
        var smallish = new List<byte[]>();
        for (int i = 1_000; i < 25_000; i+= 1_000)
        {
            smallish.Add(new byte[i]);
            r.NextBytes(smallish[i / 1_000 - 1]);
        }
        yield return smallish;
        
        var smallArrays = new List<byte[]>();
        for (int i = 10_000; i < 250_000; i+= 10_000)
        {
            smallArrays.Add(new byte[i]);
            r.NextBytes(smallArrays[i / 10_000 - 1]);
        }
        yield return smallArrays;
        
        var smallmediumArrays = new List<byte[]>();
        for (int i = 100_000; i < 25_000_000; i+= 100_000)
        {
            smallmediumArrays.Add(new byte[i]);
            r.NextBytes(smallmediumArrays[i / 100_000 - 1]);
        }
        yield return smallmediumArrays;
        
        // var mediumArrays = new List<byte[]>();
        // for (int i = 1_000_000; i < 25_000_000; i+= 1_000_000)
        // {
        //     mediumArrays.Add(new byte[i]);
        //     r.NextBytes(mediumArrays[i / 1_000_000 - 1]);
        // }
        // yield return mediumArrays;
        //
        // var bigArrays = new List<byte[]>();
        // bigArrays.Add(new byte[Array.MaxLength]);
        // r.NextBytes(bigArrays[0]);
        // bigArrays.Add(new byte[Array.MaxLength / 2]);
        // r.NextBytes(bigArrays[1]);
        // bigArrays.Add(new byte[Array.MaxLength / 4]);
        // r.NextBytes(bigArrays[2]);
        // yield return bigArrays;
    }

    [Benchmark(Baseline = true)]
    public void JustCrc32C_HardwareX64()
    {
        foreach (byte[] bytes in Arrays)
        {   
            Crc32C.CalculateHardwareX64(bytes);
        }
    }
    
    [Benchmark]
    public void JustCrc32C_Hardware()
    {
        foreach (byte[] bytes in Arrays)
        {   
            Crc32C.CalculateHardware(bytes);
        }
    }
    
    [Benchmark]
    public void JustCrc32C_Software()
    {
        foreach (byte[] bytes in Arrays)
        {   
            Crc32C.CalculateSoftware(bytes);
        }
    }
    
    [Benchmark]
    public void Crc32_dot_NET()
    {
        foreach (byte[] bytes in Arrays)
        {   
            Crc32CAlgorithm.Compute(bytes);
        }
    }
}