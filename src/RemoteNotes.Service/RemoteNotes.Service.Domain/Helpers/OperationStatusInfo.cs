namespace RemoteNotes.Service.Domain.Helpers
{
    public class OperationStatusInfo
    {
        public OperationStatus OperationStatus { get; set; }

        public string AttachedInfo { get; set; }

        public object AttachedObject { get; set; }
    }
}