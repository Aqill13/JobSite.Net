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
    public class ContactManager : IContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactManager(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<Contact> SReadInIPAddress(Expression<Func<Contact, bool>> filter)
        {
            return await _contactRepository.ReadInIPAddress(filter);
        }

        public async Task SCreateAsync(Contact entity)
        {
            await _contactRepository.CreateAsync(entity);
        }

        public async Task SDeleteAsync(Contact entity)
        {
            await _contactRepository.DeleteAsync(entity);
        }

        public async Task<List<Contact>> SGetAllAsync(Expression<Func<Contact, bool>> filter = null)
        {
            return await _contactRepository.GetAllAsync(filter);
        }

        public async Task<List<Contact>> SGetAllAsync(string parametr)
        {
            return await _contactRepository.GetAllAsync(parametr);
        }

        public async Task<Contact> SReadAsync(int id)
        {
            return await _contactRepository.ReadAsync(id);
        }

        public async Task SUpdateAsync(Contact entity)
        {
            await _contactRepository.UpdateAsync(entity);
        }
    }
}
