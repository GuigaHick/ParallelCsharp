using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WaitToCancel
{
    class Program
    {
        static void Main(string[] args)
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;

            Task t = Task.Factory.StartNew(() =>
            {

                Console.WriteLine("You have 5 seconds to cancel this task");
                bool cancelled = token.WaitHandle.WaitOne(5000);
                Console.WriteLine(cancelled ? "Bomb Disarmed" : "BOOOM");

            }, token);

            Console.ReadKey();
            cts.Cancel();

            Console.WriteLine("Main Program Done");
            Console.ReadKey();
        }
    }
}
