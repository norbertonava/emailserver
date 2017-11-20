using System.Collections.Generic;

namespace EmailServer.Core
{
    class Message2
    {
        public string PhoneNumber { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public List<byte[]> Attachments { get; set; }
    }
}
