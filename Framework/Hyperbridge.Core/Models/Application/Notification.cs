using System;

namespace Blockhub
{
    public class Notification
    {
        public string Subject { get; set; }
        public string Text { get; set; }
        public string Type { get; set; }
        public DateTime TimeStamp { get; set; }

        public bool HasBeenShown { get; set; } = false;
    }
}
