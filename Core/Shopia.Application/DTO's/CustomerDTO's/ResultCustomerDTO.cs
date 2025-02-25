using Shopia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopia.Application.DTO_s.CustomerDTO_s
{
    public class ResultCustomerDTO
    {
        public int CustomerId { get; set; }
        public string Firstname { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }
        public string PhoneNumber { get; set; }


        //public ICollection<Order> Orders { get; set; }
    }
}
