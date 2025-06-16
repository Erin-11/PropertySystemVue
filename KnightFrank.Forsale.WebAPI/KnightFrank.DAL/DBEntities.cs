using KnightFrank.DataAccessLayer.EF.Core;
using Microsoft.EntityFrameworkCore;

namespace KnightFrank.DAL
{
    public class DBEntities : DataContext
    {
        public DBEntities()
            : base()
        {
            // ADD "Where(x => x.AuditEntryID == 0)" to allow multiple SaveChanges with same Audit
            //AuditManager.DefaultConfiguration.AutoSavePreAction = (context, audit) => (context as DBEntities).AuditEntries.AddRange(audit.Entries);
        }

        public DBEntities(DbContextOptions<DBEntities> options)
           : base(options)
        {

            // ADD "Where(x => x.AuditEntryID == 0)" to allow multiple SaveChanges with same Audit
            //AuditManager.DefaultConfiguration.AutoSavePreAction = (context, audit) => (context as DBEntities).AuditEntries.AddRange(audit.Entries);
        }
    }
}
