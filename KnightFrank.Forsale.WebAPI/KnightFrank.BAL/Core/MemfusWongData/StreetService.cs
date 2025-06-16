using AutoMapper;
using KnightFrank.BAL.CoreInterface.MemfusWongData;
using KnightFrank.BAL.Dtos.MemfusWongData;
using KnightFrank.BAL.Extensions;
using KnightFrank.DAL;
using KnightFrank.DAL.Entities.Models.MemfusWongData;
using KnightFrank.DataAccessLayer.EF.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace KnightFrank.BAL.Core.MemfusWongData
{
    public class StreetService : MemfusWongDataService<Street>, IStreetService
    {
        public StreetService(ILogger<MemfusWongDataService<Street>> logger, IMapper mapper, IMemfusWongDataBaseRepositoryAsync<Street> repository)
            : base(logger, mapper, repository) { }

        public async Task<IEnumerable<StreetDropdownDto>> GetStreetsAsync()
            => await GetStreetsAsync(1, 10);
        public async Task<IEnumerable<StreetDropdownDto>> GetStreetsAsync(int? pageNumber, int? pageSize)
            => await GetStreetsAsync(pageNumber, pageSize, null);
        public async Task<IEnumerable<StreetDropdownDto>> GetStreetsAsync(int? pageNumber, int? pageSize, string zoneID)
            => await GetStreetsAsync(pageNumber, pageSize, zoneID, null);
        public async Task<IEnumerable<StreetDropdownDto>> GetStreetsAsync(int? pageNumber, int? pageSize, string zoneID, string districtID)
            => await GetStreetsAsync(pageNumber, pageSize, zoneID, districtID, null, null, null);
        public async Task<IEnumerable<StreetDropdownDto>> GetStreetsAsync(int? pageNumber, int? pageSize, string zoneID, string districtID, Guid? streetID, string streetNumberFrom, string streetNumberTo)
            => await GetStreetsAsync(pageNumber, pageSize, zoneID, districtID, streetID, streetNumberFrom, streetNumberTo, null);
        public async Task<IEnumerable<StreetDropdownDto>> GetStreetsAsync(int? pageNumber, int? pageSize, string zoneID, string districtID, Guid? streetID, string streetNumberFrom, string streetNumberTo, Guid? estateID)
            => await GetStreetsAsync(pageNumber, pageSize, zoneID, districtID, streetID, streetNumberFrom, streetNumberTo, estateID, null);
        public async Task<IEnumerable<StreetDropdownDto>> GetStreetsAsync(int? pageNumber, int? pageSize, string zoneID, string districtID, Guid? streetID, string streetNumberFrom, string streetNumberTo, Guid? estateID, Guid? buildingID)
        {
            try
            {
                IEnumerable<Street> street1Data;
                IEnumerable<Street> street2Data;

                bool requirePaging = pageNumber.HasValue && pageNumber.Value > 0 && pageSize.HasValue && pageSize.Value > 0;
                var page = new Page(1, 10);

                var street1Query = Query(e => e.IsActive);
                var street2Query = Query(e => e.IsActive);

                street1Query.Filter(fStreet => fStreet.LocationStreet1s != null && fStreet.LocationStreet1s.Any(anyLocation1 => anyLocation1.IsActive && anyLocation1.PropertyInformations != null && anyLocation1.PropertyInformations.Any(anyPropInfo1 => anyPropInfo1.IsActive)));
                street2Query.Filter(fStreet => fStreet.LocationStreet2s != null && fStreet.LocationStreet2s.Any(anyLocation2 => anyLocation2.IsActive && anyLocation2.PropertyInformations != null && anyLocation2.PropertyInformations.Any(anyPropInfo2 => anyPropInfo2.IsActive)));

                if (!string.IsNullOrWhiteSpace(zoneID))
                {
                    zoneID = zoneID.Trim();
                    street1Query.Filter(fStreet => fStreet.LocationStreet1s != null && fStreet.LocationStreet1s.Any(anyLocation1 => anyLocation1.IsActive && anyLocation1.District != null && anyLocation1.District.IsActive && anyLocation1.District.Zone != null && anyLocation1.District.Zone.IsActive && anyLocation1.District.ZoneId == zoneID));
                    street2Query.Filter(fStreet => fStreet.LocationStreet2s != null && fStreet.LocationStreet2s.Any(anyLocation2 => anyLocation2.IsActive && anyLocation2.District != null && anyLocation2.District.IsActive && anyLocation2.District.Zone != null && anyLocation2.District.Zone.IsActive && anyLocation2.District.ZoneId == zoneID));
                }

                if (!string.IsNullOrWhiteSpace(districtID))
                {
                    districtID = districtID.Trim();
                    street1Query.Filter(fStreet => fStreet.LocationStreet1s != null && fStreet.LocationStreet1s.Any(anyLocation1 => anyLocation1.IsActive && anyLocation1.District != null && anyLocation1.District.IsActive && anyLocation1.District.DistrictId == districtID));
                    street2Query.Filter(fStreet => fStreet.LocationStreet2s != null && fStreet.LocationStreet2s.Any(anyLocation2 => anyLocation2.IsActive && anyLocation2.District != null && anyLocation2.District.IsActive && anyLocation2.District.DistrictId == districtID));
                }

                if (streetID.HasValue)
                {
                    street1Query.Filter(fStreet => fStreet.LocationStreet1s.Any(anyLocation => anyLocation.IsActive && (anyLocation.Street1 != null && anyLocation.Street1.IsActive && anyLocation.Street1.StreetId == streetID.Value)
                    || (anyLocation.Street2 != null && anyLocation.Street2.IsActive && anyLocation.Street2.StreetId == streetID.Value))
                    );

                    street2Query.Filter(fStreet => fStreet.LocationStreet2s.Any(anyLocation => anyLocation.IsActive && (anyLocation.Street1 != null && anyLocation.Street1.IsActive && anyLocation.Street1.StreetId == streetID.Value)
                    || (anyLocation.Street2 != null && anyLocation.Street2.IsActive && anyLocation.Street2.StreetId == streetID.Value))
                    );
                }

                if (estateID.HasValue)
                {
                    street1Query.Filter(fStreet => fStreet.LocationStreet1s.Any(anyLocation => anyLocation.IsActive && anyLocation.Estate != null && anyLocation.Estate.IsActive && anyLocation.Estate.EstateId == estateID.Value));
                    street2Query.Filter(fStreet => fStreet.LocationStreet2s.Any(anyLocation => anyLocation.IsActive && anyLocation.Estate != null && anyLocation.Estate.IsActive && anyLocation.Estate.EstateId == estateID.Value));
                }

                if (buildingID.HasValue)
                {
                    street1Query.Filter(fStreet => fStreet.LocationStreet1s.Any(anyLocation => anyLocation.IsActive && anyLocation.Building != null && anyLocation.Building.IsActive && anyLocation.Building.BuildingId == buildingID.Value));
                    street2Query.Filter(fStreet => fStreet.LocationStreet2s.Any(anyLocation => anyLocation.IsActive && anyLocation.Building != null && anyLocation.Building.IsActive && anyLocation.Building.BuildingId == buildingID.Value));
                }

                var street1Task = street1Query.SelectAsync();
                var street2Task = street2Query.SelectAsync();

                await Task.WhenAll(street1Task, street2Task);

                street1Data = street1Task.Result;
                street2Data = street2Task.Result;

                var streetData = street1Data.Concat(street2Data);
                IEnumerable<Street> distinctStreetData = streetData
                    .Where(w => w != null)
                    .GroupBy(g => g.StreetId)
                    .Select(s => s.FirstOrDefault())
                    .OrderBy(o => o.StreetName);

                if (requirePaging)
                {
                    distinctStreetData = distinctStreetData.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value);
                }

                return Mapper.Map<IEnumerable<StreetDropdownDto>>(distinctStreetData);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.GetExceptionErrorMessage());
                throw new Exception(ex.GetExceptionErrorMessage());
            }
        }
    }
}
