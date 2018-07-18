using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionHandling
{
    class Program
    {
        private static void Test()
        {
            var t1 = Task.Factory.StartNew(() => { throw new  InvalidOperationException("Can't do this"); });
            var t2 = Task.Factory.StartNew(() => { throw new AccessViolationException("Can't access"); });
            try
            {
                Task.WaitAll(t1, t2);
            }
            catch (AggregateException ae)
            {
                ae.Handle(e =>
                {
                    if(e is InvalidOperationException)
                    {
                        Console.WriteLine("Invalid Operation ocurred");
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                });

            }
            
        }

        public static void Main(string[] args)
        {
            try
            {
                Test();
            }
            catch (AggregateException ae)
            {
                foreach (var e in ae.InnerExceptions)
                {
                    Console.WriteLine($"Handled elsewhere { e.GetType()}");
                }
            }

            Console.Write("Main Program Done");
            Console.ReadKey();
        }
    }
}
