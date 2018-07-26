``` ini

BenchmarkDotNet=v0.11.0, OS=Windows 8.1 (6.3.9600.0)
Intel Core i7-3612QM CPU 2.10GHz (Ivy Bridge), 1 CPU, 8 logical and 4 physical cores
Frequency=2046132 Hz, Resolution=488.7270 ns, Timer=TSC
  [Host] : .NET Framework 4.7.1 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.7.3130.0


```
|                  Method | Mean | Error |
|------------------------ |-----:|------:|
|         SquareEachValue |   NA |    NA |
| SquareEachValueChuncked |   NA |    NA |

Benchmarks with issues:
  Program.SquareEachValue: DefaultJob
  Program.SquareEachValueChuncked: DefaultJob
