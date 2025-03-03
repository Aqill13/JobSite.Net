using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
    public class Work
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public virtual Users? User { get; set; }
        public int FieldId { get; set; }                        //sahə
        public virtual Field? Field { get; set; }
        public int CategoryId { get; set; }                     //kateqoriya
        public virtual Category? Category { get; set; } 
        public int CityId { get; set; }                         //şəhər
        public virtual City? City { get; set; }

        [Required(ErrorMessage = "Position is required")]
        [StringLength(150, ErrorMessage = "Max. 150 characters")]
        public string Position { get; set; }                    //vəzifə

        [Required(ErrorMessage = "Min Age is required")]
        public int MinAge { get; set; }                         //Min. yaş

        [Required(ErrorMessage = "Max Age is required")]
        public int MaxAge { get; set; }                         //Max. yaş

        [Required(ErrorMessage = "Education is required")]
        public string Education { get; set; }                   //təhsil

        [Required(ErrorMessage = "Work Experience is required")]
        public string WorkExperience { get; set; }              //iş təcrübəsi
        public int? Salary { get; set; }                        //maaş
        public bool IsNegotiable { get; set; }                  //maaş razılaşma yolu ilə

        [Required(ErrorMessage = "Mode Of Work is required")]
        public string ModeOfWork { get; set; }                  //iş rejimi

        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }                      //cins

        [Required(ErrorMessage = "Job Information is required")]
        public string JobInformation { get; set; }              //iş barədə məlumat

        [Required(ErrorMessage = "Requirements is required")]
        public string Requirements { get; set; }                //tələblər
        public string? Note { get; set; }                       //qeyd  

        [Required(ErrorMessage = "Company Name is required")]
        public string CompanyName { get; set; }                 //şirkət

        [Required(ErrorMessage = "Contact Person is required")]
        public string ContactPerson { get; set; }               //əlaqədar şəxs
        public DateTime PublishDate { get; set; }               //elan tarixi
        public DateTime EndDate { get; set; }                   //bitmə tarixi
        public bool IsActive { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        public string Phone { get; set; }
        public int ViewsCount { get; set; } = 0;                //baxış sayı
    }
}
