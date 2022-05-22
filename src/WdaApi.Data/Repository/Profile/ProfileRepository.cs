using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WdaApi.Business.Dto;
using WdaApi.Business.Interfaces;
using WdaApi.Business.Models;
using WdaApi.Data.Context;
using X.PagedList;

namespace WdaApi.Data.Repository
{
    public class ProfileRepository : Repository<ProfileUser>, IProfileRepository
    {
        public ProfileRepository(SaturnApiDbContext db) : base(db) { }

        public async Task<bool> checkProfileRegistered(ProfileUser profileCustomer)
        {
            var teste = await Db.Profiles.Where(p => p.Id != profileCustomer.Id && p.Name.ToLower() == profileCustomer.Name.ToLower()).AnyAsync();

            return await Db.Profiles.Where(p => p.Id != profileCustomer.Id && p.Name == profileCustomer.Name).AnyAsync();
        }


        public async Task<bool> checkProfileExist(Guid id)
        {
            return await Db.Profiles.Where(p => p.Id == id).AnyAsync();
        }

        public async Task<IPagedList<ProfileUser>> Search(FilterProfileUserDto filterVM)
        {
            IQueryable<ProfileUser> query = Db.Profiles;

            if (!string.IsNullOrEmpty(filterVM.Name))
            {
                query = query.Where(where => where.Name.Contains(filterVM.Name));
            }
            if (!string.IsNullOrEmpty(filterVM.Description))
            {
                query = query.Where(where => where.Description.Contains(filterVM.Description));
            }
            if (filterVM.Id != Guid.Empty)
            {
                query = query.Where(where => where.Id == filterVM.Id);
            }


            int? pageIndexP = null;
            if (filterVM.PageIndex > 0)
                pageIndexP = filterVM.PageIndex;

            int pageSizeP = filterVM.PageIndex > 0 ? filterVM.PageSize : Db.Profiles.Count();

            pageSizeP = pageSizeP == 0 ? 1 : pageSizeP;

            return await query.AsNoTracking().OrderByDescending(c => c.CreateAt).ToPagedListAsync<ProfileUser>(pageIndexP, pageSizeP);

        
        }
    }
}
