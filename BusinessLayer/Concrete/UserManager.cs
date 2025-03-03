using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class UserManager : IUserService
    {
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository; 

        public UserManager(UserManager<Users> userManager, SignInManager<Users> signInManager, IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }
        private ClaimsPrincipal principal => _httpContextAccessor.HttpContext.User;


        public async Task<Users> GetUserAsync()
        {
            return await _userManager.GetUserAsync(principal);
        }

        public string GetUserId()
        {
            return _userManager.GetUserId(principal);
        }

        public async Task<IList<string>> GetUserRolesAsync()
        {
            var user = await GetUserAsync();
            return await _userManager.GetRolesAsync(user);
        }

        public async Task SCreateAsync(Users entity)
        {
            await _userManager.CreateAsync(entity);
        }

        public async Task SDeleteAsync(Users entity)
        {
            await _userManager.DeleteAsync(entity);
        }

        public async Task<List<Users>> SGetAllAsync(Expression<Func<Users, bool>> filter = null)
        {
            return await _userRepository.GetAllAsync(filter);
        }

        public async Task<List<Users>> SGetAllAsync(string parametr)
        {
            return await _userRepository.GetAllAsync(parametr);
        }

        public async Task<Users> SReadStringIdAsync(string id)
        {
            return await _userRepository.ReadStringIdAsync(id);
        }

        public async Task SUpdateAsync(Users entity)
        {
            await _userManager.UpdateAsync(entity);
        }
    }
}
