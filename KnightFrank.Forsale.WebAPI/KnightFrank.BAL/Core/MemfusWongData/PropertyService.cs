using AutoMapper;
using KnightFrank.BAL.CoreInterface.MemfusWongData;
using KnightFrank.BAL.Dtos.MemfusWongData;
using KnightFrank.BAL.Extensions;
using KnightFrank.DAL;
using KnightFrank.DAL.Entities.Models.MemfusWongData;
using KnightFrank.DataAccessLayer.EF;
using KnightFrank.DataAccessLayer.EF.Common;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using KnightFrank.BAL.StaticData;

namespace KnightFrank.BAL.Core.Compass
{
    public class PropertyService : MemfusWongDataService<Property>, IPropertyService
    {
        public PropertyService(ILogger<MemfusWongDataService<Property>> logger, IMapper mapper, IMemfusWongDataBaseRepositoryAsync<Property> repository)
            : base(logger, mapper, repository) { }

        public async Task<PagedResultDto<PropertyDto>> SearchPropertiesAsync()
            => await SearchPropertiesAsync();
        public async Task<PagedResultDto<PropertyDto>> SearchPropertiesAsync(string? region, string? district, List<string>? propertyTypes, decimal? minPrice, decimal? maxPrice, int? page, int? pageSize)
        {
            try
            {
                var staticData = StaticPropertyData.GetStaticProperties();
                var query = staticData.AsEnumerable();
                //var query = Query(e => e.IsActive);

                // Apply filters
                if (!string.IsNullOrEmpty(region))
                {
                    query = query.Where(p => p.Region == region);
                }

                if (!string.IsNullOrEmpty(district))
                {
                    query = query.Where(p => p.District == district);
                }

                if (propertyTypes != null && propertyTypes.Any())
                {
                    query = query.Where(p => propertyTypes.Contains(p.PropertyType));
                }

                if (minPrice.HasValue)
                {
                    query = query.Where(p => p.SalePrice >= minPrice.Value);
                }

                if (maxPrice.HasValue)
                {
                    query = query.Where(p => p.SalePrice <= maxPrice.Value);
                }

                // Get total count
                //var totalCount = await query.CountAsync();
                var totalCount = query.Count();

                var pageNumber = page ?? 1;
                var pageSizeValue = pageSize ?? 9;

                // Apply pagination
                //var data = await query
                //    .OrderBy(p => p.PropertyId)
                //    .Skip((pageNumber - 1) * pageSizeValue)
                //    .Take(pageSizeValue)
                //    .ToListAsync();
                var data = query
                    .Skip((pageNumber - 1) * pageSizeValue)
                    .Take(pageSizeValue)
                    .ToList();

                var dtos = Mapper.Map<IEnumerable<PropertyDto>>(data);

                var result = new PagedResultDto<PropertyDto>
                {
                    Data = dtos.ToList(),
                    TotalCount = totalCount,
                    Page = pageNumber,
                    PageSize = pageSizeValue
                };

                return result;

            }
            catch (Exception ex)
            {
                Logger.LogError(ex.GetExceptionErrorMessage());
                throw new Exception(ex.GetExceptionErrorMessage());
            }
        }
    }
}
