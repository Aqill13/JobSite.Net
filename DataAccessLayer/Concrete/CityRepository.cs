using DataAccessLayer.Abstract;
using DataAccessLayer.Data;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class CityRepository : GenericRepository<City, ApplicationDbContext>, ICityRepository
    {
        public CityRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<City>> GetListOrderByAsync()
        {
            return await _context.Cities.OrderBy(x => x.Id).ToListAsync();
        }
    }
}
