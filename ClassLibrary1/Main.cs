using BuildABot.UC;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    class Main
    {
        static void Main(string[] args)
        {
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
