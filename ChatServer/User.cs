﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime DatOfBirth { get; set; }
        public string Email { get; set; }

        
    }
}