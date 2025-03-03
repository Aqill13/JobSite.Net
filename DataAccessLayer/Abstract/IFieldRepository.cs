using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IFieldRepository:IGenericRepository<Field>
    {
        Task<Field> GetFieldWithCategoriesWithWorksAsync(int id);

        Task<List<Field>> GetAllWithWorksAsync(Expression<Func<Field, bool>> filter);
    }
}
