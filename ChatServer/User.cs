using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    class User
    {
        string UserName { get; set; }
        string Password { get; set; }
        DateTime DatOfBirth { get; set; }
        string Email { get; set; }

        public User(string UserName, string Password, DateTime DatOfBirth, string Email)
        {
            this.UserName = UserName;
            this.Password = Password;
            this.DatOfBirth = DatOfBirth;
            this.Email = Email;
        }
    }
}
