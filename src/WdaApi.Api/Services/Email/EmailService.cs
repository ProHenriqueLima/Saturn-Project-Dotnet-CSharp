
using SaturnApi.Api.Configuration;
using SaturnApi.Api.Controllers;
using SaturnApi.Api.Extensions;
using SaturnApi.Api.ViewModels.UserViewModel;
using SaturnApi.Business.Interfaces;
using SaturnApi.Business.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaturnApi.Api.Services
{
    public class EmailService : IEmailService
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _configuration;


        public EmailService(UserManager<ApplicationUser> userManager, 
             IEmailSender emailSender,
            IConfiguration configuration)
        {
    
            _userManager = userManager;         
            _emailSender = emailSender;
            _configuration = configuration;
        }



        public async Task SendUserConfirmationEmail(ApplicationUser userIdentity, string password, IStringLocalizer<UsersController> _localizer)
        {
            var confirmationLink = await GetUrlConfirmationEmail(userIdentity);

            string emailBody = _localizer["Sua senha de acesso: {1}", confirmationLink, password].Value;

            _emailSender.SendEmail(userIdentity.Email, _localizer["Confirme seu e-mail"].Value, emailBody);
        }
        public async Task SendClientConfirmationEmail(ApplicationUser userIdentity, string password, IStringLocalizer<AuthController> _localizer)
        {
            var confirmationLink = await GetUrlConfirmationEmail(userIdentity);     

            string emailBody = _localizer["Sua senha de acesso: {1}", confirmationLink, password].Value;

            _emailSender.SendEmail(userIdentity.Email, _localizer["Confirme seu e-mail"].Value, emailBody);
        }
        private async Task<string> GetUrlConfirmationEmail(ApplicationUser user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var confirmEmailToken = new EmailTokenViewModel()
            {
                userName = user.UserName,
                token = token
            };

            string json = JsonConvert.SerializeObject(confirmEmailToken);
            string confirmEmailTokenBase64 = UsefulFunctions.Base64Encode(json);

            return $"{_configuration.GetValue<string>("UrlSaturnApiWeb")}/confirm_email/{confirmEmailTokenBase64}";
        }

        public void Dispose()
        {
            
        }
    }
}
