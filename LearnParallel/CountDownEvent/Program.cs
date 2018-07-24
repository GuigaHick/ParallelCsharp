using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CountDownEventSincronize
{
    class Program
    {
        private static int taskCount = 5;
        static CountdownEvent cte = new CountdownEvent(taskCount);
        static Random random = new Random();
        static void Main(string[] args)
        {
            for (int i = 0; i < taskCount; i++)
            {
                Task.Factory.StartNew(() =>
                {

                    Console.WriteLine($"Entering Task {Task.CurrentId}");
                    Thread.Sleep(random.Next(3000));
                    cte.Signal();
                    Console.WriteLine($"Exiting Task { Task.CurrentId}");
                });
            }

            var finalTask = Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"Waiting for other tasks to complete in {Task.CurrentId}");
                cte.Wait();
                Console.WriteLine($"All Taks Completed");
            });

            finalTask.Wait();
            Console.ReadKey();
        }
    }
}
