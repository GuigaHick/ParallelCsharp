using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SincronizationMutex
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

        public void Transfer(Bank where, int amount)
        {
            this.Balance -= amount;
            where.Balance += amount;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {

            var tasks = new List<Task>();
            var ba = new Bank();
            var ba2 = new Bank();
            Mutex mutex = new Mutex();
            Mutex mutex2 = new Mutex();

            tasks.Add(Task.Factory.StartNew(() => {
                
                for(int j = 0; j < 1000; j++) { 
                var lockTaken = mutex.WaitOne();
                try
                {
                    ba.Deposit(1);
                }
                finally
                {
                    if (lockTaken) mutex.ReleaseMutex();
                }
                }
            }));
            tasks.Add(Task.Factory.StartNew(() => {
                for(int j = 0; j < 1000; j++)
                {
                    var lockTaken = mutex2.WaitOne();
                    try
                    {
                        ba2.Deposit(1);
                    }
                    finally
                    {
                        if (lockTaken) mutex2.ReleaseMutex();
                    }
                }
            }

            ));
                
            tasks.Add(Task.Factory.StartNew(() =>
            {
                for (int j = 0; j < 1000; j++)
                {
                    bool haveLock = WaitHandle.WaitAll(new[] { mutex, mutex2 });
                    try
                    {
                        ba.Transfer(ba2, 1);
                    }
                    finally
                    {
                        if(haveLock)
                        {
                            mutex.ReleaseMutex();
                            mutex2.ReleaseMutex();
                        }
                    }
                }

            }));
            
        Task.WaitAll(tasks.ToArray());

        Console.WriteLine($"The Final balance now is {ba.Balance}");//expected 0 after add lock
        Console.WriteLine($"The Final balance 2 now is {ba2.Balance}");//expected 0 after add lock
        Console.ReadKey();

        }
    }
}
