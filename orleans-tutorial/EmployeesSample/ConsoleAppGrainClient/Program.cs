using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrainInterfaces;
using Orleans;

namespace ConsoleAppGrainClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Waiting for Orleans Silo to start. Press Enter to proceed...");
            Console.ReadLine();

            // Orleans comes with a rich XML and programmatic configuration. Here we're just going to set up with basic programmatic config
            var config = Orleans.Runtime.Configuration.ClientConfiguration.LocalhostSilo(30000);
            GrainClient.Initialize(config);

            var grainFactory = GrainClient.GrainFactory;
            var e0 = grainFactory.GetGrain<IEmployee>(Guid.NewGuid());  
            var e1 = grainFactory.GetGrain<IEmployee>(Guid.NewGuid());
            var e2 = grainFactory.GetGrain<IEmployee>(Guid.NewGuid());
            var e3 = grainFactory.GetGrain<IEmployee>(Guid.NewGuid());
            var e4 = grainFactory.GetGrain<IEmployee>(Guid.NewGuid());

            var m0 = grainFactory.GetGrain<IManager>(Guid.NewGuid());
            var m1 = grainFactory.GetGrain<IManager>(Guid.NewGuid());
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
