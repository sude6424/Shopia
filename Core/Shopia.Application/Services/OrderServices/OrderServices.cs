using Shopia.Application.DTO_s.CityDTO_s;
using Shopia.Application.DTO_s.OrderDTO_s;
using Shopia.Application.DTO_s.OrderItemDTO_s;
using Shopia.Application.DTO_s.TownDTO_s;
using Shopia.Application.Interface;
using Shopia.Application.Interface.IOrderRepository;
using Shopia.Application.Services.CategoryServices;
using Shopia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopia.Application.Services.OrderServices
{
    public class OrderServices : IOrderServices
    {
        private readonly IRepository<Order> _repository;
        private readonly IRepository<OrderItem> _repositoryOrderItem;
        private readonly IRepository<Customer> _repositoryCustomer;
        private readonly IRepository<Product> _repositoryProduct;
        private readonly IRepository<Category> _repositoryCategory;
        private readonly IRepository<CartItem> _repositoryCartItem;
        private readonly IOrdersRepository _ordersRepository;

        public OrderServices(IRepository<Order> repository, IRepository<OrderItem> repositoryOrderItem, IRepository<Customer> repositoryCustomer, IRepository<Product> repositoryProduct, IRepository<Category> repositoryCategory, IRepository<CartItem> repositoryCartItem, IOrdersRepository ordersRepository)
        {
            _repository = repository;
            _repositoryOrderItem = repositoryOrderItem;
            _repositoryCustomer = repositoryCustomer;
            _repositoryProduct = repositoryProduct;
            _repositoryCategory = repositoryCategory;
            _repositoryCartItem = repositoryCartItem;
            _ordersRepository = ordersRepository;
        }
        public async Task CreateOrderAsync(CreateOrderDTO model)
        {

            decimal sum = 0;
            var order = new Order
            {
                OrderDate = DateTime.Now,
                TotalAmount = sum,
                OrderStatus = model.OrderStatus,
                //BillingAddress = model.BillingAddress,
                ShippingAddress = model.ShippingAddress,
                ShippingCityId = model.ShippingCityId,
                ShippingTownId = model.ShippingTownId,
                //PaymentMethod = model.PaymentMethod,
                CustomerId = model.CustomerId,
                CustomerName = model.CustomerName,
                CustomerSurname = model.CustomerSurname,
                CustomerEmail = model.CustomerEmail,
                CustomerPhone = model.CustomerPhone,
                UserId=model.UserId,
            };
            await _repository.CreateAsync(order);



            foreach (var item in model.OrderItems)
            {
                await _repositoryOrderItem.CreateAsync(new OrderItem
                {
                    OrderId = order.OrderId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    TotalPrice = item.TotalPrice
                });
                sum = sum + item.TotalPrice;
            }
            order.TotalAmount = sum;
            await _repository.UpdateAsync(order);

        }

        public async Task DeleteOrderAsync(int id)
        {
            var values = await _repository.GetByIdAsync(id);
            foreach (var item in values.OrderItems)
            {
                var orderItem = await _repositoryOrderItem.GetByIdAsync(item.OrderItemId);
                await _repositoryOrderItem.DeleteAsync(orderItem);
            }
            await _repository.DeleteAsync(values);
        }

        public async Task<List<ResultCityDTO>> GetAllCity()
        {
            var values = await _ordersRepository.GetCity();
            return values.Select(x => new ResultCityDTO
            {
                Id = x.Id,
                CityId = x.CityId,
                CityName = x.CityName
            }).ToList();
        }

        public async Task<List<ResultOrderDTO>> GetAllOrderAsync()
        {
            var values = await _repository.GetAllAsync();
            var OrderItem = await _repositoryOrderItem.GetAllAsync();
            var result = new List<ResultOrderDTO>();



            foreach (var item in values)
            {
                var orderCustomer = await _repositoryCustomer.GetByIdAsync(item.CustomerId);
                var orderDTO = new ResultOrderDTO
                {
                    OrderId = item.OrderId,
                    OrderDate = item.OrderDate,
                    TotalAmount = item.TotalAmount,
                    OrderStatus = item.OrderStatus,
                    ShippingAddress = item.ShippingAddress,
                    ShippingCityId = item.ShippingCityId,
                    ShippingTownId = item.ShippingTownId,

                    CustomerId = item.CustomerId,
                    CustomerName = item.CustomerName,
                    CustomerSurname = item.CustomerSurname,
                    CustomerEmail = item.CustomerEmail,
                    CustomerPhone = item.CustomerPhone,
                    OrderItems = new List<ResultOrderItemDTO>(),
                    UserId = item.UserId,

                };


                foreach (var item1 in item.OrderItems)
                {
                    var orderItemProduct = await _repositoryProduct.GetByIdAsync(item1.ProductId);
                    var orderItemDTO = new ResultOrderItemDTO
                    {
                        OrderItemId = item1.OrderItemId,
                        OrderId = item1.OrderId,
                        ProductId = item1.ProductId,
                        Quantity = item1.Quantity,
                        TotalPrice = item1.TotalPrice,
                        Product = orderItemProduct,
                    };
                    orderDTO.OrderItems.Add(orderItemDTO);
                }


                result.Add(orderDTO);
            }

            return result;
        }


        public async Task<GetByIdOrderDTO> GetByIdOrderAsync(int id)
        {
            var values = await _repository.GetByIdAsync(id);
            var ordercustomer = await _repositoryCustomer.GetByIdAsync(values.CustomerId);
            var orderitemsrepo= await _repositoryOrderItem.WhereAsync(x=>x.OrderId == id);
            var result = new GetByIdOrderDTO
            {
                OrderId = values.OrderId,
                OrderDate = values.OrderDate,
                TotalAmount = values.TotalAmount,
                OrderStatus = values.OrderStatus,
                //BillingAddress = values.BillingAddress,
                ShippingAddress = values.ShippingAddress,
                ShippingCityId = values.ShippingCityId,
                ShippingTownId = values.ShippingTownId,
                //PaymentMethod = values.PaymentMethod,
                CustomerId = values.CustomerId,
                CustomerName = values.CustomerName,
                CustomerSurname = values.CustomerSurname,
                CustomerEmail = values.CustomerEmail,
                CustomerPhone = values.CustomerPhone,
                OrderItems = new List<ResultOrderItemDTO>(),
                UserId= values.UserId,
            };
            foreach (var item in orderitemsrepo)
            {
                var orderıtemproduct = await _repositoryProduct.GetByIdAsync(item.ProductId);
                var orderItemDTO = new ResultOrderItemDTO
                {
                    OrderItemId = item.OrderItemId,
                    OrderId = item.OrderId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    TotalPrice = item.TotalPrice,
                    Product = orderıtemproduct,
                };
                result.OrderItems.Add(orderItemDTO);

            }
            return result;
        }

        public async Task<List<ResultOrderDTO>> GetOrderByUserId(string userId)
        {
            var values =await _repository.WhereAsync(x=>x.UserId == userId);
            var orderItem=await _repositoryOrderItem.GetAllAsync();
            var result = new List<ResultOrderDTO>();
            foreach (var item in values)
            {
                var ordercustomer= await _repositoryCustomer.GetByIdAsync(item.CustomerId);
                var orderDTO = new ResultOrderDTO
                {
                    OrderId = item.OrderId,
                    OrderDate = item.OrderDate,
                    TotalAmount = item.TotalAmount,
                    OrderStatus = item.OrderStatus,
                    //BillingAddress = values.BillingAddress,
                    ShippingAddress = item.ShippingAddress,
                    ShippingCityId = item.ShippingCityId,
                    ShippingTownId = item.ShippingTownId,
                    //PaymentMethod = values.PaymentMethod,
                    CustomerId = item.CustomerId,
                    CustomerName = item.CustomerName,
                    CustomerSurname = item.CustomerSurname,
                    CustomerEmail = item.CustomerEmail,
                    CustomerPhone = item.CustomerPhone,
                    OrderItems = new List<ResultOrderItemDTO>(),
                    UserId = item.UserId,
                };
                foreach (var item1 in item.OrderItems)
                {
                    var orderItemProduct = await _repositoryProduct.GetByIdAsync(item1.ProductId);
                    var orderItemDTO = new ResultOrderItemDTO
                    {
                        OrderId = item1.OrderId,
                        ProductId = item1.ProductId,
                        Quantity = item1.Quantity,
                        TotalPrice = item1.TotalPrice,
                        OrderItemId = item1.OrderItemId,
                        Product = orderItemProduct,
                    };
                    orderDTO.OrderItems.Add(orderItemDTO);
                }
                result.Add(orderDTO);
            }
            return result;
        }

        public async Task<List<ResultTownDTO>> GetTownByCityId(int cityid)
        {
            var values = await _ordersRepository.GetTownByCityId(cityid);
            return values.Select(x => new ResultTownDTO
            {
                Id = x.Id,
                CityId = x.CityId,
                TownId = x.TownId,
                TownName = x.TownName
            }).ToList();
        }

        public async Task UpdateOrderAsync(UpdateOrderDTO model)
        {

            var values = await _repository.GetByIdAsync(model.OrderId);
            var orderItems = await _repositoryOrderItem.GetAllAsync();
            values.OrderStatus = model.OrderStatus;
            decimal sum = 0;
            foreach (var item in model.OrderItems)
            {
                foreach (var item1 in values.OrderItems)
                {
                    var orderItemDTO = await _repositoryOrderItem.GetByIdAsync(item1.OrderItemId);
                    if (item.OrderItemId == item1.OrderItemId)
                    {
                        orderItemDTO.TotalPrice = item1.TotalPrice;
                        orderItemDTO.Quantity = item1.Quantity;
                    }
                    sum = sum + item1.TotalPrice;
                }
            }
            values.TotalAmount = sum;

            await _repository.UpdateAsync(values);
        }
    }
}
