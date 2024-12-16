using System.ComponentModel.DataAnnotations;

namespace Pedigree.Application.Models.DTOs
{
    public class RegisterUserDTO
    {

        [Required]
        [StringLength(25, MinimumLength = 4, ErrorMessage = "User Name lenght must be between 4 and 25 characters.")]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [MaxLength(64)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Password lenght must be between 4 and 20 characters.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Password lenght must be between 4 and 20 characters.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password confirmation does not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}