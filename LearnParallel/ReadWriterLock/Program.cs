using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReadWriterLock
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
        static ReaderWriterLockSlim padlock = new ReaderWriterLockSlim();

        static void Main(string[] args)
        {
            int x = 0;
            var tasks = new List<Task>();
            for (int i = 0; i < 10; i++)
            {

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    padlock.EnterReadLock();

                    Console.WriteLine($"current value is {x}");
                    Thread.Sleep(5000);
                    padlock.ExitReadLock();
                    Console.WriteLine($"unprotected current value is {x}");
                }));
            }

            try
            {
                Task.WaitAll(tasks.ToArray());
            }
            catch (AggregateException ae)
            {
                ae.Handle(e =>
                {
                    Console.WriteLine(e);
                    return true;
                });
            }

            while(true)
            {
                Console.ReadKey();
                padlock.EnterWriteLock();
                Console.WriteLine("Write Lock Aquired");
                Random rd = new Random();
                int newValue = rd.Next(10);
                x = newValue;
                Console.WriteLine($"set X = {x}");
                padlock.ExitWriteLock();
                Console.WriteLine("Red Lock Released");
            }
        }
    }
}
