
using SaturnApi.Business.Models;
using SaturnApi.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SaturnApi.Business.Interfaces;

namespace SaturnApi.Data.Repository
{
    public class LogExceptionRepository : ILogExceptionRepository
    {
        protected readonly SaturnApiDbContext Db;
        protected readonly DbSet<LogException> DbSet;

        public LogExceptionRepository(SaturnApiDbContext db)
        {
            Db = db;
            DbSet = db.Set<LogException>();
        }

        public async Task Add(LogException entity)
        {
            DbSet.Add(entity);
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await Db.SaveChangesAsync();
        }
    }
}