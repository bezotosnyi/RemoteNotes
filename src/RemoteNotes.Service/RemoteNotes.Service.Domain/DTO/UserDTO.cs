using System.Runtime.Serialization;

namespace RemoteNotes.Service.Domain.DTO
{
    [DataContract]
    public class UserDTO : BaseEntityDTO
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public AccountDTO Account { get; set; }

        [DataMember]
        public string Login { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public bool IsActive { get; set; }
    }
}
