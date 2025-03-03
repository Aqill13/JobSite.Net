using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class FavoriteManager : IFavoriteService
    {
        private readonly IFavoriteRepository _favoriteRepository;
        public async Task SCreateAsync(Favorite entity)
        {
            await _favoriteRepository.CreateAsync(entity);
        }

        public async Task SDeleteAsync(Favorite entity)
        {
            await _favoriteRepository.DeleteAsync(entity);
        }

        public async Task<List<Favorite>> SGetAllAsync(Expression<Func<Favorite, bool>> filter = null)
        {
            return await _favoriteRepository.GetAllAsync(filter);
        }

        public async Task<List<Favorite>> SGetAllAsync(string parametr)
        {
            return await _favoriteRepository.GetAllAsync(parametr);
        }

        public async Task<Favorite> SReadAsync(int id)
        {
            return await _favoriteRepository.ReadAsync(id);
        }

        public async Task SUpdateAsync(Favorite entity)
        {
            await _favoriteRepository.UpdateAsync(entity);
        }
    }
}
