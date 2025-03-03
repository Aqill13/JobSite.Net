using System.ComponentModel.DataAnnotations;

namespace JobSite.Areas.User.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "İstifadəçi adınızı daxil edin")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Parolunuzu daxil edin")]
        public string Password { get; set; }
    }
}
