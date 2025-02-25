using Shopia.Application.DTO_s.CartItemDTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopia.Application.Interface.ICartItemsRepository
{
    public interface ICartItemsRepository
    {
        Task UpdateQuantity(int cartId, int productId, int quantity);
        Task UpdateQuantityOnCartAsync(UpdateCartItemDTO dto);
        Task<bool> CheckCartItemAsync(int cartId, int productId);
    }
}
