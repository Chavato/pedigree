using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pedigree.Application.Interfaces;
using Pedigree.Application.Models.DTOs;
using Pedigree.Domain.Interfaces.Repositories;

namespace Pedigree.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserInformation _userInformation;

        public UserService(IUserRepository userRepository, IUserInformation userInformation)
        {
            _userRepository = userRepository;
            _userInformation = userInformation;
        }

        public async Task<bool> AuthenicateUserAsync(LoginUserDTO loginUser)
        {
            bool result = await _userRepository.AuthenticateUserByEmailAsync(loginUser.Email, loginUser.Password);

            return result;
        }

        public async Task ChangePasswordAsync(ChangePasswordDTO changePassword, string email)
        {
            ApplicationUserDTO user = await _userInformation.GetUserByEmailAsync(email);

            if (await _userRepository.AuthenticateUserByEmailAsync(email, changePassword.OldPassword))
                await _userRepository.ChangePasswordAsync(user.Id, changePassword.NewPassword);

            else
                throw new ArgumentException("The old password is wrong.");

        }

        public async Task DeleteUserByIdAsync(string userId)
        {
            await _userRepository.DeleteUserByIdAsync(userId);
        }

        public async Task DeleteUserByNameAsync(string userName)
        {
            await _userRepository.DeleteUserByNameAsync(userName);
        }

        public async Task DeleteUserByEmailAsync(string email)
        {

            await _userRepository.DeleteUserByEmailAsync(email);
        }

        public async Task<ApplicationUserDTO> GetUserByEmailAsync(string userName)
        {
            ApplicationUserDTO user = await _userInformation.GetUserByEmailAsync(userName);

            return user;
        }

        public async Task<ApplicationUserDTO> GetUserByNameAsync(string userName)
        {
            ApplicationUserDTO user = await _userInformation.GetUserByNameAsync(userName);

            return user;
        }

        public async Task LogoutAsync()
        {
            await _userRepository.LogoutAsync();
        }

        public async Task RegisterUserAsync(RegisterUserDTO registerUser)
        {
            await _userRepository.RegisterUserAsync(registerUser.UserName,
                                                    registerUser.Email,
                                                    registerUser.Password);
        }

        public Task<ApplicationUserDTO> UpdateUserAsync(ApplicationUserDTO applicationUser)
        {
            throw new NotImplementedException();
        }
    }
}