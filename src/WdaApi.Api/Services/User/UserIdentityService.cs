using WdaApi.Api.Configuration;
using WdaApi.Api.Controllers;
using WdaApi.Api.ViewModels;
using WdaApi.Api.ViewModels.UserViewModel;
using WdaApi.Business.Interfaces;
using WdaApi.Business.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WdaApi.Api.Services
{
    public class UserIdentityService : BaseService, IUserIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        IStringLocalizer<UsersController> _localizer;
        private string password;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _configuration;
        public readonly IProfileRepository _profileRepository;

        public UserIdentityService(IErrorNotifier errorNotifier, UserManager<ApplicationUser> userManager,
            IStringLocalizer<UsersController> localizer, IEmailSender emailSender,
            IConfiguration configuration, IProfileRepository profileRepository) : base(errorNotifier)
        {
            _userManager = userManager;

            _localizer = localizer;
            _emailSender = emailSender;
            _configuration = configuration;
            _profileRepository = profileRepository;
        }

        public async Task<ApplicationUser> Add(User user)
        {
            if (!await validProfileAsync((Guid)user.UserIdentity.ProfileId))
            {
                NotifyError("Perfil não encontrado!");
                return null;
            }


            if (await addUserIdentity(user))
                return await _userManager.FindByEmailAsync(user.Email);

            return null;
        }
        public async Task<ApplicationUser> AddNotProfile(User user)
        {
            if (await addUserIdentity(user))
                return await _userManager.FindByEmailAsync(user.Email);

            return null;
        }

        public async Task<bool> addUserIdentity(User user)
        {
            var userIdentity = new ApplicationUser { UserName = user.UserIdentity.Email, Email = user.Email, ProfileId = user.UserIdentity.ProfileId };

            this.password = UsefulFunctions.GenerateRandomPassword();

            var result = await _userManager.CreateAsync(userIdentity, this.password);

            if (result.Succeeded)
            {
                return true;
            }
            else
            {
                return addErrors(result);
            }
        }
        public async Task<bool> Update(string email, User user)
        {
            var userIdentity = await _userManager.FindByEmailAsync(email);
            userIdentity.IsDeleted = user.UserIdentity.IsDeleted;
            userIdentity.ProfileId = user.UserIdentity.ProfileId;
            await _userManager.UpdateAsync(userIdentity);
            return true;
        }
        public async Task<bool> UpdateStatus(string email, bool status)
        {
            var userIdentity = await _userManager.FindByEmailAsync(email);
            userIdentity.IsDeleted = status;         
            await _userManager.UpdateAsync(userIdentity);
            return true;
        }
        public async Task RemoveUser(ApplicationUser applicationUser)
        {
            await _userManager.DeleteAsync(applicationUser);
        }

        private bool addErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                NotifyError(error.Description);

            }
            return false;
        }
        private async Task<ApplicationUser> getUserIdentity(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        public string GetPassword()
        {
            return this.password;
        }
        private async Task<bool> validProfileAsync(Guid id)
        {
            return await _profileRepository.checkProfileExist(id);
        }
        public void Dispose()
        {
            _userManager?.Dispose();
        }


    }
}
