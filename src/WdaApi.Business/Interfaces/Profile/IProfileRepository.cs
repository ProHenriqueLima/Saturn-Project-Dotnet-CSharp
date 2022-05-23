using SaturnApi.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;
using SaturnApi.Business.Dto;

namespace SaturnApi.Business.Interfaces
{
    public interface IProfileRepository : IRepository<ProfileUser>
    {
        Task<bool> checkProfileExist(Guid id);

        Task<bool> checkProfileRegistered(ProfileUser profileCustomer);

        Task<IPagedList<ProfileUser>> Search(FilterProfileUserDto filterVM);


    }
}
