namespace Pedigree.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<bool> AuthenticateUserByEmailAsync(string email, string password);
        Task<bool> AuthenticateUserByUserNameAsync(string userName, string password);
        Task RegisterUserAsync(string userName, string email, string password);
        Task UpdateUserAsync(string id, string email);
        Task DeleteUserByIdAsync(string userId);
        Task DeleteUserByNameAsync(string userName);
        Task ChangePasswordAsync(string id, string newPassword);
        Task LogoutAsync();
    }
}