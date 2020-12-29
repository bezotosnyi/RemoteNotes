using System.Configuration;
using System.Windows;
using RemoteNotes.Service.Client;
using RemoteNotes.Service.Client.Front;
using RemoteNotes.Service.Client.Stub;
using RemoteNotes.UI.Contract;
using RemoteNotes.UI.Control;
using RemoteNotes.UI.ViewModel;

namespace RemoteNotes.UI.Shell
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application, IApplication
    {
        private void AppStartUp(object sender, StartupEventArgs args)
        {
            var mainWindow = new MainWindow();
            var controlManager = new ControlManager();
            var mainWindowController = new MainWindowController(mainWindow, controlManager, this);

            // var frontServiceClient = new FrontServiceClientStub();
            var remoteNotesClient = new RemoteNotesClient(ConfigurationManager.AppSettings["uri"]);
            var frontServiceClient = new FrontServiceClient(remoteNotesClient);

            var viewModelFactory = new ViewModelFactory(mainWindowController, frontServiceClient);
            var controlFactory = new ControlFactory(viewModelFactory);

            mainWindowController.RegisterControls(controlFactory);
            mainWindowController.LoadLogin();
        }

        private void AppExit(object sender, ExitEventArgs args)
        {
            Shutdown();
        }

        void IApplication.Exit()
        {
            Shutdown();
        }
    }
}