using KnightFrank.DAL;
using KnightFrank.DataAccessLayer.EF;
using KnightFrank.DataAccessLayer.EF.Common;
using KnightFrank.DataAccessLayer.EF.Core;

namespace KnightFrank.BAL.CoreInterface
{
    public interface IDemoService<TEntity, TContext> : IRepositoryAsync<TEntity, TContext> where TEntity : EntityBase, new() where TContext : DataContext, IDataContextAsync, new() { }
    public interface IService<TEntity, TContext> : IRepositoryAsync<TEntity, TContext> where TEntity : EntityBase, new() where TContext : DataContext, IDataContextAsync, new() { }
    public interface IMemfusWongDataService<TEntity> : IMemfusWongDataBaseRepositoryAsync<TEntity> where TEntity : EntityBase, new() { }
}
