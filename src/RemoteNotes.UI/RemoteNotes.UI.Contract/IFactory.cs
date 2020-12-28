namespace RemoteNotes.UI.Contract
{
    public interface IFactory
    {
        T Create<T>();
    }
}