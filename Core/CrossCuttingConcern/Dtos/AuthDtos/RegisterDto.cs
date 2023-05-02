using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Identity_Session.Core.CrossCuttingConcern.Dtos.AuthDtos
{
    public class RegisterDto
    {
        [Required]
        public string NameSurname { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public DateTime Birthdate { get; set; }

        [Required]
        public string RespondTitle { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Şifreler uyuşmuyor")]
        public string ConfirmPassword { get; set; }
    }
}
