using Pedigree.Application.Models.DTOs;

namespace Pedigree.Application.Interfaces
{
    public interface IUserInformation
    {
        Task<ApplicationUserDTO> GetActualUser();
        Task<ApplicationUserDTO> GetUserByEmailAsync(string email);
        Task<ApplicationUserDTO> GetUserByNameAsync(string name);
    }
}