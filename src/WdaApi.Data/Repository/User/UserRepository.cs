using SaturnApi.Business.Interfaces;
using SaturnApi.Business.Models;
using SaturnApi.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace SaturnApi.Data.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(SaturnApiDbContext db) : base(db) { }

        public async Task<User> GetByIdWithIncludes(Guid id)
        {

            return await Db.UsersCustume.Where(u => u.Id == id).Include(p => p.UserIdentity).ThenInclude(p => p.Profile).FirstOrDefaultAsync();
        }

        public async Task<IPagedList<User>> Search(int pageIndex, int pageSize, string fullName, string email, string profileName, bool? status, Guid Id)
        {
            IQueryable<User> query = Db.UsersCustume;

            if (!string.IsNullOrEmpty(fullName))
            {
                query = query.Where(where => where.FullName.Contains(fullName));
            }
            if (!string.IsNullOrEmpty(email))
            {
                query = query.Where(where => where.UserIdentity.Email.Contains(email));
            }
            if (!string.IsNullOrEmpty(profileName))
            {
                query = query.Where(where => where.UserIdentity.Profile.Name.Contains(profileName));
            }
            if (status != null)
            {
                query = query.Where(where => where.UserIdentity.IsDeleted == status);
            }
            if (Id.ToString() != "00000000-0000-0000-0000-000000000000")
            {
                query = query.Where(where => where.Id == Id);
            }


            int? pageIndexP = null;
            if (pageIndex > 0)
                pageIndexP = pageIndex;

            int pageSizeP = pageIndex > 0 ? pageSize : Db.UsersCustume.Count();

            pageSizeP = pageSizeP == 0 ? 1 : pageSizeP;

            return await query.OrderByDescending(c => c.CreateAt).AsNoTracking().Include(p => p.UserIdentity).ThenInclude(p => p.Profile).ToPagedListAsync<User>(pageIndexP, pageSizeP);
        }
    }
}
