using System.ComponentModel.DataAnnotations;

namespace Identity_Session.Core.CrossCuttingConcern.Dtos.AuthDtos
{
    public class ForgotPasswordDto
    {
        [Required, EmailAddress]
        public string Email { get; set; }
    }
}
