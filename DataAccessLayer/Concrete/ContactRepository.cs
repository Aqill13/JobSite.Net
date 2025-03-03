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
    public class ContactRepository : GenericRepository<Contact, ApplicationDbContext>, IContactRepository
    {
        public ContactRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Contact> ReadInIPAddress(Expression<Func<Contact, bool>> filter)
        {
            return await _context.Contacts.Where(filter).OrderByDescending(x => x.Date).FirstOrDefaultAsync();
        }
    }
}
