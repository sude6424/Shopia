using Shopia.Application.DTO_s.CartDTO_s;
using Shopia.Application.DTO_s.CartItemDTO_s;
using Shopia.Application.Interface;
using Shopia.Application.Interface.ICartRepository;
using Shopia.Domain.Entities;
using System.Linq.Expressions;

namespace Shopia.Application.Services.CartServices
{
    public class CartServices : ICartServices
    {
        private readonly IRepository<Cart> _repository;
        private readonly IRepository<CartItem> _itemrepository;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly ICartsRepository _cartRepository;
        private readonly IUserIdentityRepository _userIdentityRepository;

        public CartServices(IRepository<Cart> repository, IRepository<CartItem> itemrepository, IRepository<Customer> customerRepository, IRepository<Product> productRepository, ICartsRepository cartRepository, IUserIdentityRepository userIdentityRepository)
        {
            _repository = repository;
            _itemrepository = itemrepository;
            _customerRepository = customerRepository;
            _productRepository = productRepository;
            _cartRepository = cartRepository;
            _userIdentityRepository = userIdentityRepository;
        }

        public async Task<bool> CheckCartAsync(string userId)
        {
            Expression<Func<Cart, bool>> filter = x => x.UserId == userId;
            var result = await _repository.FirstOrDefaultAsync(filter);
            if (result == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task CreateCartAsync(CreateCartDTO model)
        {
            var cart = new Cart
            {
                //TotalAmount = model.TotalAmount,
                CreatedDate = DateTime.Now,
                CustomerId = model.CustomerId,
                UserId = model.UserId,
            };
            await _repository.CreateAsync(cart);
            var sum = 0;
            foreach (var item in model.CartItems)
            {
                var cartItem = new CartItem
                {
                    CartId = cart.CartId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    TotalPrice = item.TotalPrice,
                };
                sum = sum + (item.TotalPrice);
                await _itemrepository.CreateAsync(cartItem);

            }
            cart.TotalAmount = sum;
            await _repository.UpdateAsync(cart);
        }

        public async Task DeleteCartAsync(int id)
        {
            var cart = await _repository.GetByIdAsync(id);
            var cartItems = await _itemrepository.GetAllAsync();
            foreach (var item in cartItems)
            {
                if (item.CartId == id)
                {
                    var Cartitem = await _itemrepository.GetByIdAsync(item.CartItemId);
                    await _itemrepository.DeleteAsync(Cartitem);
                }
            }
            await _repository.DeleteAsync(cart);
        }

        public async Task<List<ResultCartDTO>> GetAllCartAsync()
        {
            var carts = await _repository.GetAllAsync();
            var cartItems = await _itemrepository.GetAllAsync();
            var products = await _productRepository.GetAllAsync();
            var result = new List<ResultCartDTO>();
            foreach (var item in carts)
            {
                var customerdto = await _customerRepository.GetByFilterAsync(cus => cus.CustomerId == item.CustomerId);
                var cartdto = new ResultCartDTO
                {
                    CartId = item.CartId,
                    TotalAmount = item.TotalAmount,
                    Customer = customerdto,
                    CreatedDate = item.CreatedDate,
                    CustomerId = item.CustomerId,
                    UserId = item.UserId,
                    CartItems = new List<ResultCartItemDTO>()

                };
                foreach (var item1 in item.CartItems)
                {
                    var productdto = await _productRepository.GetByFilterAsync(prd => prd.ProductId == item1.ProductId);
                    var cartItemdto = new ResultCartItemDTO
                    {
                        CartItemId = item1.CartItemId,
                        CartId = item1.CartId,
                        ProductId = item1.ProductId,
                        Product = productdto,
                        Quantity = item1.Quantity,
                        TotalPrice = item1.TotalPrice
                    };
                    cartdto.CartItems.Add(cartItemdto);

                }
                result.Add(cartdto);
            }
            return result;
        }

        public async Task<GetByIdCartDTO> GetByIdCartAsync(int id)
        {
            var cart = await _repository.GetByIdAsync(id);
            if (cart != null)
            {
                var cartItems = await _itemrepository.GetAllAsync();
                var customer = await _customerRepository.GetByIdAsync(id);

                var result = new GetByIdCartDTO
                {
                    CartId = cart.CartId,
                    CartItems = new List<ResultCartItemDTO>(),
                    CreatedDate = cart.CreatedDate,
                    CustomerId = cart.CustomerId,
                    Customer = customer,
                    TotalAmount = cart.TotalAmount,
                    UserId = cart.UserId,


                };
                if (cart.CartItems != null)
                {
                    foreach (var item1 in cart.CartItems)
                    {
                        var productdto = await _productRepository.GetByFilterAsync(prd => prd.ProductId == item1.ProductId);
                        var cartItemdto = new ResultCartItemDTO
                        {
                            CartItemId = item1.CartItemId,
                            CartId = item1.CartId,
                            ProductId = item1.ProductId,
                            Product = productdto,
                            Quantity = item1.Quantity,
                            TotalPrice = item1.TotalPrice
                        };
                        result.CartItems.Add(cartItemdto);

                    }
                }

                return result;
            }
            else
            {
                return new GetByIdCartDTO { };
            }

        }

        public async Task<GetByIdCartDTO> GetByUserIdCartAsync(string userId)
        {
            Expression<Func<Cart, bool>> filter = x => x.UserId == userId;
            var cart = await _repository.FirstOrDefaultAsync(filter);
            if (cart != null)
            {
                var cartItems = await _itemrepository.GetAllAsync();
                var customer = await _customerRepository.GetByIdAsync(1);

                var result = new GetByIdCartDTO
                {
                    CartId = cart.CartId,
                    CartItems = new List<ResultCartItemDTO>(),
                    CreatedDate = cart.CreatedDate,
                    CustomerId = cart.CustomerId,
                    Customer = customer,
                    TotalAmount = cart.TotalAmount,
                    UserId = cart.UserId,


                };
                if (cart.CartItems != null)
                {
                    foreach (var item1 in cart.CartItems)
                    {
                        var productdto = await _productRepository.GetByFilterAsync(prd => prd.ProductId == item1.ProductId);
                        var cartItemdto = new ResultCartItemDTO
                        {
                            CartItemId = item1.CartItemId,
                            CartId = item1.CartId,
                            ProductId = item1.ProductId,
                            Product = productdto,
                            Quantity = item1.Quantity,
                            TotalPrice = item1.TotalPrice
                        };
                        result.CartItems.Add(cartItemdto);

                    }
                }
                return result;

            }
            else
            {
                return new GetByIdCartDTO { };
            }


        }

        public async Task UpdateCartAsync(UpdateCartDTO model)
        {
            var cart = await _repository.GetByIdAsync(model.CartId);
            var cartItems = await _itemrepository.GetAllAsync();
            //cart.TotalAmount = model.TotalAmount;
            //cart.CustomerId = model.CustomerId;
            //cart.CreatedDate = model.CreatedDate;
            var sum = 0;
            foreach (var item1 in model.CartItems)
            {
                foreach (var item in cart.CartItems)
                {
                    var cartItem = await _itemrepository.GetByIdAsync(item.CartItemId);
                    if (item.CartItemId == item1.CartItemId)
                    {
                        cartItem.Quantity = item1.Quantity;
                        cartItem.TotalPrice = item1.TotalPrice;

                    }
                    sum = sum + item.TotalPrice;
                }

            }
            cart.TotalAmount = sum;
            await _repository.UpdateAsync(cart);
        }

        public async Task UpdateTotalAmount(int cartId, decimal totalAmount)
        {


            await _cartRepository.UpdateTotalAmountAsync(cartId, totalAmount);

        }
    }
}
