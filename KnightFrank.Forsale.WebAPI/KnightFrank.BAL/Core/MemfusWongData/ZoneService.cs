using AutoMapper;
using KnightFrank.BAL.CoreInterface.MemfusWongData;
using KnightFrank.BAL.Dtos.MemfusWongData;
using KnightFrank.BAL.Extensions;
using KnightFrank.DAL;
using KnightFrank.DAL.Entities.Models.MemfusWongData;
using KnightFrank.DataAccessLayer.EF.Common;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnightFrank.BAL.Core.Compass
{
    public class ZoneService : MemfusWongDataService<Zone>, IZoneService
    {
        public ZoneService(ILogger<MemfusWongDataService<Zone>> logger, IMapper mapper, IMemfusWongDataBaseRepositoryAsync<Zone> repository)
            : base(logger, mapper, repository) { }

        public async Task<IEnumerable<ZoneDropdownDto>> GetZonesAsync()
            => await GetZonesAsync(1, 10);
        public async Task<IEnumerable<ZoneDropdownDto>> GetZonesAsync(int? pageNumber, int? pageSize)
        {
            try
            {
                IEnumerable<Zone> data;

                bool requirePaging = pageNumber.HasValue && pageNumber.Value > 0 && pageSize.HasValue && pageSize.Value > 0;
                var page = new Page(1, 10);

                var query = Query(e => e.IsActive);

                query.Filter(fZone => fZone.Districts != null && fZone.Districts.Any(anyDistrict => anyDistrict.IsActive && anyDistrict.Locations != null && anyDistrict.Locations.Any(anyLocation => anyLocation.IsActive && anyLocation.PropertyInformations != null && anyLocation.PropertyInformations.Any(anyPropInfo => anyPropInfo.IsActive))));

                query.OrderBy(obQuery => obQuery.OrderBy(obZone => !string.IsNullOrWhiteSpace(obZone.ZoneName) ? obZone.ZoneName : string.Empty));


                if (requirePaging)
                {
                    page = new Page(pageNumber.Value, pageSize.Value);
                    data = await query.SelectPageAsync(page);
                }
                else
                {
                    data = await query.SelectAsync();
                }


                return Mapper.Map<IEnumerable<ZoneDropdownDto>>(data);

            }
            catch (Exception ex)
            {
                Logger.LogError(ex.GetExceptionErrorMessage());
                throw new Exception(ex.GetExceptionErrorMessage());
            }
        }
    }
}
