using System.ComponentModel.DataAnnotations;

namespace JobSite.Areas.User.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Adınızı daxil edin")]
        [StringLength(20, ErrorMessage = "Ad sahəsi 20 xarakterdən uzun ola bilməz")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Soyadınızı daxil edin")]
        [StringLength(30, ErrorMessage = "Soyad sahəsi 30 xarakterdən uzun ola bilməz")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Şəxsi email'inizi daxil edin")]
        public string Email { get; set; }

        [Required(ErrorMessage = "İstifadəçi adı yaradın")]
        [StringLength(20, ErrorMessage = "İstifadəçi ad sahəsi 20 xarakterdən uzun ola bilməz")]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Parol yaradın")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Parolu təkrar edin")]
        [Compare("Password", ErrorMessage = "Parollar uyğun deyil")]
        public string ConfirmPassword { get; set; }
    }
}
