using System.ComponentModel.DataAnnotations;

namespace Pedigree.Application.Models.DTOs
{
    public class LoginUserDTO
    {
        [Required]
        [MaxLength(64)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(20, MinimumLength = 4)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}