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
    public class BuildingService : MemfusWongDataService<Building>, IBuildingService
    {
        public BuildingService(ILogger<MemfusWongDataService<Building>> logger, IMapper mapper, IMemfusWongDataBaseRepositoryAsync<Building> repository)
            : base(logger, mapper, repository) { }

        public async Task<IEnumerable<BuildingDropdownDto>> SearchBuildingsAsync()
            => await SearchBuildingsAsync(1, 10);
        public async Task<IEnumerable<BuildingDropdownDto>> SearchBuildingsAsync(int? pageNumber, int? pageSize)
            => await SearchBuildingsAsync(pageNumber, pageSize, null);
        public async Task<IEnumerable<BuildingDropdownDto>> SearchBuildingsAsync(int? pageNumber, int? pageSize, string zoneID)
            => await SearchBuildingsAsync(pageNumber, pageSize, zoneID, null);
        public async Task<IEnumerable<BuildingDropdownDto>> SearchBuildingsAsync(int? pageNumber, int? pageSize, string zoneID, string districtID)
            => await SearchBuildingsAsync(pageNumber, pageSize, zoneID, districtID, null, null, null);
        public async Task<IEnumerable<BuildingDropdownDto>> SearchBuildingsAsync(int? pageNumber, int? pageSize, string zoneID, string districtID, Guid? streetID, string streetNumberFrom, string streetNumberTo)
            => await SearchBuildingsAsync(pageNumber, pageSize, zoneID, districtID, streetID, streetNumberFrom, streetNumberTo, null);
        public async Task<IEnumerable<BuildingDropdownDto>> SearchBuildingsAsync(int? pageNumber, int? pageSize, string zoneID, string districtID, Guid? streetID, string streetNumberFrom, string streetNumberTo, Guid? estateID)
            => await SearchBuildingsAsync(pageNumber, pageSize, zoneID, districtID, streetID, streetNumberFrom, streetNumberTo, estateID, null);
        public async Task<IEnumerable<BuildingDropdownDto>> SearchBuildingsAsync(int? pageNumber, int? pageSize, string zoneID, string districtID, Guid? streetID, string streetNumberFrom, string streetNumberTo, Guid? estateID, Guid? buildingID)
        {
            try
            {
                IEnumerable<Building> data;

                bool requirePaging = pageNumber.HasValue && pageNumber.Value > 0 && pageSize.HasValue && pageSize.Value > 0;
                var page = new Page(1, 10);

                var query = Query(e => e.IsActive);

                query.Filter(fBuilding => fBuilding.Locations != null && fBuilding.Locations.Any(anyLocation => anyLocation.IsActive && anyLocation.PropertyInformations != null && anyLocation.PropertyInformations.Any(anyPropInfo => anyPropInfo.IsActive)));

                if (!string.IsNullOrWhiteSpace(zoneID))
                {
                    zoneID = zoneID.Trim();
                    query.Filter(fBuilding => fBuilding.Locations.Any(anyLocation => anyLocation.IsActive && anyLocation.District != null && anyLocation.District.IsActive && anyLocation.District.Zone != null && anyLocation.District.Zone.IsActive && anyLocation.District.Zone.ZoneId == zoneID));
                }

                if (!string.IsNullOrWhiteSpace(districtID))
                {
                    districtID = districtID.Trim();
                    query.Filter(fBuilding => fBuilding.Locations.Any(anyLocation => anyLocation.IsActive && anyLocation.District != null && anyLocation.District.IsActive && anyLocation.DistrictId == districtID));
                }

                if (streetID.HasValue)
                {
                    query.Filter(fBuilding => fBuilding.Locations.Any(anyLocation => anyLocation.IsActive && (anyLocation.Street1 != null && anyLocation.Street1.IsActive && anyLocation.Street1.StreetId == streetID.Value)
                    || (anyLocation.Street2 != null && anyLocation.Street2.IsActive && anyLocation.Street2.StreetId == streetID.Value)));
                }

                if (estateID.HasValue)
                {
                    query.Filter(fBuilding => fBuilding.Locations.Any(anyLocation => anyLocation.IsActive && anyLocation.Estate != null && anyLocation.Estate.IsActive && anyLocation.Estate.EstateId == estateID.Value));
                }

                if (buildingID.HasValue)
                {
                    query.Filter(fBuilding => fBuilding.Locations.Any(anyLocation => anyLocation.IsActive && anyLocation.Building != null && anyLocation.Building.IsActive && anyLocation.Building.BuildingId == buildingID.Value));
                }

                query.OrderBy(obQuery => obQuery.OrderBy(obBuilding => !string.IsNullOrWhiteSpace(obBuilding.BuildingName) ? obBuilding.BuildingName : string.Empty));


                if (requirePaging)
                {
                    page = new Page(pageNumber.Value, pageSize.Value);
                    data = await query.SelectPageAsync(page);
                }
                else
                {
                    data = await query.SelectAsync();
                }


                return Mapper.Map<IEnumerable<BuildingDropdownDto>>(data);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.GetExceptionErrorMessage());
                throw new Exception(ex.GetExceptionErrorMessage());
            }
        }
    }
}
