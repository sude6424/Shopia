﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopia.Application.DTO_s.AccountDTO_s
{
    public class RegisterDTO
    {
        public string Name{ get; set; }
        public string Lastname{ get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
    }
}
