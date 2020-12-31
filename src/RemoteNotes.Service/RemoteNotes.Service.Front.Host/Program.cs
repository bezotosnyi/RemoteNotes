using System;
using System.Configuration;
using System.ServiceModel;
using Autofac;
using Autofac.Integration.Wcf;
using RemoteNotes.Logging.Contract;
using RemoteNotes.Service.Front.Contract;
using RemoteNotes.Service.Front.Host.Configuration;

namespace RemoteNotes.Service.Front.Host
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var container = AutofacConfig.Configure())
            {
                var uri = new Uri(ConfigurationManager.AppSettings["uri"]);
                using (var serviceHost = new ServiceHost(typeof(RemoteNotesService), uri))
                {
                    var logger = container.Resolve<IRemoteNotesLogger<Program>>();

                    try
                    {
                        serviceHost.AddDependencyInjectionBehavior<IRemoteNotesService>(container);
                        serviceHost.Open();

                        logger.Info($"Service {uri} was running.");

                        Console.WriteLine("Press any key to exit...");
                        Console.ReadKey();

                        serviceHost.Close();
                        logger.Info($"Service {uri} was stopped.");

                    }
                    catch (Exception ex)
                    {
                        logger.Error($"The following exception was thrown: '{ex.Message}'. Stack trace: '{ex.StackTrace}'.");

                        Console.WriteLine("Press any key to exit...");
                        Console.ReadKey();
                    }
                }
            }
        }
    }
}
