using Shopia.Application.DTO_s.CityDTO_s;
using Shopia.Application.DTO_s.TownDTO_s;
using Shopia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopia.Application.Interface.IOrderRepository
{
    public interface IOrdersRepository
    {
        Task<List<City>> GetCity();
        Task<List<Town>> GetTownByCityId(int cityid);
    }
}
