using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ICandidateService:IGenericService<Candidate>
    {
        Task<List<Candidate>> SGetCandidatesWithIncludesAsync(string[] includes);
    }
}
