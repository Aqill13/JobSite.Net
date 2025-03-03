using DataAccessLayer.Abstract;
using DataAccessLayer.Data;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class WorkRepository : GenericRepository<Work, ApplicationDbContext>, IWorkRepository
    {
        public WorkRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<Work>> GetWorksWithIncludesAsync(string[] includes, Expression<Func<Work, bool>> filter = null)
        {
            IQueryable<Work> query = _context.Works;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();
        }
    }
}
