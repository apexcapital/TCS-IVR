using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildABot.UC;
using System.Configuration;
using System.Diagnostics;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            GitHubService.GetApexBranches();

            // Print all Debug.WriteLine calls to the console to make it
            // easier to determine the cause of any errors.
            Debug.Listeners.Add(new ConsoleTraceListener());

            String applicationUserAgent = ConfigurationManager.AppSettings["applicationuseragent"];
            String applicationurn = ConfigurationManager.AppSettings["applicationurn"];
            UCBotHost ucBotHost = new UCBotHost(applicationUserAgent, applicationurn);
            ucBotHost.Run();
        }
    }
}
