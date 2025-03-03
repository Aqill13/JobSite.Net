using EntityLayer.Entities;

namespace JobSite.Models
{
    public class FiltrModel
    {
        public int? FieldId { get; set; }
        public int? CategoryId { get; set; }
        public int? CityId { get; set; }
        public string? Education { get; set; }
        public string? WorkExprience { get; set; }
        public int? MinSalary { get; set; }

        //public Category? Category { get; set; }
        //public Field? Field { get; set; }
        //public City? City { get; set; }
    }
}
