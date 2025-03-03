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
    public class FieldRepository : GenericRepository<Field, ApplicationDbContext>, IFieldRepository
    {
        public FieldRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<Field>> GetAllWithWorksAsync(Expression<Func<Field, bool>> filter)
        {
            return await _context.Fields.Where(filter).Include(x => x.Works).ToListAsync();
        }

        public async Task<Field> GetFieldWithCategoriesWithWorksAsync(int id)
        {
            return await _context.Fields.Include(x => x.Categories).Include(x => x.Works).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
