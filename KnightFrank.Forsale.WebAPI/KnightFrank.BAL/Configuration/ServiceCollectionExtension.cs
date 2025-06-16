using KnightFrank.BAL.Core.Compass;
using KnightFrank.BAL.Core.MemfusWongData;
using KnightFrank.BAL.CoreInterface.MemfusWongData;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace KnightFrank.BAL.Configuration
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            #region Memfus Wong Data
            services.AddScoped<IZoneService, ZoneService>();
            services.AddScoped<IDistrictService, DistrictService>();

            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<IStreetService, StreetService>();
            services.AddScoped<IEstateService, EstateService>();
            services.AddScoped<IBuildingService, BuildingService>();
            services.AddScoped<IPhaseService, PhaseService>();
            services.AddScoped<IBlockService, BlockService>();

            services.AddScoped<IPropertyInformationService, PropertyInformationService>();
            services.AddScoped<IFloorService, FloorService>();
            services.AddScoped<IUnitService, UnitService>();

            #endregion

            return services;
        }
    }
}
