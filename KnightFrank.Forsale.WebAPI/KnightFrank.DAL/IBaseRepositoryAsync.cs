using KnightFrank.DataAccessLayer.EF;
using KnightFrank.DataAccessLayer.EF.Common;

namespace KnightFrank.DAL
{
    public interface IMemfusWongDataBaseRepositoryAsync<TEntity> : IRepositoryAsync<TEntity, MemfusWongDataEntities> where TEntity : EntityBase, new() { }
}
