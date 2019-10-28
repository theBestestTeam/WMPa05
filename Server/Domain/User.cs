using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain
{
    public class User
    {
         //User information
        public User()
        {
            UserId = Guid.NewGuid().ToString().Split('-')[4];
        }
        public string UserId { get; set; }
        public string Name { get; set; }
        public DateTime TimeCreated { get; set; }
        public ObservableCollection<Messages> UserMessages { get; set; }
    }
}
