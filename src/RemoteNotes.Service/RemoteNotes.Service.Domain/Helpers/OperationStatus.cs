using System.Runtime.Serialization;

namespace RemoteNotes.Service.Domain.Helpers
{
    [DataContract]
    public enum OperationStatus
    {
        [EnumMember]
        Success,

        [EnumMember]
        Fail
    }
}