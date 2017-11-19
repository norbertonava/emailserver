using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
