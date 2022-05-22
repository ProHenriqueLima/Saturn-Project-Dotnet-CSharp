using Microsoft.EntityFrameworkCore;


namespace WdaApi.Business.Models.Audit
{
    public class CustomAutoHistory : AutoHistory
    {
        public string UserId { get; set; }
    }
}
