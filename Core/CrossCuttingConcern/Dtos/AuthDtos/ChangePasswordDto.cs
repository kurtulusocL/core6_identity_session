using System.ComponentModel.DataAnnotations;

namespace Identity_Session.Core.CrossCuttingConcern.Dtos.AuthDtos
{
    public class ChangePasswordDto
    {
        [Required]
        public string CurrentPassword { get; set; }

        [Required]
        public string NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessage = "Şifreler uyuşmuyor, kontrol edin.")]
        public string ConfirmNewPassword { get; set; }
        public string StatusMessage { get; set; }
    }
}
