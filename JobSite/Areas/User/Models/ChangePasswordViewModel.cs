using System.ComponentModel.DataAnnotations;

namespace JobSite.Areas.User.Models
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Mövcud şifrə vacibdir")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "Yeni şifrə vacibdir")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Yeni şifrənin təkrarı vacibdir")]
        [Compare("NewPassword", ErrorMessage = "Şifrələr uyğun deyil")]
        public string ConfirmNewPassword { get; set; }
    }
}
