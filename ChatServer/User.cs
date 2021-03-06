﻿using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    public class User
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public override string ToString()
        {
            return UserName;
        }
        public User()
        {
            UserId = Guid.NewGuid().ToString();
        }
    }
}
