using SaturnApi.Api.ViewModels;
using SaturnApi.Business.Models;
using SaturnApi.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SaturnApi.Business.Dto;

namespace SaturnApi.Api.Services.Profiles
{
    public interface IProfileService : IDisposable
    {
        Task Add(ProfileUser profile);
        Task Update(ProfileUser profile);    
        Task<PagedResult<ProfileResponseVM>> Search(FilterProfileUserDto filterVM);
        Task<bool> checkProfileExist(Guid id);
        Task<bool> CheckValidProfileAsync(ProfileUser profile);
        Task<ProfileResponseVM> GetById(Guid id);
        Task DeleteById(Guid id);

    }
}
