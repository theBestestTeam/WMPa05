using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain
{
    public class Messages
    {
        public DateTime TimeSent { get; set; }
        public string FromUserId { get; set; }
        public string ToUserId { get; set; }
        public string MessageSent { get; set; }
    }
}
