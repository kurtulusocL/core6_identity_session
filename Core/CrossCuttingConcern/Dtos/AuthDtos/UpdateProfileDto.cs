using System.ComponentModel.DataAnnotations;

namespace Identity_Session.Core.CrossCuttingConcern.Dtos.AuthDtos
{
    public class UpdateProfileDto
    {
        [Required]
        public string Username { get; set; }        

        [Required]
        public string PhoneNumber { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        public DateTime? UpdatedDate { get; set; }
        public bool IsConfirm { get; set; }
        public UpdateProfileDto()
        {
            IsConfirm = true;
        }
    }
}
