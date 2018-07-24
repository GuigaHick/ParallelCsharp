using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoManualResetEvent
{
    class Program
    {
        static void Main(string[] args)
        {
            var evt = new ManualResetEventSlim(false);

            Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Boiling Water");
                evt.Set();//true
            });

            var makeTea = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Waiting for Water");
                evt.Wait();//true
                Console.WriteLine("Here is your tea");
                var ok = evt.Wait(1000);
                if(ok)
                {
                    Console.WriteLine("Enjoy your tea");
                }
                else
                {
                    Console.WriteLine("No Tea For you");
                }
            });

            Console.ReadKey();
        }
    }
}
