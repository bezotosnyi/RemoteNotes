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
        private bool _mainWindowShown;

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
            _controlManager.Register("RegisterControl", controlFactory.Create<RegisterControl>());
            _controlManager.Register("DashboardControl", controlFactory.Create<DashboardControl>());
        }

        public void LoadLogin()
        {
            if (!_mainWindowShown)
            {
                _mainWindow.WindowState = WindowState.Normal;
                _mainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                _mainWindow.Show();
                _mainWindowShown = true;
            }

            _mainWindow.Height = 410;
            _controlManager.Place("MainWindow", "MainRegion", "LoginControl");
        }

        public void LoadRegister()
        {
            _mainWindow.Height = 660;
            _controlManager.Place("MainWindow", "MainRegion", "RegisterControl");
        }

        public void LoadDashboard()
        {
            _mainWindow.Height = 410;
            _controlManager.Place("MainWindow", "MainRegion", "DashboardControl");
        }

        public void Exit() => _application.Exit();
    }
}