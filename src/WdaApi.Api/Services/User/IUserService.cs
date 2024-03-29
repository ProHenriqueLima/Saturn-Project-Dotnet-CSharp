﻿using SaturnApi.Api.ViewModels;
using SaturnApi.Business.Models;
using SaturnApi.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaturnApi.Api.Services
{
    public interface IUserService : IDisposable
    {
        Task<bool> Add(User user, ApplicationUser applicationUser);
        Task<UserRequestVM> Update(Guid id, User user);
        Task<bool> CheckUserExist(Guid id);
        Task Delete(Guid id, UserDeleteVM user);

        Task<PagedResult<UserResponseVM>> Search(FilterUserVM filterVM);
    }
}
