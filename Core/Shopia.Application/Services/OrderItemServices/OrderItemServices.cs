using Shopia.Application.DTO_s.OrderItemDTO_s;
using Shopia.Application.Interface;
using Shopia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopia.Application.Services.OrderItemServices
{
    public class OrderItemServices : IOrderItemServices
    {
        private readonly IRepository<OrderItem> _repository;

        public OrderItemServices(IRepository<OrderItem> repository)
        {
            _repository = repository;
        }

        public async Task CreateOrderItemAsync(CreateOrderItemDTO model)
        {
            await _repository.CreateAsync(new OrderItem
            {
                //OrderId = model.OrderId,
                ProductId = model.ProductId,
                Quantity = model.Quantity,
                TotalPrice = model.TotalPrice
            });
        }

        public async Task DeleteOrderItemAsync(int id)
        {
            var values = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(values);
        }

        public async Task<List<ResultOrderItemDTO>> GetAllOrderItemAsync()
        {
            var values = await _repository.GetAllAsync();
            return values.Select(x => new ResultOrderItemDTO
            {
                OrderItemId = x.OrderItemId,
                OrderId = x.OrderId,
                ProductId = x.ProductId,
                Quantity = x.Quantity,
                TotalPrice = x.TotalPrice
            }).ToList();
        }

        public async Task<GetByIdOrderItemDTO> GetByIdOrderItemAsync(int id)
        {
            var values = await _repository.GetByIdAsync(id);
            var result = new GetByIdOrderItemDTO
            {
                OrderItemId = values.OrderItemId,
                OrderId = values.OrderId,
                ProductId = values.ProductId,
                Quantity = values.Quantity,
                TotalPrice = values.TotalPrice
            };
            return result;
        }

        public async Task UpdateOrderItemAsync(UpdateOrderItemDTO model)
        {
            var values = await _repository.GetByIdAsync(model.OrderItemId);
            //values.OrderId = model.OrderId;
            values.ProductId = model.ProductId;
            values.Quantity = model.Quantity;
            values.TotalPrice = model.TotalPrice;
            await _repository.UpdateAsync(values);

        }
    }
}
