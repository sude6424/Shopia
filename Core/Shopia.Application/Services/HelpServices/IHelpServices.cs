using Shopia.Application.DTO_s.HelpDTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopia.Application.Services.HelpServices
{
    public interface IHelpServices
    {
        Task<List<ResultHelpDTO>> GetAllHelpAsync();
        Task<GetByIdHelpDTO> GetByIdHelpAsync(int id);
        Task CreateHelpAsync(CreateHelpDTO model);
        Task UpdateHelpAsync(UpdateHelpDTO model);
        Task DeleteHelpAsync(int id);
    }
}
