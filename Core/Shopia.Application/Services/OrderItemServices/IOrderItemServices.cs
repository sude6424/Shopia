using Shopia.Application.DTO_s.OrderItemDTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopia.Application.Services.OrderItemServices
{
    public interface IOrderItemServices
    {
        Task<List<ResultOrderItemDTO>> GetAllOrderItemAsync();
        Task<GetByIdOrderItemDTO> GetByIdOrderItemAsync(int id);
        Task CreateOrderItemAsync(CreateOrderItemDTO model);
        Task UpdateOrderItemAsync(UpdateOrderItemDTO model);
        Task DeleteOrderItemAsync(int id);
    }
}
