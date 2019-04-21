using System;

namespace ChatServer
{
    public class Message
    {
        public string Text { get; set; }
        public DateTime SentDateTime { get; set; }
        public string Username { get; set; }
    }
}
