using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Pedigree.Application.Interfaces;
using Pedigree.Application.Models.DTOs;
using Pedigree.Domain.Exceptions;

namespace Pedigree.Infra.Data.Identity
{
    public class UserInformation : IUserInformation
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public UserInformation(UserManager<ApplicationUser> userManager, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApplicationUserDTO> GetActualUser()
        {
            string? userName = _httpContextAccessor.HttpContext!.User.Identity!.Name;

            if (userName == null)
                throw new Exception("Problem with access HttpContext");

            return await GetUserByNameAsync(userName);
        }

        public async Task<ApplicationUserDTO> GetUserByEmailAsync(string email)
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                throw new EntityNotFoundException();
            }

            return _mapper.Map<ApplicationUserDTO>(user);
        }

        public async Task<ApplicationUserDTO> GetUserByNameAsync(string name)
        {
            ApplicationUser? user = await _userManager.FindByNameAsync(name);

            if (user == null)
            {
                throw new EntityNotFoundException();
            }

            return _mapper.Map<ApplicationUserDTO>(user);
        }
    }
}