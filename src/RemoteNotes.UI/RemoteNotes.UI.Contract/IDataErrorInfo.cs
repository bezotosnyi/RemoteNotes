namespace RemoteNotes.UI.Contract
{
    public interface IDataErrorInfo
    {
        string Error { get; }
        string this[string propertyName] { get; }
    }
}