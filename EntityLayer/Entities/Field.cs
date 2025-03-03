using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
    public class Field
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Max. 50 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Icon is required")]
        [StringLength(50, ErrorMessage = "Max. 50 characters")] 
        public string Icon { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public virtual List<Category> Categories { get; set; } = new List<Category>();
        public virtual List<Candidate> Candidates { get; set; } = new List<Candidate>();
        public virtual List<Work> Works { get; set; } = new List<Work>();

    }
}
