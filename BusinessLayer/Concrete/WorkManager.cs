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
    public class WorkManager : IWorkService
    {
        private readonly IWorkRepository _workRepository;

        public WorkManager(IWorkRepository workRepository)
        {
            _workRepository = workRepository;
        }

        public async Task SCreateAsync(Work entity)
        {
            await _workRepository.CreateAsync(entity);
        }

        public async Task SDeleteAsync(Work entity)
        {
            await _workRepository.DeleteAsync(entity);
        }

        public async Task<List<Work>> SGetAllAsync(Expression<Func<Work, bool>> filter = null)
        {
            return await _workRepository.GetAllAsync(filter);
        }

        public async Task<List<Work>> SGetAllAsync(string parametr)
        {
            return await _workRepository.GetAllAsync(parametr);
        }

        public async Task<List<Work>> SGetWorksWithIncludesAsync(string[] includes, Expression<Func<Work, bool>> filter = null)
        {
            return await _workRepository.GetWorksWithIncludesAsync(includes, filter);
        }

        public async Task<Work> SReadAsync(int id)
        {
            return await _workRepository.ReadAsync(id);
        }

        public async Task SUpdateAsync(Work entity)
        {
            await _workRepository.UpdateAsync(entity);
        }
    }
}
