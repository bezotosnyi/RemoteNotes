using System;
using System.Configuration;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
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
                using (var serviceHost = new WebServiceHost(typeof(RemoteNotesService)))
                {
                    var serviceEndpoint =
                        serviceHost.AddServiceEndpoint(typeof(IRemoteNotesService), new WebHttpBinding(), uri);
                    serviceEndpoint.Behaviors.Add(new WebHttpBehavior());

                    try
                    {
                        serviceHost.AddDependencyInjectionBehavior<IRemoteNotesService>(container);
                        serviceHost.Open();

                        var message = $"Service {uri} was running.";
                        Console.WriteLine(message);

                        Console.WriteLine("Press any key to exit...");
                        Console.ReadKey();

                        serviceHost.Close();
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
