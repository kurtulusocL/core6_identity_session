using System.ComponentModel.DataAnnotations;

namespace Identity_Session.Core.CrossCuttingConcern.Dtos.AuthDtos
{
    public class LoginDto
    {

        [MinLength(4), Required]
        [EmailAddress]
        public string Email { get; set; }

        [MinLength(4), Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }
    }
}
