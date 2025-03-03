using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IFieldService:IGenericService<Field>
    {
        Task<Field> SGetFieldWithCategoriesWithWorksAsync(int id);
        Task<List<Field>> SGetAllWithWorksAsync(Expression<Func<Field, bool>> filter);
    }
}
