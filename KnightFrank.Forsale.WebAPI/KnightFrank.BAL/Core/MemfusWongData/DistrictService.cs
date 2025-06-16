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

namespace KnightFrank.BAL.Core.MemfusWongData
{
    public class DistrictService : MemfusWongDataService<District>, IDistrictService
    {
        public DistrictService(ILogger<MemfusWongDataService<District>> logger, IMapper mapper, IMemfusWongDataBaseRepositoryAsync<District> repository)
            : base(logger, mapper, repository) { }


        public async Task<IEnumerable<DistrictDropdownDto>> GetDistrictsAsync()
            => await GetDistrictsAsync(1, 10);
        public async Task<IEnumerable<DistrictDropdownDto>> GetDistrictsAsync(int? pageNumber, int? pageSize)
            => await GetDistrictsAsync(pageNumber, pageSize, null);
        public async Task<IEnumerable<DistrictDropdownDto>> GetDistrictsAsync(int? pageNumber, int? pageSize, string zoneID)
        {
            try
            {
                IEnumerable<District> data;

                bool requirePaging = pageNumber.HasValue && pageNumber.Value > 0 && pageSize.HasValue && pageSize.Value > 0;
                var page = new Page(1, 10);

                var query = Query(e => e.IsActive);

                if (!string.IsNullOrWhiteSpace(zoneID))
                {
                    zoneID = zoneID.Trim();
                    query.Filter(fDistrict => fDistrict.Zone != null && fDistrict.Zone.IsActive && fDistrict.Zone.ZoneId == zoneID);
                }

                query.Filter(fDistrict => fDistrict.IsActive && fDistrict.Locations != null && fDistrict.Locations.Any(anyLocation => anyLocation.IsActive && anyLocation.PropertyInformations != null && anyLocation.PropertyInformations.Any(anyPropInfo => anyPropInfo.IsActive)));

                query.OrderBy(obQuery => obQuery.OrderBy(obDistrict => !string.IsNullOrWhiteSpace(obDistrict.DistrictName) ? obDistrict.DistrictName : string.Empty));


                if (requirePaging)
                {
                    page = new Page(pageNumber.Value, pageSize.Value);
                    data = await query.SelectPageAsync(page);
                }
                else
                {
                    data = await query.SelectAsync();
                }


                return Mapper.Map<IEnumerable<DistrictDropdownDto>>(data);




                //var query = GetRepository<PropertyInformation>().Query(e => e.IsActive);

                //query.Filter(f => f.Location != null && f.Location.IsActive);
                //query.Filter(f => f.Location.District != null && f.Location.District.IsActive);
                //query.Filter(f => f.Location.District.Zone != null && f.Location.District.Zone.IsActive);

                //if (!string.IsNullOrWhiteSpace(zoneID))
                //{
                //    query.Filter(f => f.Location.District.Zone.ZoneId == zoneID);
                //}

                //var data = await query.SelectAsync(s => s.Location.District);
                //data = data.GroupBy(g => g.DistrictId)
                //    .Select(s => s.FirstOrDefault())
                //    .OrderBy(ob => !string.IsNullOrWhiteSpace(ob.DistrictName) ? ob.DistrictName : string.Empty);

                //bool requirePaging = pageNumber.HasValue && pageNumber.Value >= 0 && pageSize.HasValue && pageSize.Value > 0;
                //if (requirePaging)
                //{
                //    data = data.Skip(pageNumber.Value * pageSize.Value).Take(pageSize.Value);
                //}

                //return Mapper.Map<IEnumerable<DistrictDropdownDto>>(data);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.GetExceptionErrorMessage());
                throw new Exception(ex.GetExceptionErrorMessage());
            }
        }
    }
}
