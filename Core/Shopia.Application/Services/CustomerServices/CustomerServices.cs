using Shopia.Application.DTO_s.CustomerDTO_s;
using Shopia.Application.Interface;
using Shopia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopia.Application.Services.CustomerServices
{
    public class CustomerServices : ICustomerServices
    {
        private readonly IRepository<Customer> _repository;

        public CustomerServices(IRepository<Customer> repository)
        {
            _repository = repository;
        }

        public async Task CreateCustomerAsync(CreateCustomerDTO createCustomerDTO)
        {
            await _repository.CreateAsync(new Customer
            {
                Firstname = createCustomerDTO.Firstname,
                LastName = createCustomerDTO.LastName,
                Email = createCustomerDTO.Email,
                UserId = createCustomerDTO.UserId,
                PhoneNumber = createCustomerDTO.PhoneNumber,
            });
        }

        public async Task DeleteCustomerAsync(int id)
        {
            var values= await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(values);
        }

        public async Task<List<ResultCustomerDTO>> GetAllCustomerAsync()
        {
            var values= await _repository.GetAllAsync();
            return values.Select(x => new ResultCustomerDTO
            {
                CustomerId = x.CustomerId,
                Firstname = x.Firstname,
                LastName = x.LastName,
                Email = x.Email,
                UserId = x.UserId,
                PhoneNumber = x.PhoneNumber,
                //Orders = x.Orders
            }).ToList();
        }

        public async Task<GetByIdCustomerDTO> GetByIdCustomerAsync(int id)
        {
            var values = await _repository.GetByIdAsync(id);
            return new GetByIdCustomerDTO
            {
                CustomerId = values.CustomerId,
                Firstname = values.Firstname,
                LastName = values.LastName,
                Email = values.Email,
                UserId = values.UserId,
                PhoneNumber = values.PhoneNumber,
                //Orders = values.Orders
            };
        }

        public async Task<GetByIdCustomerDTO> GetCustomerByUserId(string userid)
        {
            var customer = await _repository.FirstOrDefaultAsync(x => x.UserId == userid);
            return new GetByIdCustomerDTO
            {
                CustomerId = customer.CustomerId,
                Firstname = customer.Firstname,
                LastName = customer.LastName,
                Email = customer.Email,
                UserId = customer.UserId,
                PhoneNumber = customer.PhoneNumber,
            };
            }

        public async Task UpdateCustomerAsync(UpdateCustomerDTO updateCustomerDTO)
        {
            var values = await _repository.GetByIdAsync(updateCustomerDTO.CustomerId);
            values.Firstname = updateCustomerDTO.Firstname;
            values.LastName = updateCustomerDTO.LastName;
            values.Email = updateCustomerDTO.Email;
            values.PhoneNumber = updateCustomerDTO.PhoneNumber;
            await _repository.UpdateAsync(values);
        }
    }
}
