using Microsoft.AspNetCore.Identity;

namespace KnightFrank.AuthServer.DAL.Entities.Models
{
    public class ApplicationUserToken : IdentityUserToken<int>
    {
        public virtual ApplicationUser User { get; set; }
    }
}
