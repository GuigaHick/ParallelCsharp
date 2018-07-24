using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BarrierSincronize
{
    class Program
    {
        static Barrier barrier = new Barrier(2, b =>
        {
            Console.WriteLine($"Phase {b.CurrentPhaseNumber} is finished");
        });

        public static void Water()
        {
            Console.WriteLine($"Putting the Kettle on (takes a bit longer");
            Thread.Sleep(2000);
            barrier.SignalAndWait();
            Console.WriteLine("Put the water on the cup");
            barrier.SignalAndWait();
            Console.WriteLine("Putting the Kettle away");
        }

        public static void Cup()
        {
            Console.WriteLine("Finding the nicest cup of tea(Fast)");
            barrier.SignalAndWait();
            Console.WriteLine("Adding Tea");
            barrier.SignalAndWait();
            Console.WriteLine("Adding Sugar");
        }

        static void Main(string[] args)
        {
            var water = Task.Factory.StartNew(Water);
            var cup = Task.Factory.StartNew(Cup);
            var tea = Task.Factory.ContinueWhenAll(new[] { water, cup }, tasks =>
            {
                Console.WriteLine("Enjoy the cup of Tea");
            });

            Console.ReadKey();
        }
    }
}
