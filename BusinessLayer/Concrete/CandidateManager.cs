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
    public class CandidateManager : ICandidateService
    {
        private readonly ICandidateRepository _candidateRepository;

        public CandidateManager(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        public async Task SCreateAsync(Candidate entity)
        {
            await _candidateRepository.CreateAsync(entity);
        }

        public async Task SDeleteAsync(Candidate entity)
        {
            await _candidateRepository.DeleteAsync(entity);
        }

        public async Task<List<Candidate>> SGetAllAsync(Expression<Func<Candidate, bool>> filter = null)
        {
            return await _candidateRepository.GetAllAsync(filter);
        }

        public async Task<List<Candidate>> SGetAllAsync(string parametr)
        {
            return await _candidateRepository.GetAllAsync(parametr);
        }

        public async Task<List<Candidate>> SGetCandidatesWithIncludesAsync(string[] includes)
        {
            return await _candidateRepository.GetCandidatesWithIncludesAsync(includes);
        }

        public async Task<Candidate> SReadAsync(int id)
        {
            return await _candidateRepository.ReadAsync(id);
        }

        public async Task SUpdateAsync(Candidate entity)
        {
            await _candidateRepository.UpdateAsync(entity);
        }
    }
}
