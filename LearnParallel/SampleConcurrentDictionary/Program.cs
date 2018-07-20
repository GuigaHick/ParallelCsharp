using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleConcurrentDictionary
{
    class Program
    {
        private static ConcurrentDictionary<string, string> capitals = new ConcurrentDictionary<string, string>();

        public static void AddParis()
        {
            bool success = capitals.TryAdd("France", "Paris");
            string who = Task.CurrentId.HasValue ? ("Task" + Task.CurrentId) : "Main Thread";
            Console.WriteLine($"{who} {(success ? "added":"did not added")} the element.");
        }

        public static void Main(string[] args)
        {
            Task.Factory.StartNew(AddParis).Wait();
            AddParis();

            capitals["Russia"] = "Leningrad";
            capitals.AddOrUpdate("Russia", "Moscow",(k,old)=>old + "--->Moscow");
            Console.WriteLine($"The Capital of Russia is {capitals["Russia"]}");

            capitals["Sweden"] = "Uppsala";
            var capOfSweden = capitals.GetOrAdd("Sweden", "Stockholm");
            Console.WriteLine($"The Capital of Sweden is {capOfSweden}");

            const string toRemove = "Russia";
            string removed;
            var didRemove = capitals.TryRemove(toRemove, out removed);
            if(didRemove)
            {
                Console.WriteLine($"We just removed {removed}");
            }

            foreach (var capital in capitals)
            {
                Console.WriteLine($"-{capital.Value} is the capital of {capital.Key}");
            }
            Console.ReadKey();
        }
    }
}
