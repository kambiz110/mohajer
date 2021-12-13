using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mohajer.Entity
{
    public class User :BaseEntity
    {
        public string Password { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Role{ get; set; }
        public string Image { get; set; }

        public bool IsWorkUser { get; set; }

        public string Token { get; set; }
    }
}
