using Shopia.Application.DTO_s.CartItemDTO_s;
using Shopia.Application.Interface;
using Shopia.Application.Interface.ICartItemsRepository;
using Shopia.Application.Interface.ICartRepository;
using Shopia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopia.Application.Services.CartItemServices
{
    public class CartItemServices : ICartItemServices
    {
        private readonly IRepository<CartItem> _repository;
        private readonly ICartItemsRepository _cartItemRepository;
        private readonly IRepository<Cart> _cartRepository;


        public CartItemServices(IRepository<CartItem> repository, ICartItemsRepository cartItemRepository, IRepository<Cart> cartRepository)
        {
            _repository = repository;
            _cartItemRepository = cartItemRepository;
            _cartRepository = cartRepository;
        }

        public async Task<bool> CheckCartItem(int cartId, int productId)
        {
            var value = await _cartItemRepository.CheckCartItemAsync(cartId, productId);
            return value;
        }

        public async Task CreateCartItemAsync(CreateCartItemDTO model)
        {
            var cartItem = new CartItem
            {
                CartId = model.CartId,
                ProductId = model.ProductId,
                Quantity = model.Quantity,
                TotalPrice = model.TotalPrice
            };
            await _repository.CreateAsync(cartItem);
        }

        public async Task DeleteCartItemAsync(int id)
        {
            var cartItem = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(cartItem);
        }

        public async Task<List<ResultCartItemDTO>> GetAllCartItemAsync()
        {
            var cartItems = await _repository.GetAllAsync();
            return cartItems.Select(x => new ResultCartItemDTO
            {
                CartItemId = x.CartItemId,
                CartId = x.CartId,
                ProductId = x.ProductId,
                Quantity = x.Quantity,
                TotalPrice = x.TotalPrice
            }).ToList();
        }

        public async Task<GetByIdCartItemDTO> GetByIdCartItemAsync(int id)
        {
            var cartItem = await _repository.GetByIdAsync(id);
            return new GetByIdCartItemDTO
            {
                CartItemId = cartItem.CartItemId,
                CartId = cartItem.CartId,
                ProductId = cartItem.ProductId,
                Quantity = cartItem.Quantity,
                TotalPrice = cartItem.TotalPrice
            };
        }

        public Task<List<ResultCartItemDTO>> GetCartItemByCartIdAsync(int cartId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetCountCartItemByCartId(string userId)
        {
            var cartid = await _cartRepository.FirstOrDefaultAsync(x => x.UserId == userId);
            if (cartid==null)
            {
                return 0;
            }
            else
            {
                var cartItems = await _repository.WhereAsync(x => x.CartId == cartid.CartId);
                if (cartItems == null)
                {
                    return 0;
                }
                else
                {
                    return cartItems.Count();

                }
            }
            
        }

        public async Task UpdateCartItemAsync(UpdateCartItemDTO model)
        {
            var cartItem = await _repository.GetByIdAsync(model.CartItemId);
            //cartItem.CartId = model.CartId;
            cartItem.ProductId = model.ProductId;
            cartItem.Quantity = model.Quantity;
            cartItem.TotalPrice = model.TotalPrice;
            await _repository.UpdateAsync(cartItem);
        }

        public async Task UpdateQuantity(int cartId, int productId, int quantity)
        {
            await _cartItemRepository.UpdateQuantity(cartId, productId, quantity);
        }

        public async Task UpdateQuantityOnCart(UpdateCartItemDTO dto)
        {
            await _cartItemRepository.UpdateQuantityOnCartAsync(dto);
        }
    }
}
