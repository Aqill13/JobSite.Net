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
    public class FieldManager : IFieldService
    {
        private readonly IFieldRepository _fieldRepository;

        public FieldManager(IFieldRepository fieldRepository)
        {
            _fieldRepository = fieldRepository;
        }

        public async Task SCreateAsync(Field entity)
        {
            await _fieldRepository.CreateAsync(entity);
        }

        public async Task SDeleteAsync(Field entity)
        {
            await _fieldRepository.DeleteAsync(entity);
        }

        public async Task<List<Field>> SGetAllAsync(Expression<Func<Field, bool>> filter = null)
        {
            return await _fieldRepository.GetAllAsync(filter);
        }

        public async Task<List<Field>> SGetAllAsync(string parametr)
        {
            return await _fieldRepository.GetAllAsync(parametr);
        }

        public async Task<List<Field>> SGetAllWithWorksAsync(Expression<Func<Field, bool>> filter)
        {
            return await _fieldRepository.GetAllWithWorksAsync(filter);
        }

        public async Task<Field> SGetFieldWithCategoriesWithWorksAsync(int id)
        {
            return await _fieldRepository.GetFieldWithCategoriesWithWorksAsync(id);
        }

        public async Task<Field> SReadAsync(int id)
        {
            return await _fieldRepository.ReadAsync(id);
        }

        public async Task SUpdateAsync(Field entity)
        {
            await _fieldRepository.UpdateAsync(entity);
        }
    }
}
