using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WaitTasks
{
    class Program
    {
        static void Main(string[] args)
        {

            var cts = new CancellationTokenSource();
            var token = cts.Token;
            var t = new Task(()=>
            {

                Console.WriteLine("I Take 5 seconds");

                for(int i = 0; i < 5; i++)
                {
                    token.ThrowIfCancellationRequested();
                    Thread.Sleep(1000);
                }

                Console.WriteLine("I'm Done");

            },token);

            t.Start();

            Task t2 = Task.Factory.StartNew(() => Thread.Sleep(3000), token);

            //wait all task complete or the timeout or cancel one of them
            Task.WaitAll(new[] { t, t2 }, 4000, token);

            Console.WriteLine($"Task 1 status is {t.Status}");
            Console.WriteLine($"Task 2 Status is {t2.Status}");

            Console.Write("Main program is done");
            Console.ReadKey();

        }
    }
}
