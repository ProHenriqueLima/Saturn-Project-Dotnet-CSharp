using Microsoft.EntityFrameworkCore;


namespace SaturnApi.Business.Models.Audit
{
    public class CustomAutoHistory : AutoHistory
    {
        public string UserId { get; set; }
    }
}
