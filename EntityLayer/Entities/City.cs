using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public virtual List<Work> Works { get; set; } = new List<Work>();
        public virtual List<Candidate> Candidates { get; set; } = new List<Candidate>();
    }
}
