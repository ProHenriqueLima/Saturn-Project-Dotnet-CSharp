using SaturnApi.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SaturnApi.Business.Interfaces
{
    public interface ILogExceptionRepository
    {
        Task Add(LogException entity);
    }
}
