using WdaApi.Api.ViewModels;
using WdaApi.Business.Models;
using WdaApi.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WdaApi.Business.Dto;

namespace WdaApi.Api.Services.Profiles
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
