using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LoopErrorHandler
{
    class Program
    {
        public static ParallelLoopResult result;
        public static void Demo()
        {
            var cts = new CancellationTokenSource();

            ParallelOptions po = new ParallelOptions();
            po.CancellationToken = cts.Token;
            result = Parallel.For(0, 20, po,(int x,ParallelLoopState state) =>
             {
                 Console.WriteLine($"{x}[{Task.CurrentId}]\t");

                 if(x == 10)
                 {
                     //throw new Exception();
                     //state.Stop();
                     state.Break();
                 }
             });

            Console.WriteLine($"Loop was completed {result.IsCompleted}");
            if(result.LowestBreakIteration.HasValue)
            {
                Console.WriteLine($"Lowest break interation");
            }
        }

        static void Main(string[] args)
        {
            try
            {
                Demo();
            }
            catch (AggregateException ae)
            {
                ae.Handle(e =>
                 {
                     Console.WriteLine(e.Message);
                     return true;
                 });
                
            }
            

            Console.ReadKey();
        }
    }
}
