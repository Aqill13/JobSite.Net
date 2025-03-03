using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IGenericService<T> where T : class
    {
        Task SCreateAsync(T entity);
        Task<T> SReadAsync(int id);
        Task SUpdateAsync(T entity);
        Task SDeleteAsync(T entity);
        Task<List<T>> SGetAllAsync(Expression<Func<T, bool>> filter = null);
        Task<List<T>> SGetAllAsync(string parametr);
    }
}
