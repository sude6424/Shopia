﻿using Shopia.Application.DTO_s.OrderDTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopia.Application.DTO_s.AccountDTO_s
{
    public class ResultProfileDTO
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public List<ResultOrderDTO> Orders { get; set; }
    }
}
