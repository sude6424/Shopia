using Microsoft.EntityFrameworkCore;
using Shopia.Application.DTO_s.CityDTO_s;
using Shopia.Application.DTO_s.TownDTO_s;
using Shopia.Application.Interface.IOrderRepository;
using Shopia.DataAccess.Context;
using Shopia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopia.DataAccess.Repositories.OrdersRepository
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly AppDbContext _context;

        public OrdersRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<City>> GetCity()
        {
            var cities= await _context.City.ToListAsync();
            return cities;
        }

        public async Task<List<Town>> GetTownByCityId(int cityid)
        {
            var town = await _context.Town.Where(x => x.CityId == cityid).ToListAsync();
            return town;
        }

        
    }
}
