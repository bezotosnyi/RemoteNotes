using System;

namespace RemoteNotes.DAL.Core.Entities
{
    public class Note : BaseEntity
    {
        public string Title { get; set; }

        public int AccountId { get; set; }
        public Account Account { get; set; }

        public DateTime PublishTime { get; set; }

        public DateTime ModifyTime { get; set; }

        public byte[] Image { get; set; }

        public string Text { get; set; }
    }
}