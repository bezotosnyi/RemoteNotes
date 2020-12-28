namespace RemoteNotes.UI.Contract
{
    public interface IView
    {
        void SetFocus();
        void ClearError();
        void ShowError(string errorMessage);
    }
}