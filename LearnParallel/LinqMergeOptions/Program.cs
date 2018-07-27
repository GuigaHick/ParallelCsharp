using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqMergeOptions
{
    class Program
    {
        static void Main(string[] args)
        {
            var numbers = Enumerable.Range(1, 20);

            var results = numbers.
                AsParallel().
                WithMergeOptions(ParallelMergeOptions.FullyBuffered).
                Select(x =>
                {
                    var result = Math.Log(x);
                    Console.WriteLine($"P {result}\t");
                    return result;
                });

            foreach (var result in results)
            {
                Console.WriteLine($"Result = {result}");
            }

            Console.ReadKey();
        }
    }
}
