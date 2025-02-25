using Shopia.Application.DTO_s.CartItemDTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopia.Application.Services.CartItemServices
{
    public interface ICartItemServices
    {
        Task<List<ResultCartItemDTO>> GetAllCartItemAsync();
        Task<GetByIdCartItemDTO> GetByIdCartItemAsync(int id);
        Task CreateCartItemAsync(CreateCartItemDTO model);
        Task UpdateCartItemAsync(UpdateCartItemDTO model);
        Task DeleteCartItemAsync(int id);
        Task<List<ResultCartItemDTO>> GetCartItemByCartIdAsync(int cartId);
        Task UpdateQuantity(int cartId, int productId, int quantity);
        Task UpdateQuantityOnCart(UpdateCartItemDTO dto);

        Task<bool> CheckCartItem(int cartId, int productId);
        Task<int> GetCountCartItemByCartId(string userId);

    }
}
