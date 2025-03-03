using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IUserService
    {
        Task SCreateAsync(Users entity);
        Task SUpdateAsync(Users entity);
        Task SDeleteAsync(Users entity);
        Task<List<Users>> SGetAllAsync(Expression<Func<Users, bool>> filter = null);
        Task<List<Users>> SGetAllAsync(string parametr);
        Task<Users> SReadStringIdAsync(string id);
        Task<Users> GetUserAsync();
        string GetUserId(); 
    }
}
