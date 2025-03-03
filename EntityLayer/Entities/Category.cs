using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Max. 50 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Field is required")]
        [Range(1, 1000, ErrorMessage = "Please select a Field")]
        public int FieldId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual Field? Field { get; set; } 
        public virtual List<Work> Works { get; set; } = new List<Work>();
        public virtual List<Candidate> Candidates { get; set; } = new List<Candidate>();
    }
}
