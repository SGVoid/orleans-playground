using System;
using Orleans;
using Orleans.Runtime.Host;

namespace ConsoleAppSiloHost
{
    class Program
    {
        static SiloHost siloHost;

        static void Main(string[] args)
        {
            InitSilo();

            //DoSomeClientWork();

            Console.WriteLine("Orleans Silo is running.\nPress Enter to terminate...");
            Console.ReadLine();

            // We do a clean shutdown in the other AppDomain
            ShutdownSilo();
        }


        static void InitSilo()
        {
            siloHost = new SiloHost(System.Net.Dns.GetHostName());
            // The Cluster config is quirky and weird to configure in code, so we're going to use a config file
            siloHost.ConfigFileName = "OrleansConfiguration.xml";

            siloHost.InitializeOrleansSilo();
            var startedok = siloHost.StartOrleansSilo();
            if (!startedok)
                throw new SystemException(String.Format("Failed to start Orleans silo '{0}' as a {1} node", siloHost.Name, siloHost.Type));

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
