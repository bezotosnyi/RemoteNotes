using System.Windows;
using RemoteNotes.UI.Contract;
using RemoteNotes.UI.Control;

namespace RemoteNotes.UI.Shell
{
    public class MainWindowController: IMainWindowController
    {
        private readonly Window _mainWindow;
        private readonly ControlManager _controlManager;
        private readonly IApplication _application;

        public MainWindowController(Window mainWindow, ControlManager controlManager, IApplication application)
        {
            _mainWindow = mainWindow;
            _controlManager = controlManager;
            _application = application;
        }

        public void RegisterControls(IControlFactory controlFactory)
        {
            _controlManager.Register("MainWindow", _mainWindow);
            _controlManager.Register("LoginControl", controlFactory.Create<LoginControl>());
        }

        public void LoadLogin()
        {
            _mainWindow.WindowStyle = WindowStyle.None;
            _mainWindow.WindowState = WindowState.Normal;
            _mainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            _mainWindow.Show();

            _controlManager.Place("MainWindow", "MainRegion", "LoginControl");
        }

        public void LoadRegister()
        {
            throw new System.NotImplementedException();
        }

        public void LoadDashboard()
        {
            throw new System.NotImplementedException();
        }

        public void Exit() => _application.Exit();
    }
}