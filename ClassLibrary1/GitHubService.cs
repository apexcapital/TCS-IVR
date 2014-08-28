using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;

namespace ClassLibrary1
{
  public  class GitHubService
    {

        string card;
        string pin;
        string url;



        public  GitHubService(string card, string pin)
        {
           // var url = "https://api.github.com/users/apexcapital/repos";
            // http://irv-analogbytes.rhcloud.com/rest/cards/1234567890/1234
            this.url = "http://irv-analogbytes.rhcloud.com/rest/cards/auth/";
           // string username = "adminxk85swn";
          //  string password = "UhYbAd9SNgm2";
           // string card = "1234567890";
          //  string pin = "1234";
            this.card = card;
            this.pin = pin;

        }

        public string getBalance()
        {

            string separator = "/";
            var syncClient = new WebClient();

            syncClient.Headers.Add("User-Agent: Other");   //that is the simple line!

            var content = syncClient.DownloadString(url + card + separator + pin);

            Console.WriteLine(content);
            return content;

        }


    }
}
