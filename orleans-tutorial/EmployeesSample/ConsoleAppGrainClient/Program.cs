using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using GrainInterfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Orleans;
using Orleans.Runtime.Configuration;

namespace ConsoleAppGrainClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Waiting for Orleans Silo to start. Press Enter to proceed...");
            Console.ReadLine();

            var client = new ClientBuilder()
                .UseLocalhostClustering()
                .Build();

            client.Connect().Wait();

            var e0 = client.GetGrain<IEmployee>(Guid.NewGuid());  
            var e1 = client.GetGrain<IEmployee>(Guid.NewGuid());
            var e2 = client.GetGrain<IEmployee>(Guid.NewGuid());
            var e3 = client.GetGrain<IEmployee>(Guid.NewGuid());
            var e4 = client.GetGrain<IEmployee>(Guid.NewGuid());

            var m0 = client.GetGrain<IManager>(Guid.NewGuid());
            var m1 = client.GetGrain<IManager>(Guid.NewGuid());
            var m0e = m0.AsEmployee().Result;
            var m1e = m1.AsEmployee().Result;

            m0e.Promote(10);
            m1e.Promote(11);

            m0.AddDirectReport(e0).Wait();
            m0.AddDirectReport(e1).Wait();
            m0.AddDirectReport(e2).Wait();

            m1.AddDirectReport(m0e).Wait();
            m1.AddDirectReport(e3).Wait();

            m1.AddDirectReport(e4).Wait();

            Console.WriteLine("Orleans Silo is running.\nPress Enter to terminate...");
            Console.ReadLine();
        }
    }
}
