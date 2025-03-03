using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface ICandidateRepository : IGenericRepository<Candidate>
    {
        Task<List<Candidate>> GetCandidatesWithIncludesAsync(string[] includes);
    }
}
