using KnightFrank.AuthServer.DAL.Entities.Models;
using KnightFrank.DataAccessLayer.EF.Core;
using Microsoft.EntityFrameworkCore;

namespace KnightFrank.AuthServer.DAL
{
    public class IdentityEntities : DataContext<ApplicationUser, ApplicationRole, int, ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin, ApplicationRoleClaim, ApplicationUserToken>
    {
        public IdentityEntities()
        {
            // ADD "Where(x => x.AuditEntryID == 0)" to allow multiple SaveChanges with same Audit
            //AuditManager.DefaultConfiguration.AutoSavePreAction = (context, audit) => (context as DBEntities).AuditEntries.AddRange(audit.Entries);
        }

        public IdentityEntities(DbContextOptions<IdentityEntities> options)
           : base(options)
        {

            // ADD "Where(x => x.AuditEntryID == 0)" to allow multiple SaveChanges with same Audit
            //AuditManager.DefaultConfiguration.AutoSavePreAction = (context, audit) => (context as DBEntities).AuditEntries.AddRange(audit.Entries);
        }
    }
}
