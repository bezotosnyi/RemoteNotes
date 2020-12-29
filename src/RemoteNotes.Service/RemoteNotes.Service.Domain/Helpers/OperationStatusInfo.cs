namespace RemoteNotes.Service.Domain.Helpers
{
    public class OperationStatusInfo<T>
    {
        public OperationStatus OperationStatus { get; set; }

        public string AttachedInfo { get; set; }

        public T AttachedObject { get; set; }
    }
}