using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadLocalStorage
{
    class Program
    {
        static void Main(string[] args)
        {
            int sum = 0;

            Parallel.For(1, 1001, 
              () => 0,
              (x, state, tls) =>
              {
                  tls = tls + x;
                  Console.WriteLine($"Task {Task.CurrentId} has sum {tls}");
                  return tls;
              },
              partialSum =>
              {
                  Console.WriteLine($"Partial Value of task {Task.CurrentId} has sum {partialSum}");
                  Interlocked.Add(ref sum, partialSum);
              });
            Console.WriteLine($"Sum of 1..1000 = {sum}");
            Console.ReadKey();
        }
    }
}
