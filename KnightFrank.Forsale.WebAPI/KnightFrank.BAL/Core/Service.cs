using AutoMapper;
using IdentityModel;
using KnightFrank.BAL.CoreInterface;
using KnightFrank.DAL.Entities.Base;
using KnightFrank.DataAccessLayer.EF;
using KnightFrank.DataAccessLayer.EF.Common;
using KnightFrank.DataAccessLayer.EF.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace KnightFrank.BAL.Core
{
    public partial class Service<TEntity, TContext> : IService<TEntity, TContext> where TEntity : ModelEntityBase, new() where TContext : DataContext, IDataContextAsync, new()
    {
        protected readonly ILogger<Service<TEntity, TContext>> _logger;
        protected readonly IMapper _mapper;
        protected readonly IRepositoryAsync<TEntity, TContext> _repository;
        protected readonly ClaimsPrincipal _user;

        public Service(ILogger<Service<TEntity, TContext>> logger, IMapper mapper, IRepositoryAsync<TEntity, TContext> repository)
        {
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
        }

        public Service(ILogger<Service<TEntity, TContext>> logger, IMapper mapper, IRepositoryAsync<TEntity, TContext> repository, ClaimsPrincipal user)
        {
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
            _user = user;
        }

        //public virtual IAccountManager AccountManager => _accountManager;

        public virtual ILogger<Service<TEntity, TContext>> Logger => _logger;

        public virtual IMapper Mapper => _mapper;

        public virtual ClaimsPrincipal CurrentUser => _user;

        public virtual string CurrentUserId => CurrentUser?.FindFirst(JwtClaimTypes.Subject)?.Value?.Trim();

        //public virtual string CurrentUserName => CurrentUser?.FindFirst(PropertyConstants.DisplayName)?.Value;

        public virtual async Task CommitAsync(string user) => await _repository.CommitAsync(user);

        public virtual void Commit(string user) => _repository.Commit(user);

        public virtual void Add(string user, TEntity entity)
            => _repository.Add(user, entity);

        public virtual void Add(string user, IEnumerable<TEntity> entities)
            => _repository.Add(user, entities);

        public virtual void Update(string user, TEntity entity)
            => _repository.Update(user, entity);

        public virtual void Update(string user, IEnumerable<TEntity> entities)
            => _repository.Update(user, entities);

        public virtual void Delete(string user, TEntity entity)
            => _repository.Delete(user, entity);

        public virtual void Delete(string user, IEnumerable<TEntity> entities)
            => _repository.Delete(user, entities);

        public virtual void Delete(string user, object keyValue)
            => _repository.Delete(user, keyValue);

        public virtual void Delete(string user, params object[] keyValues)
            => _repository.Delete(user, keyValues);

        public virtual bool Exists(params object[] keyValues)
            => _repository.Exists(keyValues);

        public virtual bool Exists<TKey>(TKey keyValue)
            => _repository.Exists(keyValue);

        public virtual TEntity Find(params object[] keyValues)
            => _repository.Find(keyValues);

        public virtual IQueryFluentAsync<TEntity, TContext> Query()
            => _repository.Query();

        public virtual IQueryFluentAsync<TEntity, TContext> Query(IQueryObject<TEntity> queryObject)
            => _repository.Query(queryObject);

        public virtual IQueryFluentAsync<TEntity, TContext> Query(Expression<Func<TEntity, bool>> query)
            => _repository.Query(query);

        public virtual IQueryable<TEntity> Queryable()
         => _repository.Queryable();

        public virtual IQueryable<TEntity> QueryableSql(string sql, params object[] parameters)
            => _repository.QueryableSql(sql, parameters);

        public virtual async Task<bool> ExistsAsync(params object[] keyValues)
            => await _repository.ExistsAsync(keyValues);

        public virtual async Task<bool> ExistsAsync(CancellationToken cancellationToken, params object[] keyValues)
            => await _repository.ExistsAsync(cancellationToken, keyValues);

        public virtual async Task<bool> ExistsAsync<TKey>(TKey keyValue)
            => await _repository.ExistsAsync(keyValue);

        public virtual async Task<bool> ExistsAsync<TKey>(TKey keyValue, CancellationToken cancellationToken)
            => await _repository.ExistsAsync(cancellationToken, keyValue);

        public virtual async Task<TEntity> FindAsync(params object[] keyValues)
            => await _repository.FindAsync(keyValues);

        public virtual async Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
            => await _repository.FindAsync(cancellationToken, keyValues);

        public virtual async Task LoadPropertyAsync(TEntity item, Expression<Func<TEntity, object>> property, CancellationToken cancellationToken)
            => await _repository.LoadPropertyAsync(item, property, cancellationToken);

        public virtual async Task<IQueryable<TEntity>> QueryableAsync()
            => await _repository.QueryableAsync();

        public virtual async Task<IQueryable<TEntity>> QueryableAsync(CancellationToken cancellationToken)
            => await _repository.QueryableAsync(cancellationToken);

        public virtual async Task<IQueryable<TEntity>> QueryableSqlAsync(string sql, params object[] parameters)
            => await _repository.QueryableSqlAsync(sql, parameters);

        public virtual async Task<IQueryable<TEntity>> QueryableSqlAsync(string sql, CancellationToken cancellationToken, params object[] parameters)
            => await _repository.QueryableSqlAsync(sql, cancellationToken, parameters);

        public virtual IRepositoryAsync<T, TContext> GetRepository<T>() where T : EntityBase, new()
            => _repository.GetRepository<T>();

        protected virtual void UpdateChildList<TSource, TDestination>(IEnumerable<TSource> sources, ICollection<TDestination> destinations, string updateUser)
            where TDestination : EntityBase, new()
        {
            var _destinationRepository = GetRepository<TDestination>();
            DateTime updatedatetime = DateTime.Now;
            string updateuser = updateUser;

            foreach (var destinationItem in destinations)
            {
                var destinationItem_pis = destinationItem.GetType().GetProperties();

                var sourceItem = sources.SingleOrDefault(source =>
                {
                    var sourceItem_pis = source.GetType().GetProperties();

                    bool isValid = true;
                    foreach (var destinationItem_key_pis in destinationItem_pis.Where(c => c.CustomAttributes.Any(a => a.AttributeType == typeof(KeyAttribute))))
                    {
                        var destination_key_value = destinationItem_key_pis.GetValue(destinationItem);

                        var sourceItem_key_pis = sourceItem_pis.SingleOrDefault(s => s.CustomAttributes.Any(a => a.AttributeType == typeof(KeyAttribute)) && s.Name == destinationItem_key_pis.Name);

                        if (sourceItem_key_pis == null)
                            return false;

                        var source_key_value = sourceItem_key_pis.GetValue(source);

                        if (destination_key_value.ToString() != source_key_value.ToString())
                        {
                            isValid = false;
                        }
                    }

                    return isValid;
                });

                if (sourceItem == null)
                {
                    _destinationRepository.Delete(updateuser, destinationItem);
                }
            }

            foreach (var sourceItem in sources)
            {
                var sourceItem_pis = sourceItem.GetType().GetProperties();

                var destinationItem = destinations.SingleOrDefault(destination =>
                {
                    var destinationItem_pis = destination.GetType().GetProperties();

                    bool isValid = true;
                    foreach (var sourceItem_key_pis in sourceItem_pis.Where(c => c.CustomAttributes.Any(a => a.AttributeType == typeof(KeyAttribute))))
                    {
                        var source_key_value = sourceItem_key_pis.GetValue(sourceItem);

                        var destinationItem_key_pis = destinationItem_pis.SingleOrDefault(s => s.CustomAttributes.Any(a => a.AttributeType == typeof(KeyAttribute)) && s.Name == sourceItem_key_pis.Name);

                        if (destinationItem_key_pis == null)
                            return false;

                        var destination_key_value = destinationItem_key_pis.GetValue(destination);

                        if (destination_key_value.ToString() != source_key_value.ToString())
                        {
                            isValid = false;
                        }
                    }

                    return isValid;
                });


                if (destinationItem == null)
                {
                    var newItem = Mapper.Map<TDestination>(sourceItem);
                    _destinationRepository.Add(updateuser, newItem);
                }
                else
                {
                    Mapper.Map(sourceItem, destinationItem);
                    _destinationRepository.Update(updateuser, destinationItem);
                }
            }
        }
    }
}
