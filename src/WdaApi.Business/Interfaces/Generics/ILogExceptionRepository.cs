using WdaApi.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WdaApi.Business.Interfaces
{
    public interface ILogExceptionRepository
    {
        Task Add(LogException entity);
    }
}
