namespace RemoteNotes.UI.Contract
{
    public interface IViewModelFactory
    {
        T Create<T>(IView view);
    }
}