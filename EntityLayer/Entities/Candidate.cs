using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
    public class Candidate
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public virtual Users? User { get; set; }
        public string? Image { get; set; }

        [Required(ErrorMessage = "Firstname is required")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Lastname is required")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Patronymic is required")]
        public string Patronymic { get; set; }

        [Required(ErrorMessage = "Position is required")]
        [StringLength(150, ErrorMessage = "Max. 150 characters")]
        public string Position { get; set; }
        public int FieldId { get; set; }
        public virtual Field? Field { get; set; }
        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }
        public int CityId { get; set; }
        public virtual City? City { get; set; }

        [Required(ErrorMessage = "Age is required")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Education is required")]
        public string Education { get; set; }
        public string? EducationMore { get; set; }

        [Required(ErrorMessage = "Work Experience is required")]
        public string WorkExperience { get; set; }
        public string? WorkExperienceMore { get; set; }

        [Required(ErrorMessage = "Skills is required")]
        public string Skills { get; set; }

        [Required(ErrorMessage = "Min Salary is required")]
        public int MinSalary { get; set; }
        public string? AddInformation { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        public string Phone { get; set; }
        public DateTime PublishDate { get; set; }         
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public int ViewsCount { get; set; } = 0;
    }
}
