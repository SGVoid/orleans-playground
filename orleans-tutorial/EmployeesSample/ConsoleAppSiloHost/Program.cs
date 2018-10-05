using System;
using System.Net.Sockets;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Hosting;
using Orleans.Runtime.Host;

namespace ConsoleAppSiloHost
{
    class Program
    {
        static ISiloHost siloHost;

        static void Main(string[] args)
        {
            InitSilo();

            Console.WriteLine("Orleans Silo is running.\nPress Enter to terminate...");
            Console.ReadLine();

            ShutdownSilo();
        }


        static void InitSilo()
        {
            siloHost = new SiloHostBuilder()
                .ConfigureEndpoints(30000, 11111)
                .UseLocalhostClustering()
                .ConfigureLogging(x => x.AddConsole())
                .UseDashboard(options =>
                {
                    options.Username = "USERNAME";
                    options.Password = "PASSWORD";
                    options.Host = "*";
                    options.Port = 8080;
                    options.HostSelf = false;
                    //options.CounterUpdateIntervalMs = 1000;
                })
                .Build();

            siloHost.StartAsync().Wait();
        }

        static void ShutdownSilo()
        {
            if (siloHost != null)
            {
                siloHost.Dispose();
                GC.SuppressFinalize(siloHost);
                siloHost = null;
            }
        }

    }
}
