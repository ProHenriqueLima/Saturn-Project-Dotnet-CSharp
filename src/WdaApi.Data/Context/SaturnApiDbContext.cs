using SaturnApi.Business.Interfaces;
using SaturnApi.Business.Models;
using SaturnApi.Business.Models.Audit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SaturnApi.Data.Context
{
    public class SaturnApiDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IUser _user;


        public SaturnApiDbContext(DbContextOptions<SaturnApiDbContext> options, IUser user) : base(options)
        {
            _user = user;
        }
        public DbSet<CustomAutoHistory> Audits { get; set; }
        public DbSet<User> UsersCustume { get; set; }
        public DbSet<ProfileUser> Profiles { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SaturnApiDbContext).Assembly);

            modelBuilder.EnableAutoHistory<CustomAutoHistory>(o => { });

            modelBuilder.Entity<User>().HasQueryFilter(p => !p.UserIdentity.IsExcluded);

            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            if (_user.Name != null)
            {
                this.EnsureAutoHistory(() => new CustomAutoHistory
                {
                    UserId = _user.Name
                });
            }

            AddCreateAt();
            AddUpdateAt();


            return await base.SaveChangesAsync(cancellationToken);
        }

        private void AddCreateAt()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("CreateAt") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("CreateAt").CurrentValue = DateTime.Now;
                }
            }
        }

        private void AddUpdateAt()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("UpdateAt") != null))
            {
                if (entry.State == EntityState.Modified)
                {
                    entry.Property("UpdateAt").CurrentValue = DateTime.Now;
                    entry.Property("CreateAt").CurrentValue =  entry.Property("CreateAt").CurrentValue;
                }
            }
        }
    }
}
