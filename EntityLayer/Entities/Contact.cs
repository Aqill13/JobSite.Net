using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
    public class Contact
    {
        public int Id { get; set; }
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Ad boş qoyula bilməz")]
        [StringLength(30, ErrorMessage = "Max. 30 hərf")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email boş qoyula bilməz")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mövzu boş qoyula bilməz")]
        [StringLength(50, ErrorMessage = "Max. 50 hərf")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Mesaj boş qoyula bilməz")]
        [StringLength(100, ErrorMessage = "Max. 100 hərf")]
        public string Message { get; set; }
    }
}
