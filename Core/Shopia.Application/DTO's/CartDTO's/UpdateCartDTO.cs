using Shopia.Application.DTO_s.CartItemDTO_s;
using Shopia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopia.Application.DTO_s.CartDTO_s
{
    public class UpdateCartDTO
    {
        public int CartId { get; set; }
        //public decimal TotalAmount { get; set; }
        //public DateTime CreatedDate { get; set; }
        //public int CustomerId { get; set; }
        //public Customer? Customer { get; set; }
        public ICollection<UpdateCartItemDTO> CartItems { get; set; }

    }
}
