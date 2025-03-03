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
    public class CityManager : ICityService
    {
        private readonly ICityRepository _cityRepository;

        public CityManager(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public async Task SCreateAsync(City entity)
        {
            await _cityRepository.CreateAsync(entity);
        }

        public async Task SDeleteAsync(City entity)
        {
            await _cityRepository.DeleteAsync(entity);
        }

        public async Task<List<City>> SGetAllAsync(Expression<Func<City, bool>> filter = null)
        {
            return await _cityRepository.GetAllAsync(filter);
        }

        public async Task<List<City>> SGetAllAsync(string parametr)
        {
            return await _cityRepository.GetAllAsync(parametr);
        }

        public Task<List<City>> SGetListOrderByAsync()
        {
            return _cityRepository.GetListOrderByAsync();
        }

        public async Task<City> SReadAsync(int id)
        {
            return await _cityRepository.ReadAsync(id);
        }

        public async Task SUpdateAsync(City entity)
        {
            await _cityRepository.UpdateAsync(entity);
        }
    }
}
