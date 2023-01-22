# Just-CRC32C
[![License: LGPL](https://img.shields.io/github/license/bartimaeusnek/Just-CRC32C)](https://opensource.org/licenses/LGPL-3.0) [![nuget](https://img.shields.io/nuget/v/JustCRC32C.svg)](https://www.nuget.org/packages/JustCRC32C/)

Just a simple and fast CRC32C Wrapper with hardware acceleration.

The library overall is licensed under LGPLv3, so you can use it in a propriatary project, as long as you do not change the library itsef!

Software-fallback is taken from: [here](https://github.com/force-net/Crc32.NET/blob/26c5a818a5c7a3d6a622c92d3cd08dba586c263c/Crc32.NET/SafeProxy.cs#L38)
and Licensed under [MIT License](https://github.com/force-net/Crc32.NET/blob/26c5a818a5c7a3d6a622c92d3cd08dba586c263c/LICENSE) and improved by a tiny bit.

Benchmark results:
```
BenchmarkDotNet=v0.13.3, OS=Windows 10 (10.0.19044.2364/21H2/November2021Update)
AMD Ryzen 5 5600X, 1 CPU, 12 logical and 6 physical cores
.NET SDK=6.0.402
  [Host]     : .NET 6.0.12 (6.0.1222.56807), X64 RyuJIT AVX2
  Job-BYMXXN : .NET 6.0.12 (6.0.1222.56807), X64 RyuJIT AVX2

Runtime=.NET 6.0  RunStrategy=Throughput  
```

|                 Method |               Arrays |             Mean |          Error |         StdDev | Ratio | RatioSD |
|----------------------- |--------------------- |-----------------:|---------------:|---------------:|------:|--------:|
| JustCrc32C_HardwareX64 | big a(...)ytes) [41] |   330,226.069 μs |  5,736.6919 μs |  4,790.3975 μs |  1.00 |    0.00 |
|    JustCrc32C_Hardware | big a(...)ytes) [41] |   629,052.193 μs |  9,892.7473 μs |  9,253.6820 μs |  1.90 |    0.05 |
|    JustCrc32C_Software | big a(...)ytes) [41] | 1,668,792.867 μs | 22,200.8505 μs | 20,766.6893 μs |  5.04 |    0.11 |
|          Crc32_dot_NET | big a(...)ytes) [41] | 1,671,624.007 μs | 15,321.7433 μs | 14,331.9682 μs |  5.05 |    0.09 |
|                        |                      |                  |                |                |       |         |
| JustCrc32C_HardwareX64 | mediu(...)ytes) [44] |    26,340.056 μs |    430.1118 μs |    402.3269 μs |  1.00 |    0.00 |
|    JustCrc32C_Hardware | mediu(...)ytes) [44] |    50,136.805 μs |    807.4380 μs |    674.2473 μs |  1.91 |    0.05 |
|    JustCrc32C_Software | mediu(...)ytes) [44] |   133,309.879 μs |  1,613.7295 μs |  1,430.5292 μs |  5.07 |    0.10 |
|          Crc32_dot_NET | mediu(...)ytes) [44] |   133,926.738 μs |  1,478.0324 μs |  1,382.5524 μs |  5.09 |    0.11 |
|                        |                      |                  |                |                |       |         |
| JustCrc32C_HardwareX64 | small(...)ytes) [37] |       248.442 μs |      4.0474 μs |      3.7860 μs |  1.00 |    0.00 |
|    JustCrc32C_Hardware | small(...)ytes) [37] |       493.948 μs |      7.9443 μs |      7.4311 μs |  1.99 |    0.03 |
|    JustCrc32C_Software | small(...)ytes) [37] |     1,327.410 μs |     13.9663 μs |     13.0641 μs |  5.34 |    0.10 |
|          Crc32_dot_NET | small(...)ytes) [37] |     1,336.333 μs |     20.3987 μs |     19.0809 μs |  5.38 |    0.12 |
|                        |                      |                  |                |                |       |         |
| JustCrc32C_HardwareX64 | small(...)ytes) [49] |     2,592.696 μs |     32.4776 μs |     30.3796 μs |  1.00 |    0.00 |
|    JustCrc32C_Hardware | small(...)ytes) [49] |     5,028.597 μs |     86.9979 μs |     81.3779 μs |  1.94 |    0.03 |
|    JustCrc32C_Software | small(...)ytes) [49] |    13,351.273 μs |    180.4138 μs |    159.9322 μs |  5.15 |    0.08 |
|          Crc32_dot_NET | small(...)ytes) [49] |    13,440.784 μs |    154.0398 μs |    144.0889 μs |  5.18 |    0.08 |
|                        |                      |                  |                |                |       |         |
| JustCrc32C_HardwareX64 | small(...)ytes) [38] |        24.800 μs |      0.3488 μs |      0.3263 μs |  1.00 |    0.00 |
|    JustCrc32C_Hardware | small(...)ytes) [38] |        50.105 μs |      0.4064 μs |      0.3801 μs |  2.02 |    0.03 |
|    JustCrc32C_Software | small(...)ytes) [38] |       132.803 μs |      1.1026 μs |      1.0314 μs |  5.36 |    0.09 |
|          Crc32_dot_NET | small(...)ytes) [38] |       133.919 μs |      1.2902 μs |      1.0774 μs |  5.41 |    0.07 |
|                        |                      |                  |                |                |       |         |
| JustCrc32C_HardwareX64 | tiny (...)ytes) [27] |         2.335 μs |      0.0301 μs |      0.0282 μs |  1.00 |    0.00 |
|    JustCrc32C_Hardware | tiny (...)ytes) [27] |         4.284 μs |      0.0621 μs |      0.0581 μs |  1.83 |    0.03 |
|    JustCrc32C_Software | tiny (...)ytes) [27] |        16.036 μs |      0.2534 μs |      0.2370 μs |  6.87 |    0.14 |
|          Crc32_dot_NET | tiny (...)ytes) [27] |        16.510 μs |      0.1651 μs |      0.1544 μs |  7.07 |    0.12 |
