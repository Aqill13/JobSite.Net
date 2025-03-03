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
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryManager(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task SCreateAsync(Category entity)
        {
            await _categoryRepository.CreateAsync(entity);
        }

        public async Task SDeleteAsync(Category entity)
        {
            await _categoryRepository.DeleteAsync(entity);
        }

        public async Task<List<Category>> SGetAllAsync(Expression<Func<Category, bool>> filter = null)
        {
            return await _categoryRepository.GetAllAsync(filter);
        }

        public async Task<List<Category>> SGetAllAsync(string parametr)
        {
            return await _categoryRepository.GetAllAsync(parametr);
        }

        public async Task<List<Category>> SGetAllWithWorksAsync(Expression<Func<Category, bool>> filter)
        {
            return await _categoryRepository.GetAllWithWorksAsync(filter);
        }

        public async Task<Category> SReadAsync(int id)
        {
            return await _categoryRepository.ReadAsync(id);
        }

        public async Task SUpdateAsync(Category entity)
        {
            await _categoryRepository.UpdateAsync(entity);
        }
    }
}
