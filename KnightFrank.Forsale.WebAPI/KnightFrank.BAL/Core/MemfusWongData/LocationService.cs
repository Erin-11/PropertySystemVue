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
    public class LocationService : MemfusWongDataService<Location>, ILocationService
    {
        public LocationService(ILogger<MemfusWongDataService<Location>> logger, IMapper mapper, IMemfusWongDataBaseRepositoryAsync<Location> repository)
            : base(logger, mapper, repository) { }

        public async Task<IEnumerable<LocationFilterDto>> SearchLocationsAsync()
            => await SearchLocationsAsync(1, 10);
        public async Task<IEnumerable<LocationFilterDto>> SearchLocationsAsync(int? pageNumber, int? pageSize)
            => await SearchLocationsAsync(pageNumber, pageSize, null);
        public async Task<IEnumerable<LocationFilterDto>> SearchLocationsAsync(int? pageNumber, int? pageSize, string zoneID)
            => await SearchLocationsAsync(pageNumber, pageSize, zoneID, null);
        public async Task<IEnumerable<LocationFilterDto>> SearchLocationsAsync(int? pageNumber, int? pageSize, string zoneID, string districtID)
            => await SearchLocationsAsync(pageNumber, pageSize, zoneID, districtID, null);
        public async Task<IEnumerable<LocationFilterDto>> SearchLocationsAsync(int? pageNumber, int? pageSize, string zoneID, string districtID, string streetNameKeyword)
            => await SearchLocationsAsync(pageNumber, pageSize, zoneID, districtID, streetNameKeyword, null, null);
        public async Task<IEnumerable<LocationFilterDto>> SearchLocationsAsync(int? pageNumber, int? pageSize, string zoneID, string districtID, string streetNameKeyword, string estateNameKeyword)
            => await SearchLocationsAsync(pageNumber, pageSize, zoneID, districtID, streetNameKeyword, estateNameKeyword, null);
        public async Task<IEnumerable<LocationFilterDto>> SearchLocationsAsync(int? pageNumber, int? pageSize, string zoneID, string districtID, string streetNameKeyword, string estateNameKeyword, string buildingNameKeyword)
        {
            try
            {
                IEnumerable<LocationFilterDto> data;

                bool requirePaging = pageNumber.HasValue && pageNumber.Value > 0 && pageSize.HasValue && pageSize.Value > 0;

                var query = Queryable();
                query = query.Where(fLocation => fLocation.IsActive);

                query = query.Where(fLocation => fLocation.PropertyInformations != null && fLocation.PropertyInformations.Any(anyPropInfo => anyPropInfo.IsActive));

                query = query.Where(fLocation => fLocation.District != null && fLocation.District.IsActive);
                query = query.Where(fLocation => fLocation.District.Zone != null && fLocation.District.Zone.IsActive);

                query = query.Where(fLocation => fLocation.Street1 != null && fLocation.Street1.IsActive);
                query = query.Where(fLocation => fLocation.Street2 != null && fLocation.Street2.IsActive);
                query = query.Where(fLocation => fLocation.Estate != null && fLocation.Estate.IsActive);
                query = query.Where(fLocation => fLocation.Building != null && fLocation.Building.IsActive);

                query = query.Include(i => i.District);
                query = query.Include(i => i.District.Zone);
                query = query.Include(i => i.Street1);
                query = query.Include(i => i.Street2);
                query = query.Include(i => i.Estate);
                query = query.Include(i => i.Building);

                if (!string.IsNullOrWhiteSpace(zoneID))
                {
                    zoneID = zoneID.Trim();
                    query = query.Where(fLocation => fLocation.District.ZoneId == zoneID);
                }

                if (!string.IsNullOrWhiteSpace(districtID))
                {
                    districtID = districtID.Trim();
                    query = query.Where(fLocation => fLocation.DistrictId == districtID);
                }

                if (!string.IsNullOrWhiteSpace(streetNameKeyword))
                {
                    query = query.Where(fLocation => (!string.IsNullOrWhiteSpace(fLocation.Street1.StreetName) && fLocation.Street1.StreetName.Trim().ToLower().Contains(streetNameKeyword.Trim().ToLower()))
                                            || (!string.IsNullOrWhiteSpace(fLocation.Street2.StreetName) && fLocation.Street2.StreetName.Trim().ToLower().Contains(streetNameKeyword.Trim().ToLower())));
                }

                //if (filterStreetNumberFrom.HasValue && filterStreetNumberTo.HasValue)
                //{
                //    query.Filter(f => (f.Location.Street1 != null && ((f.Location.StreetNumberFromNumeric1.HasValue && f.Location.StreetNumberFromNumeric1 >= filterStreetNumberFrom.Value && f.Location.StreetNumberFromNumeric1 <= filterStreetNumberTo.Value) || (f.Location.StreetNumberToNumeric1.HasValue && f.Location.StreetNumberToNumeric1 >= filterStreetNumberFrom.Value && f.Location.StreetNumberToNumeric1 <= filterStreetNumberTo.Value)))
                //                   || (f.Location.Street2 != null && ((f.Location.StreetNumberFromNumeric2.HasValue && f.Location.StreetNumberFromNumeric2 >= filterStreetNumberFrom.Value && f.Location.StreetNumberFromNumeric2 <= filterStreetNumberTo.Value) || (f.Location.StreetNumberToNumeric2.HasValue && f.Location.StreetNumberToNumeric2 >= filterStreetNumberFrom.Value && f.Location.StreetNumberToNumeric2 <= filterStreetNumberTo.Value))));
                //}
                //else if (filterStreetNumberFrom.HasValue)
                //{
                //    query.Filter(f => (f.Location.Street1 != null && ((f.Location.StreetNumberFromNumeric1.HasValue && f.Location.StreetNumberFromNumeric1 >= filterStreetNumberFrom.Value) || (f.Location.StreetNumberToNumeric1.HasValue && f.Location.StreetNumberToNumeric1 >= filterStreetNumberFrom.Value)))
                //                   || (f.Location.Street2 != null && ((f.Location.StreetNumberFromNumeric2.HasValue && f.Location.StreetNumberFromNumeric2 >= filterStreetNumberFrom.Value) || (f.Location.StreetNumberToNumeric2.HasValue && f.Location.StreetNumberToNumeric2 >= filterStreetNumberFrom.Value))));
                //}
                //else if (filterStreetNumberTo.HasValue)
                //{
                //    query.Filter(f => (f.Location.Street1 != null && ((f.Location.StreetNumberFromNumeric1.HasValue && f.Location.StreetNumberFromNumeric1 <= filterStreetNumberTo.Value) || (f.Location.StreetNumberToNumeric1.HasValue && f.Location.StreetNumberToNumeric1 <= filterStreetNumberTo.Value)))
                //                   || (f.Location.Street2 != null && ((f.Location.StreetNumberFromNumeric2.HasValue && f.Location.StreetNumberFromNumeric2 <= filterStreetNumberTo.Value) || (f.Location.StreetNumberToNumeric2.HasValue && f.Location.StreetNumberToNumeric2 <= filterStreetNumberTo.Value))));
                //}

                if (!string.IsNullOrWhiteSpace(estateNameKeyword))
                {
                    query = query.Where(fLocation => !string.IsNullOrWhiteSpace(fLocation.Estate.EstateName) && fLocation.Estate.EstateName.Trim().ToLower().Contains(estateNameKeyword.Trim().ToLower())
                                                || !string.IsNullOrWhiteSpace(fLocation.Building.BuildingName) && fLocation.Building.BuildingName.Trim().ToLower().Contains(estateNameKeyword.Trim().ToLower()));
                }

                if (!string.IsNullOrWhiteSpace(buildingNameKeyword))
                {
                    query = query.Where(fLocation => !string.IsNullOrWhiteSpace(fLocation.Estate.EstateName) && fLocation.Estate.EstateName.Trim().ToLower().Contains(buildingNameKeyword.Trim().ToLower())
                                                || !string.IsNullOrWhiteSpace(fLocation.Building.BuildingName) && fLocation.Building.BuildingName.Trim().ToLower().Contains(buildingNameKeyword.Trim().ToLower()));
                }

                if (requirePaging)
                {
                    data = query
                            .AsEnumerable()
                            .GroupBy(gb => new
                            {
                                ZoneId = gb.District.Zone.ZoneId,
                                DistrictId = gb.District.DistrictId,
                                Street1Id = gb.Street1.StreetId,
                                Street2Id = gb.Street2.StreetId,
                                EstateId = gb.Estate.EstateId,
                                BuildingId = gb.Building.BuildingId
                            }, (key, group) => new LocationFilterDto()
                            {
                                ZoneId = group.FirstOrDefault().District.Zone.ZoneId,
                                ZoneName = group.FirstOrDefault().District.Zone.ZoneName,
                                DistrictId = group.FirstOrDefault().District.DistrictId,
                                DistrictName = group.FirstOrDefault().District.DistrictName,
                                Street1Id = group.FirstOrDefault().Street1.StreetId,
                                StreetName1 = group.FirstOrDefault().Street1.StreetName,
                                Street2Id = group.FirstOrDefault().Street2.StreetId,
                                StreetName2 = group.FirstOrDefault().Street2.StreetName,
                                EstateId = group.FirstOrDefault().Estate.EstateId,
                                EstateName = group.FirstOrDefault().Estate.EstateName,
                                BuildingId = group.FirstOrDefault().Building.BuildingId,
                                BuildingName = group.FirstOrDefault().Building.BuildingName
                            })
                            .OrderBy(obLocation => !string.IsNullOrWhiteSpace(obLocation.ZoneName) ? obLocation.ZoneName : string.Empty)
                            .ThenBy(obLocation => !string.IsNullOrWhiteSpace(obLocation.DistrictName) ? obLocation.DistrictName : string.Empty)
                            .ThenBy(obLocation => obLocation.StreetName1 != null && !string.IsNullOrWhiteSpace(obLocation.StreetName1) ? obLocation.StreetName1 : string.Empty)
                            .ThenBy(obLocation => obLocation.StreetName2 != null && !string.IsNullOrWhiteSpace(obLocation.StreetName2) ? obLocation.StreetName2 : string.Empty)
                            .ThenBy(obLocation => obLocation.EstateName != null && !string.IsNullOrWhiteSpace(obLocation.EstateName) ? obLocation.EstateName : string.Empty)
                            .ThenBy(obLocation => obLocation.BuildingName != null && !string.IsNullOrWhiteSpace(obLocation.BuildingName) ? obLocation.BuildingName : string.Empty)
                            .Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value)
                            .ToList();
                }
                else
                {
                    data = query
                            .AsEnumerable()
                            .GroupBy(gb => new
                            {
                                ZoneId = gb.District.Zone.ZoneId,
                                DistrictId = gb.District.DistrictId,
                                Street1Id = gb.Street1.StreetId,
                                Street2Id = gb.Street2.StreetId,
                                EstateId = gb.Estate.EstateId,
                                BuildingId = gb.Building.BuildingId
                            }, (key, group) => new LocationFilterDto()
                            {
                                ZoneId = group.FirstOrDefault().District.Zone.ZoneId,
                                ZoneName = group.FirstOrDefault().District.Zone.ZoneName,
                                DistrictId = group.FirstOrDefault().District.DistrictId,
                                DistrictName = group.FirstOrDefault().District.DistrictName,
                                Street1Id = group.FirstOrDefault().Street1.StreetId,
                                StreetName1 = group.FirstOrDefault().Street1.StreetName,
                                Street2Id = group.FirstOrDefault().Street2.StreetId,
                                StreetName2 = group.FirstOrDefault().Street2.StreetName,
                                EstateId = group.FirstOrDefault().Estate.EstateId,
                                EstateName = group.FirstOrDefault().Estate.EstateName,
                                BuildingId = group.FirstOrDefault().Building.BuildingId,
                                BuildingName = group.FirstOrDefault().Building.BuildingName
                            })
                            .OrderBy(obLocation => !string.IsNullOrWhiteSpace(obLocation.ZoneName) ? obLocation.ZoneName : string.Empty)
                            .ThenBy(obLocation => !string.IsNullOrWhiteSpace(obLocation.DistrictName) ? obLocation.DistrictName : string.Empty)
                            .ThenBy(obLocation => obLocation.StreetName1 != null && !string.IsNullOrWhiteSpace(obLocation.StreetName1) ? obLocation.StreetName1 : string.Empty)
                            .ThenBy(obLocation => obLocation.StreetName2 != null && !string.IsNullOrWhiteSpace(obLocation.StreetName2) ? obLocation.StreetName2 : string.Empty)
                            .ThenBy(obLocation => obLocation.EstateName != null && !string.IsNullOrWhiteSpace(obLocation.EstateName) ? obLocation.EstateName : string.Empty)
                            .ThenBy(obLocation => obLocation.BuildingName != null && !string.IsNullOrWhiteSpace(obLocation.BuildingName) ? obLocation.BuildingName : string.Empty)
                            .ToList();
                }

                return data;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.GetExceptionErrorMessage());
                throw new Exception(ex.GetExceptionErrorMessage());
            }
        }
    }
}
