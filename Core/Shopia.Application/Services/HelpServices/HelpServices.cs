using Shopia.Application.DTO_s.HelpDTO_s;
using Shopia.Application.Interface;
using Shopia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopia.Application.Services.HelpServices
{
    public class HelpServices : IHelpServices
    {
        private readonly IRepository<Help> _repository;

        public HelpServices(IRepository<Help> repository)
        {
            _repository = repository;
        }

        public async Task CreateHelpAsync(CreateHelpDTO model)
        {
            await _repository.CreateAsync(new Help
            {
                Name = model.Name,
                Email = model.Email,
                Subject = model.Subject,
                Message = model.Message,
                CreatedDate = model.CreatedDate,
                Status = model.Status,
            });
        }

        public async Task DeleteHelpAsync(int id)
        {
            var help = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(help);
        }

        public async Task<List<ResultHelpDTO>> GetAllHelpAsync()
        {
            var helps = await _repository.GetAllAsync();
            return helps.Select(x => new ResultHelpDTO
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                Subject = x.Subject,
                Message = x.Message,
                CreatedDate = x.CreatedDate,
                Status = x.Status,
            }).ToList();
        }

        public async Task<GetByIdHelpDTO> GetByIdHelpAsync(int id)
        {
            var help = await _repository.GetByIdAsync(id);
            var newhelp = new GetByIdHelpDTO
            {
                Id = help.Id,
                Name = help.Name,
                Email = help.Email,
                Subject = help.Subject,
                Message = help.Message,
                CreatedDate = help.CreatedDate,
                Status = help.Status,
            };
            return newhelp;
        }

        public async Task UpdateHelpAsync(UpdateHelpDTO model)
        {
            var help = await _repository.GetByIdAsync(model.Id);
            help.Name = model.Name;
            help.Email = model.Email;
            help.Subject = model.Subject;
            help.Message = model.Message;
            help.CreatedDate = model.CreatedDate;
            help.Status = model.Status;
            await _repository.UpdateAsync(help);
        }
    }
}
