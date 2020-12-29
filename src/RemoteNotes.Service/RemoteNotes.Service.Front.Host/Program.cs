using System;
using System.Configuration;
using System.ServiceModel;
using Autofac.Integration.Wcf;
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

                using (var host = new ServiceHost(typeof(RemoteNotesService), uri))
                {
                    try
                    {
                        host.AddDependencyInjectionBehavior<IRemoteNotesService>(container);
                        host.Open();

                        var message = $"Service {uri} was running.";
                        Console.WriteLine(message);

                        Console.WriteLine("Press any key to exit...");
                        Console.ReadKey();

                        host.Close();
                        Console.WriteLine($"Service {uri} was stopped.");
                    }
                    catch (Exception exception)
                    {
                        var message = $"The following exception was thrown: '{exception.Message}'. Stack trace: '{exception.StackTrace}'.";
                        Console.WriteLine(message);

                        Console.WriteLine("Press any key to exit...");
                        Console.ReadKey();
                    }
                }
            }
        }
    }
}
