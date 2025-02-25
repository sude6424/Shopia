using Shopia.Application.DTO_s.SubscriberDTO_s;
using Shopia.Application.Interface;
using Shopia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopia.Application.Services.SubscriberServices
{
    public class SubscriberServices : ISubscriberServices
    {
        private readonly IRepository<Subscriber> _repository;

        public SubscriberServices(IRepository<Subscriber> repository)
        {
            _repository = repository;
        }

        public async Task CreateSubsricer(CreateSubscriberDTO dto)
        {
            var subscriber = new Subscriber()
            {
                Name = dto.Name,
                Email = dto.Email,
                SubscriberDate = dto.SubscriberDate
            };
            await _repository.CreateAsync(subscriber);
        }

        public async Task DeleteSubscriber(int id)
        {
            var subscriber = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(subscriber);
        }

        public async Task<List<ResultSubscriberDTO>> GetAllSubscribers()
        {
            var subscribers = await _repository.GetAllAsync();
            return subscribers.Select(x => new ResultSubscriberDTO
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                SubscriberDate = x.SubscriberDate,
            }).ToList();
        }

        public async Task<GetByIdSubscriberDTO> GetSubscriberById(int id)
        {
            var subscriber =await _repository.GetByIdAsync(id);
            var newSubscriber = new GetByIdSubscriberDTO
            {
                Id = subscriber.Id,
                Name = subscriber.Name,
                Email = subscriber.Email,
                SubscriberDate = subscriber.SubscriberDate
            };
            return newSubscriber;
        }

        public async Task UpdateSubscriber(UpdateSubscriberDTO dto)
        {
            var subscriber = await _repository.GetByIdAsync(dto.Id);
            subscriber.Name = dto.Name;
            subscriber.Email = dto.Email;
            subscriber.SubscriberDate = dto.SubscriberDate;
            await _repository.UpdateAsync(subscriber);
        }
    }
}
