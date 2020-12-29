using System.Runtime.Serialization;

namespace RemoteNotes.Service.Domain.Helpers
{
    [DataContract]
    public class OperationStatusInfo<T>
    {
        [DataMember]
        public OperationStatus OperationStatus { get; set; }

        [DataMember]
        public string AttachedInfo { get; set; }

        [DataMember]
        public T AttachedObject { get; set; }
    }
}