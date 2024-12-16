using System.ComponentModel.DataAnnotations;

namespace Pedigree.Application.Models.DTOs
{
    public class ChangePasswordDTO
    {
        [Required]
        [StringLength(20, MinimumLength = 4)]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; } = string.Empty;

        [Required]
        [StringLength(20, MinimumLength = 4)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; } = string.Empty;
    }
}