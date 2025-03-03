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
    public class FavoriteRepository : GenericRepository<Favorite, ApplicationDbContext>, IFavoriteRepository
    {
        public FavoriteRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<Favorite>> GetFavoritesByUserId(string id)
        {
            return await _context.Favorites.Include(x => x.Work).Where(x => x.UserId == id).ToListAsync();
        }
    }
}
