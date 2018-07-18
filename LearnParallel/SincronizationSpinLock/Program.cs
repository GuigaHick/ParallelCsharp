using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SincronizationSpinLock
{
    class Program
    {
        public class Bank
        {
            public int Balance { get; private set; }

            public void Deposit(int amount)
            {
                this.Balance += amount;
            }

            public void WithDraw(int amount)
            {

                this.Balance -= amount;
            }

        }
        static void Main(string[] args)
        {
            var tasks = new List<Task>();
            var ba = new Bank();
            SpinLock sl = new SpinLock();

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    tasks.Add(Task.Factory.StartNew(() => {

                        var lockTaken = false;
                        try
                        {
                            sl.Enter(ref lockTaken);
                            ba.Deposit(100);
                        }
                        finally
                        {
                           if (lockTaken) sl.Exit();
                        }
                        
                    }));
                }

                for (int j = 0; j < 100; j++)
                {
                    tasks.Add(Task.Factory.StartNew(() => {
                        var lockTaken = false;
                        try
                        {
                            sl.Enter(ref lockTaken);
                            ba.WithDraw(100);
                        }
                        finally
                        {
                           if(lockTaken) sl.Exit();
                        }
                     }
                    
                    ));
                }

            }
            Task.WaitAll(tasks.ToArray());

            Console.WriteLine($"The Final balance now is {ba.Balance}");//expected 0 after add lock

            Console.ReadKey();

        }
    }
}
