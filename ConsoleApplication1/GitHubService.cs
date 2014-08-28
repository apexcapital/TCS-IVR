using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class GitHubService
    {

        public static void GetApexBranches()
        {
            var url = "https://api.github.com/users/apexcapital/repos";


            // Syncronious consumption
            var syncClient = new WebClient();
            syncClient.Headers.Add("User-Agent: Other");   //that is the simple line!
            var content = syncClient.DownloadString(url);

            Console.WriteLine(content);

        }


    }
}
