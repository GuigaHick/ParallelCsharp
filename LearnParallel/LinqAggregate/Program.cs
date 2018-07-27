using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqAggregate
{
    class Program
    {
        static void Main(string[] args)
        {
            var sum = ParallelEnumerable.Range(1, 1000).Aggregate(0,(partialSum,i) => 
            partialSum +=i,
            (total, subtotal) => total += subtotal,
            i => i);

            Console.WriteLine($"Sum is {sum}");
            Console.ReadKey();
        }
    }
}
