using System.Runtime.Serialization;

namespace RemoteNotes.Service.Domain.DTO
{
    [DataContract]
    public class BaseEntityDTO
    {
        [DataMember]
        public int Id { get; set; }
    }
}