using KnightFrank.DAL.Entities.Base;
using KnightFrank.DataAccessLayer.EF;
using KnightFrank.DataAccessLayer.EF.Common;
using KnightFrank.DataAccessLayer.EF.Core;
using System;
using static KnightFrank.DAL.Common.Constant;

namespace KnightFrank.DAL.Repositories
{
    public class MemfusWongDataBaseRepository<TEntity> : Repository<TEntity, MemfusWongDataEntities>, IMemfusWongDataBaseRepositoryAsync<TEntity> where TEntity : ModelEntityBase, new()
    {
        public MemfusWongDataBaseRepository(IUnitOfWorkAsync<MemfusWongDataEntities> unitOfWork)
            : base(unitOfWork) { }

        public override void Add(string user, TEntity entity)
        {
            entity.IsActive = DBConstant.IsActive;
            entity.CreatedDate = DateTime.Now;
            entity.CreatedBy = user;

            entity.ObjectState = ObjectState.Added;
            DbSet.Add(entity);
        }

        public override void Update(string user, TEntity entity)
        {
            entity.IsActive = DBConstant.IsActive;
            entity.ModifiedDate = DateTime.Now;
            entity.ModifiedBy = user;

            entity.ObjectState = ObjectState.Modified;
            DbSet.Attach(entity);
        }

        public override void Delete(string user, TEntity entity)
        {
            entity.IsActive = DBConstant.IsInactive;
            entity.ModifiedDate = DateTime.Now;
            entity.ModifiedBy = user;

            entity.ObjectState = ObjectState.Modified;
            DbSet.Attach(entity);
        }
    }
}
