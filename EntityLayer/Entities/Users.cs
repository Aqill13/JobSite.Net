using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
    public class Users : IdentityUser
    {
        public int UniqueNumber { get; set; }
        public string Fullname { get; set; }
        public DateTime CreateAccount  { get; set; }
        public DateTime LastLogin  { get; set; }
        public DateTime? LastActiveTime  { get; set; }
        public string Status { get; set; }
        public int ControleCode { get; set; }
        public virtual List<Work> Works { get; set; } = new List<Work>();
        public virtual List<Candidate> Candidates { get; set; } = new List<Candidate>();
    }
}
