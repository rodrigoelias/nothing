using Microsoft.Owin.Hosting;
using OwinSelfhostSample;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Host
{
    class Program
    {
        static void Main(string[] args)
        {
            int port = 9000;
            string baseAddress = String.Format("http://localhost:{0}/", port);

            StartOptions options = new StartOptions();
            options.Urls.Add(baseAddress);
            options.Urls.Add(String.Format("http://127.0.0.1:{0}", port));
            options.Urls.Add(String.Format("http://{0}:{1}", Environment.MachineName, port));
            
            // Start OWIN host 
            using (WebApp.Start<Startup>(options))
            {
                Console.WriteLine("Web API Self Hosted is running. Press Enter to Exit.");
                Console.ReadLine(); 
            }
        }
    }
}
