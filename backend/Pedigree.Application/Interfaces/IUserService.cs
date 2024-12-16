using Pedigree.Application.Models.DTOs;

namespace Pedigree.Application.Interfaces
{
    public interface IUserService
    {
        Task<bool> AuthenicateUserAsync(LoginUserDTO loginUser);
        Task<ApplicationUserDTO> GetUserByEmailAsync(string userEmail);
        Task<ApplicationUserDTO> GetUserByNameAsync(string userName);
        Task RegisterUserAsync(RegisterUserDTO registerUser);
        Task<ApplicationUserDTO> UpdateUserAsync(ApplicationUserDTO applicationUser);
        Task DeleteUserByIdAsync(string userId);
        Task DeleteUserByNameAsync(string userName);
        Task DeleteUserByEmailAsync(string email);
        Task ChangePasswordAsync(ChangePasswordDTO changePassword, string userName);
        Task LogoutAsync();
    }
}