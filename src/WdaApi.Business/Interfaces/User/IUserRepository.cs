using WdaApi.Business.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace WdaApi.Business.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<User> GetByIdWithIncludes(Guid id);

        Task<IPagedList<User>> Search(int pageIndex, int pageSize,
                string fullName, string email,
                string profileName, bool? status,  Guid Id);
    }
}
