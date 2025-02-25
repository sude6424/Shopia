using Shopia.Application.DTO_s.CityDTO_s;
using Shopia.Application.DTO_s.OrderDTO_s;
using Shopia.Application.DTO_s.TownDTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopia.Application.Services.OrderServices
{
    public interface IOrderServices
    {
        Task<List<ResultOrderDTO>> GetAllOrderAsync();
        Task<GetByIdOrderDTO> GetByIdOrderAsync(int id);
        Task CreateOrderAsync(CreateOrderDTO model);
        Task UpdateOrderAsync(UpdateOrderDTO model);
        Task DeleteOrderAsync(int id);
        Task<List<ResultCityDTO>> GetAllCity();
        Task<List<ResultTownDTO>> GetTownByCityId(int cityid);
        Task<List<ResultOrderDTO>> GetOrderByUserId(string userId);

    }
}
