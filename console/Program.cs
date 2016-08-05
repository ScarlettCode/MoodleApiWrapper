using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MoodleApiWrapper;

namespace console
{
    class Program
    {
        static void Main(string[] args)
        {
            DoSomething();


            Console.ReadLine();
        }

        private static async void DoSomething()
        {
            ApiWrapper.Host = new Uri("http://detussenschool.nl/elo/");
            ApiWrapper.ApiToken = "dabe95abf7e2bdf5a9633a2de16b4ac2";

            var names = new string[] {"event1", "event2" , "event3" , "event4" , "event5" , "event6" };
            var starttimes = new DateTime[]
            {
                DateTime.Now.AddDays(1), 
                DateTime.Now.AddDays(2),
                DateTime.Now.AddDays(3),
                DateTime.Now.AddDays(4),
                DateTime.Now.AddDays(5),
                DateTime.Now.AddDays(6),
            };
            var durations = new TimeSpan[]
            {
                TimeSpan.FromMinutes(50),
                TimeSpan.FromMinutes(50),
                TimeSpan.FromMinutes(50),
                TimeSpan.FromMinutes(50),
                TimeSpan.FromMinutes(50),
                TimeSpan.FromMinutes(50),
            };
            var test = await ApiWrapper.CreateCalanderEvents(names, timestarts: starttimes, timedurations: durations);

             test = null;
        }
    }
}
