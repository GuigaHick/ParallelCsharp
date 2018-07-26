using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartitioningSample
{
    public class Program
    {
        [Benchmark]
        public void SquareEachValue()
        {
            const int count = 100000;
            var values = Enumerable.Range(1, count);
            var result = new int[count];
            Parallel.ForEach(values, x => { result[x] = (int)Math.Pow(x, 2); });

        }
        [Benchmark]
        public void SquareEachValueChuncked()
        {
            const int count = 100000;
            var values = Enumerable.Range(1, count);
            var result = new int[count];

            var part = Partitioner.Create(0, count, 10000);
            Parallel.ForEach(part, range =>
            {
                for (int i = 0; range.Item1 < range.Item2; i++)
                {
                    result[i] = (int)Math.Pow(i, 2);
                }
            });
        }

        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<Program>();
            Console.WriteLine(summary);

            Console.ReadKey();
        }
    }
}
