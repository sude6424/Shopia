using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopia.Application.Interface.ICartRepository
{
    public interface ICartsRepository
    {
        Task UpdateTotalAmountAsync(int cartId, decimal totalPrice);

    }
}
