//using KnightFrank.DAL.Repositories;
using KnightFrank.DAL.Repositories;
using KnightFrank.DataAccessLayer.EF;
using KnightFrank.DataAccessLayer.EF.Configuration;
using KnightFrank.DataAccessLayer.EF.Core;
using Microsoft.Extensions.DependencyInjection;

namespace KnightFrank.DAL.Configuration
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationDbContext<TContext>(this IServiceCollection services, string connection, string migrationAssembly) where TContext : DataContext, IDataContextAsync, new()
        {
            services.AddDataAccessLayerDbContext<TContext>(connection, migrationAssembly);

            return services;
        }

        public static IServiceCollection AddApplicationDataRepository(this IServiceCollection services)
        {
            services.AddDataAccessLayerConfiguration();

            services.AddScoped(typeof(IMemfusWongDataBaseRepositoryAsync<>), typeof(MemfusWongDataBaseRepository<>));

            return services;
        }
    }
}
