using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IWorkRepository:IGenericRepository<Work>
    {
        Task<List<Work>> GetWorksWithIncludesAsync(string[] includes, Expression<Func<Work, bool>> filter = null);

    }
}
