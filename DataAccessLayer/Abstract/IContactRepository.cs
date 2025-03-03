using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IContactRepository : IGenericRepository<Contact>
    {
        Task<Contact> ReadInIPAddress(Expression<Func<Contact, bool>> filter);
    }
}
