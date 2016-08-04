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
            var token = await ApiWrapper.GetApiToken("tom", "MK3$tpthggguhdwu", "services");
            ApiWrapper.ApiToken = token.Data.token;
          //  var created = await ApiWrapper.CreateUser("tvlll", "AaO", "vdStr", "linden@tom.nl", "zydcp-Q!GA1FdQbmmd");
            var fullnames = new string[] {"CursusEen", "CursusTwee", "CursusDrie", "CursusVier", "CursusVijf" };
            var shortnames = new string[] { "Een", "Twee", "Drie", "Vier", "Vijf" };
            var ids = new int[] {1, 1, 1, 1, 1};

            var test = await ApiWrapper.CreateCourses(fullnames, shortnames, ids);

            test = null;
        }
    }
}
