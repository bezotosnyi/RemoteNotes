using System;

namespace RemoteNotes.DAL.Domain.Entities
{
    public class Note : BaseEntity
    {
        public int UserId { get; set; }
        public Account Account { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public byte[] Image { get; set; }

        public DateTime PublishTime { get; set; }

        public DateTime ModifyTime { get; set; }
    }
}