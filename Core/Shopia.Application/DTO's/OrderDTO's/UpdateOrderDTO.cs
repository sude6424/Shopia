using Shopia.Application.DTO_s.OrderItemDTO_s;
using Shopia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopia.Application.DTO_s.OrderDTO_s
{
    public class UpdateOrderDTO
    {

        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string OrderStatus { get; set; }
        //public string BillingAddress { get; set; }
        public string ShippingAddress { get; set; }
        public int ShippingCityId { get; set; }

        public int ShippingTownId { get; set; }
        //public string PaymentMethod { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerSurname { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public string UserId { get; set; }

        public List<ResultOrderItemDTO> OrderItems { get; set; }
    }
}

