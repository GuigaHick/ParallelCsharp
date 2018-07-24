using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Continuations
{
    class Program
    {
        static void Main(string[] args)
        {
            var task1 = Task<string>.Factory.StartNew(() => "Task1");
            var task2 = Task<string>.Factory.StartNew(() => "Task2");

            var task3 = Task.Factory.ContinueWhenAll(new[] { task1, task2 }, 
             tasks =>
             {
                 Console.WriteLine("Tasks Completed");
                 foreach (var t in tasks)
                    Console.WriteLine("-" + t.Result);
                 Console.WriteLine("All Tasks Done");
             });
            task3.Wait();
            var task4 = Task.Factory.StartNew(() => "Task4");
            var task5 = Task.Factory.StartNew(() => "Task5");
            var task6 = Task.Factory.ContinueWhenAny(new[] { task4, task5 }, t =>
            {
                Console.WriteLine("Any Tasks Completed");
                Console.WriteLine("-" + t.Result);
                Console.WriteLine("All Tasks Done");
            });
            Console.ReadKey();
        }
    }
}
