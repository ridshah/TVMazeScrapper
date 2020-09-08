using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace TVMazeScrapper
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var webhost = CreateWebHostBuilder(args).Build();
            
            Thread thread = new Thread(new ThreadStart(InvokeMethod));
            thread.Start();
            
            webhost.Run();
        }

        static void InvokeMethod()
        {
            while (true)
            {
                Task scrapperTask = Task.Run(() => Scrapper.GetData());
                //Run every 24 hour
                Thread.Sleep(1440 * 60 * 1000);
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
