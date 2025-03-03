using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IWorkService:IGenericService<Work>
    {
        Task<List<Work>> SGetWorksWithIncludesAsync(string[] includes, Expression<Func<Work, bool>> filter = null);

    }
}
