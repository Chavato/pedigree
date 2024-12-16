using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Pedigree.Domain.Exceptions;
using Pedigree.Domain.Interfaces.Repositories;
using Pedigree.Infra.Data.Identity;

namespace Pedigree.Infra.Data.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;

        public UserRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        public async Task<bool> AuthenticateUserByEmailAsync(string email, string password)
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                throw new EntityNotFoundException("User not found");
            }

            bool isAuthenticate = await _userManager.CheckPasswordAsync(user, password);

            return isAuthenticate;
        }

        public async Task<bool> AuthenticateUserByUserNameAsync(string userName, string password)
        {
            ApplicationUser? user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                throw new EntityNotFoundException("User not found");
            }

            bool isAuthenticate = await _userManager.CheckPasswordAsync(user, password);

            return isAuthenticate;
        }

        public async Task ChangePasswordAsync(string id, string newPassword)
        {
            ApplicationUser? user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                throw new EntityNotFoundException("User not found.");
            }

            string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            var result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);

            if (!result.Succeeded)
            {
                throw new Exception("Change password failed.");
            }
        }

        public async Task DeleteUserByIdAsync(string userId)
        {
            ApplicationUser? user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                throw new EntityNotFoundException("User not found.");

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
                throw new Exception("Something was wrong.");

        }

        public async Task DeleteUserByNameAsync(string userName)
        {
            ApplicationUser? user = await _userManager.FindByNameAsync(userName);

            if (user == null)
                throw new EntityNotFoundException("User not found.");

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
                throw new Exception("Something was wrong.");

        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task RegisterUserAsync(string userName, string email, string password)
        {
            ApplicationUser user = new ApplicationUser(userName, email);

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.FirstOrDefault()!.Description);
            }

            await _signInManager.SignInAsync(user, false);
        }

        public Task UpdateUserAsync(string id, string email)
        {
            throw new NotImplementedException();
        }
    }
}