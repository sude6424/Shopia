using Shopia.Application.DTO_s.CartDTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopia.Application.Services.CartServices
{
    public interface ICartServices
    {
        Task<List<ResultCartDTO>> GetAllCartAsync();
        Task<GetByIdCartDTO> GetByIdCartAsync(int id);
        Task CreateCartAsync(CreateCartDTO model);
        Task UpdateCartAsync(UpdateCartDTO model);
        Task DeleteCartAsync(int id);
        Task UpdateTotalAmount(int cartId, decimal totalAmount);
        Task<GetByIdCartDTO> GetByUserIdCartAsync(string userId);
        Task<bool> CheckCartAsync(string userId);
    }
}
