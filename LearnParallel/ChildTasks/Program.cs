using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChildTasks
{
    class Program
    {
        static void Main(string[] args)
        {
            var parent = new Task(() =>
            {
                var child = new Task(() =>
                {
                    Console.WriteLine("Child Task Starting");
                    Thread.Sleep(3000);
                    Console.WriteLine("Child  Task Finishing");

                },TaskCreationOptions.AttachedToParent);

                var completionHandler = child.ContinueWith(t =>
                {
                    Console.WriteLine($"Hooray, Task { t.Id} status is {t.Status}");
                }, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.OnlyOnRanToCompletion);

                var failHandler = child.ContinueWith(t =>
               {
                   Console.WriteLine($"Hooray, Task { t.Id} status is {t.Status}");
               },TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.OnlyOnFaulted);

                child.Start();
            });

            parent.Start();

            try
            {
                parent.Wait();
            }
            catch (AggregateException ae)
            {
                ae.Handle(e => true);
                throw;
            }

            Console.ReadKey();
        }
    }
}
