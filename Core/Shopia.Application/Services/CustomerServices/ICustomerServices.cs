using Shopia.Application.DTO_s.CustomerDTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopia.Application.Services.CustomerServices
{
    public interface ICustomerServices
    {
        Task<List<ResultCustomerDTO>> GetAllCustomerAsync();
        Task<GetByIdCustomerDTO> GetByIdCustomerAsync(int id);
        Task CreateCustomerAsync(CreateCustomerDTO createCustomerDTO);
        Task UpdateCustomerAsync(UpdateCustomerDTO updateCustomerDTO);
        Task DeleteCustomerAsync(int id);
        Task<GetByIdCustomerDTO> GetCustomerByUserId(string userid);
    }
}
