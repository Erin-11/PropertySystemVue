using KnightFrank.DataAccessLayer.EF.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KnightFrank.DAL.Entities.Base
{
    public abstract class ModelEntityMapBase<TEntity> : EntityMapBase<TEntity> where TEntity : ModelEntityBase
    {
        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            BaseConfigure(builder);
        }
    }
}
