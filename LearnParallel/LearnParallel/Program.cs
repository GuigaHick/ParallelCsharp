using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LearnParallel
{
    class Program
    {
        static void Main(string[] args)
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;

            token.Register(() =>
            {
                Console.WriteLine("Cancelation has been requested");
            });

            var t = new Task(() =>
            {
                int i = 0;
                while (true)
                {
                    if (token.IsCancellationRequested)
                        break;
                    else
                        Console.WriteLine($"{i++}");
                }
            },token);

            t.Start();

            Console.ReadKey();
            cts.Cancel();

            Console.WriteLine("Programa finalizado com exito");
            Console.ReadKey();
        }
    }
}
