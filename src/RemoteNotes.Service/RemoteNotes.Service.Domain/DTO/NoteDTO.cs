using System;
using System.Runtime.Serialization;

namespace RemoteNotes.Service.Domain.DTO
{
    [DataContract]
    public class NoteDTO : BaseEntityDTO
    {
        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public AccountDTO Account { get; set; }

        [DataMember]
        public DateTime PublishTime { get; set; }

        [DataMember]
        public DateTime ModifyTime { get; set; }

        [DataMember]
        public byte[] Image { get; set; }

        [DataMember]
        public string Text { get; set; }
    }
}
