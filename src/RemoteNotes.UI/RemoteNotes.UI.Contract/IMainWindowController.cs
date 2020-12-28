namespace RemoteNotes.UI.Contract
{
    public interface IMainWindowController
    {
        void RegisterControls(IControlFactory controlFactory);

        void LoadLogin();
        void LoadRegister();
        void LoadDashboard();

        void Exit();
    }
}