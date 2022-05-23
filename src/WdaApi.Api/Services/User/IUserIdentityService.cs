using SaturnApi.Api.ViewModels;
using SaturnApi.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaturnApi.Api.Services
{
    public interface IUserIdentityService : IDisposable
    {
        Task<ApplicationUser> Add(User user);

        Task<ApplicationUser> AddNotProfile(User user);

        Task RemoveUser(ApplicationUser applicationUser);

        string GetPassword();

        Task<bool> Update(string email, User user);
        Task<bool> UpdateStatus(string email, bool status);

    }
}
