using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace _09_GithubStalker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a username");
            string username = Console.ReadLine();
            WebClient wc = new WebClient();
            WebClient wcr = new WebClient();
            wc.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            wcr.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            // a string that uses the webclients method to download the JSON object under indicated URL.
            string json = wc.DownloadString("https://api.github.com/users/" + username);
            string jsonRepos = wcr.DownloadString("https://api.github.com/users/" + username + "/repos");

            // after the JSON object is downloaded it is parsed into display...
            var o = JObject.Parse(json);
            var r = JArray.Parse(jsonRepos);
            
            Console.WriteLine("Name: " + o["name"]);
            Console.WriteLine("URL: " + o["url"]);
            Console.WriteLine("Followers: " + o["followers"]);
            Console.WriteLine();
            Console.WriteLine("Repositories: " + o["public_repos"]);

            //access repo details

            for (int i = 0; i < r.Count; i++)
            {
                WebClient wcc = new WebClient();
                WebClient wci = new WebClient();
                wcc.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                wci.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                string jsonCommits = wcc.DownloadString("https://api.github.com/repos/" + username + "/" + r[i]["name"] + "/commits");
                string jsonIssues = wci.DownloadString("https://api.github.com/repos/" + username + "/" + r[i]["name"] + "/issues");

                var c = JArray.Parse(jsonCommits);
                var issues = JArray.Parse(jsonIssues);

                Console.WriteLine(r[i]["name"] + ", " + r[i]["stargazers_count"] + " stars, " + r[i]["watchers_count"] + " and watchers.");
                Console.WriteLine("-- " + c.Count + " commits.");
                for (int j = 0; j < c.Count; j++)
                {
                    Console.WriteLine("---- " + c[j]["commit"]["message"]);
                }
                Console.WriteLine("-- " + issues.Count + " issues.");
                for (int k = 0; k < issues.Count; k++)
                {
                    Console.WriteLine("---- Issue number: " + issues[k]["number"]);
                }
            }
            Console.WriteLine("Hit \"enter\" to exit.");
            Console.ReadLine();


            /* Console.WriteLine("Enter a username");
            string username = Console.ReadLine();
            // webClient - class that simulates a browser
            // download 
            WebClient wc = new WebClient();
            // C# exception
            // GitHub needs statistics on who is calling the API. Doenst like anonymous access to its API.
            // Need to google c# github API webclient exception and the solution should populate.
            wc.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            // string that 
            string json = wc.DownloadString("https://api.github.com/users/" + username);
            // parse json into something I can display
            var o = JObject.Parse(json);
            // this will write the usersname on the screen
            Console.WriteLine(o["login"].ToString());
            // this will write username, public repositories, qty of followers, and qty of followed by.
            Console.WriteLine(o["login"].ToString() + " has " + o["public_repos"].ToString() + "public repositories, " + o["followers"].ToString() + " followers, " + "and is followed by " + o["following"].ToString());
            Console.ReadLine(); */

        }
    }
}
