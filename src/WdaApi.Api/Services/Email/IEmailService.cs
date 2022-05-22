using WdaApi.Api.Controllers;
using WdaApi.Business.Models;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WdaApi.Api.Services
{
    public interface IEmailService : IDisposable
    {
        Task SendUserConfirmationEmail(ApplicationUser userIdentity, string password,  IStringLocalizer<UsersController> _localizer);
        Task SendClientConfirmationEmail(ApplicationUser userIdentity, string password, IStringLocalizer<AuthController> _localizer);
    }
}
