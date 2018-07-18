using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sincronization
{
    public class Bank
    {
        public object padLock = new object();
        public int Balance { get; private set; }

        public void Deposit(int amount)
        {
            lock(padLock)
            {
                this.Balance += amount;
            }
            
        }

        public void WithDraw(int amount)
        {
            lock(padLock)
            {
                this.Balance -= amount;
            }
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            var tasks = new List<Task>();
            var ba = new Bank();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    tasks.Add(Task.Factory.StartNew(() => { ba.Deposit(100); }));
                }

                for (int j = 0; j < 100; j++)
                {
                    tasks.Add(Task.Factory.StartNew(() => { ba.WithDraw(100); }));
                }

            }
            Task.WaitAll(tasks.ToArray());

            Console.WriteLine($"The Final balance now is {ba.Balance}");//expected 0 after add lock

            Console.ReadKey();


        }
    }
}
