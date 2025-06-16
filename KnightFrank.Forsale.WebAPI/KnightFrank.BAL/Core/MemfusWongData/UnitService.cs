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
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace KnightFrank.BAL.Core.MemfusWongData
{
    public class UnitService : MemfusWongDataService<Unit>,  IUnitService
    {
        public UnitService(ILogger<MemfusWongDataService<Unit>> logger, IMapper mapper, IMemfusWongDataBaseRepositoryAsync<Unit> repository)
            : base(logger, mapper, repository) { }

        public async Task<IEnumerable<UnitDropdownDto>> SearchUnitAsync()
            => await SearchUnitAsync(1, 10);
        public async Task<IEnumerable<UnitDropdownDto>> SearchUnitAsync(int? pageNumber, int? pageSize)
            => await SearchUnitAsync(pageNumber, pageSize, null);
        public async Task<IEnumerable<UnitDropdownDto>> SearchUnitAsync(int? pageNumber, int? pageSize, string zoneID)
            => await SearchUnitAsync(pageNumber, pageSize, zoneID, null);
        public async Task<IEnumerable<UnitDropdownDto>> SearchUnitAsync(int? pageNumber, int? pageSize, string zoneID, string districtID)
            => await SearchUnitAsync(pageNumber, pageSize, zoneID, districtID,null);
        public async Task<IEnumerable<UnitDropdownDto>> SearchUnitAsync(int? pageNumber, int? pageSize, string zoneID, string districtID, Guid? streetID)
            => await SearchUnitAsync(pageNumber, pageSize, zoneID, districtID, streetID, null);
        public async Task<IEnumerable<UnitDropdownDto>> SearchUnitAsync(int? pageNumber, int? pageSize, string zoneID, string districtID, Guid? streetID, Guid? estateID)
            => await SearchUnitAsync(pageNumber, pageSize, zoneID, districtID, streetID, estateID, null);
        public async Task<IEnumerable<UnitDropdownDto>> SearchUnitAsync(int? pageNumber, int? pageSize, string zoneID, string districtID, Guid? streetID, Guid? estateID, Guid? buildingID)
            => await SearchUnitAsync(pageNumber, pageSize, zoneID, districtID, streetID, estateID, buildingID, null);
        public async Task<IEnumerable<UnitDropdownDto>> SearchUnitAsync(int? pageNumber, int? pageSize, string zoneID, string districtID, Guid? streetID, Guid? estateID, Guid? buildingID, int? phaseID)
            => await SearchUnitAsync(pageNumber, pageSize, zoneID, districtID, streetID, estateID, buildingID, phaseID, null);
        public async Task<IEnumerable<UnitDropdownDto>> SearchUnitAsync(int? pageNumber, int? pageSize, string zoneID, string districtID, Guid? streetID, Guid? estateID, Guid? buildingID, int? phaseID, int? blockID)
            => await SearchUnitAsync(pageNumber, pageSize, zoneID, districtID, streetID, estateID, buildingID, phaseID, blockID, null);
        public async Task<IEnumerable<UnitDropdownDto>> SearchUnitAsync(int? pageNumber, int? pageSize, string zoneID, string districtID, Guid? streetID, Guid? estateID, Guid? buildingID, int? phaseID, int? blockID, int? floorID)
        {
            try
            {
                var query = GetRepository<PropertyInformation>().Query(e => e.IsActive);

                query.Filter(f => f.Location != null && f.Location.IsActive);
                query.Filter(f => f.Location.District != null && f.Location.District.IsActive);
                query.Filter(f => f.Location.District.Zone != null && f.Location.District.Zone.IsActive);

                query.Filter(f => f.Floor != null && f.Floor.IsActive);
                query.Filter(f => f.Unit != null && f.Unit.IsActive);

                if (!string.IsNullOrWhiteSpace(zoneID))
                {
                    zoneID = zoneID.Trim();
                    query.Filter(f => f.Location.District.ZoneId == zoneID);
                }
                if (!string.IsNullOrWhiteSpace(districtID))
                {
                    districtID = districtID.Trim();
                    query.Filter(f => f.Location.District.DistrictId == districtID);
                }
                if (streetID.HasValue)
                {
                    query.Filter(f => (f.Location.Street1Id.HasValue && f.Location.Street1Id == streetID) || (f.Location.Street2Id.HasValue && f.Location.Street2Id == streetID));
                }
                if (estateID.HasValue)
                {
                    query.Filter(f => f.Location.EstateId == estateID);
                }
                if (buildingID.HasValue)
                {
                    query.Filter(f => f.Location.BuildingId == buildingID);
                }
                if (phaseID.HasValue)
                {
                    query.Filter(f => f.Location.PhaseId == phaseID);
                }
                if (blockID.HasValue)
                {
                    query.Filter(f => f.Location.BlockId == blockID);
                }
                if (floorID.HasValue)
                {
                    query.Filter(f => f.FloorId == floorID);
                }

                var data = await query.SelectAsync(s => s.Unit);
                data = data.GroupBy(g => g.UnitId)
                    .Select(s => s.FirstOrDefault())
                    .OrderBy(ob => !string.IsNullOrWhiteSpace(ob.UnitName) ? Regex.Replace(ob.UnitName, @"\d+", n => n.Value.PadLeft(4, '0')) : string.Empty);

                bool requirePaging = pageNumber.HasValue && pageNumber.Value > 0 && pageSize.HasValue && pageSize.Value > 0;
                if (requirePaging)
                {
                    data = data.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value);
                }

                return Mapper.Map<IEnumerable<UnitDropdownDto>>(data);


                //IEnumerable<Unit> data;

                //bool requirePaging = pageNumber.HasValue && pageNumber.Value > 0 && pageSize.HasValue && pageSize.Value > 0;
                //var page = new Page(1, 10);

                //var query = GetRepository<Unit>().Query(e => e.IsActive);

                ////query.Include(i => i.PropertyInformations, p => ((PropertyInformation)p).Location.District);
                ////query.Include(i => i.PropertyInformations, p => ((PropertyInformation)p).Location.District.Zone);
                ////query.Include(i => i.PropertyInformations, p => ((PropertyInformation)p).Location.Street1);
                ////query.Include(i => i.PropertyInformations, p => ((PropertyInformation)p).Location.Street2);
                ////query.Include(i => i.PropertyInformations, p => ((PropertyInformation)p).Location.Estate);
                ////query.Include(i => i.PropertyInformations, p => ((PropertyInformation)p).Location.Building);
                ////query.Include(i => i.PropertyInformations, p => ((PropertyInformation)p).Location.Phase);
                ////query.Include(i => i.PropertyInformations, p => ((PropertyInformation)p).Location.Block);
                ////query.Include(i => i.PropertyInformations, p => ((PropertyInformation)p).Floor);

                //query.Filter(fUnit => fUnit.PropertyInformations != null && fUnit.PropertyInformations.Any(anyPropInfo => anyPropInfo.IsActive));

                //if (!string.IsNullOrWhiteSpace(zoneID))
                //{
                //    query.Filter(fUnit => fUnit.PropertyInformations.Any(anyPropInfo => anyPropInfo.IsActive && anyPropInfo.Location != null && anyPropInfo.Location.IsActive && anyPropInfo.Location.District != null && anyPropInfo.Location.District.IsActive && anyPropInfo.Location.District.Zone != null && anyPropInfo.Location.District.Zone.IsActive && anyPropInfo.Location.District.Zone.ZoneId == zoneID));
                //}

                ////if (!string.IsNullOrWhiteSpace(districtID))
                ////{
                ////    query.Filter(fUnit => fUnit.PropertyInformations.Any(anyPropInfo => anyPropInfo.IsActive && anyPropInfo.Location != null && anyPropInfo.Location.IsActive && anyPropInfo.Location.District != null && anyPropInfo.Location.District.IsActive && anyPropInfo.Location.District.DistrictId == districtID));
                ////}

                ////if (streetID.HasValue)
                ////{
                ////    query.Filter(fUnit => fUnit.PropertyInformations.Any(anyPropInfo => anyPropInfo.IsActive && anyPropInfo.Location != null && anyPropInfo.Location.IsActive &&
                ////    ((anyPropInfo.Location.Street1 != null && anyPropInfo.Location.Street1.IsActive && anyPropInfo.Location.Street1.StreetId == streetID.Value)
                ////    || (anyPropInfo.Location.Street2 != null && anyPropInfo.Location.Street2.IsActive && anyPropInfo.Location.Street2.StreetId == streetID.Value))));
                ////}

                ////if (estateID.HasValue)
                ////{
                ////    query.Filter(fUnit => fUnit.PropertyInformations.Any(anyPropInfo => anyPropInfo.IsActive && anyPropInfo.Location != null && anyPropInfo.Location.IsActive && anyPropInfo.Location.Estate != null && anyPropInfo.Location.Estate.IsActive && anyPropInfo.Location.Estate.EstateId == estateID.Value));
                ////}

                ////if (buildingID.HasValue)
                ////{
                ////    query.Filter(fUnit => fUnit.PropertyInformations.Any(anyPropInfo => anyPropInfo.IsActive && anyPropInfo.Location != null && anyPropInfo.Location.IsActive && anyPropInfo.Location.Building != null && anyPropInfo.Location.Building.IsActive && anyPropInfo.Location.Building.BuildingId == buildingID.Value));
                ////}

                ////if (phaseID.HasValue)
                ////{
                ////    query.Filter(fUnit => fUnit.PropertyInformations.Any(anyPropInfo => anyPropInfo.IsActive && anyPropInfo.Location != null && anyPropInfo.Location.IsActive && anyPropInfo.Location.Phase != null && anyPropInfo.Location.Phase.IsActive && anyPropInfo.Location.Phase.PhaseId == phaseID.Value));
                ////}

                ////if (blockID.HasValue)
                ////{
                ////    query.Filter(fUnit => fUnit.PropertyInformations.Any(anyPropInfo => anyPropInfo.IsActive && anyPropInfo.Location != null && anyPropInfo.Location.IsActive && anyPropInfo.Location.Block != null && anyPropInfo.Location.Block.IsActive && anyPropInfo.Location.Block.BlockId == blockID.Value));
                ////}

                ////if (floorID.HasValue)
                ////{
                ////    query.Filter(fUnit => fUnit.PropertyInformations.Any(anyPropInfo => anyPropInfo.IsActive && anyPropInfo.Floor != null && anyPropInfo.Floor.IsActive && anyPropInfo.Floor.FloorId == floorID.Value));
                ////}

                //if (requirePaging)
                //{
                //    page = new Page(pageNumber.Value, pageSize.Value);
                //    data = await query.SelectPageAsync(page);
                //}
                //else
                //{
                //    data = await query.SelectAsync();
                //}

                //data = data.OrderBy(obUnit => !string.IsNullOrWhiteSpace(obUnit.UnitName) ? Regex.Replace(obUnit.UnitName, @"\d+", n => n.Value.PadLeft(4, '0')) : string.Empty);

                //return Mapper.Map<IEnumerable<UnitDropdownDto>>(data);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.GetExceptionErrorMessage());
                throw new Exception(ex.GetExceptionErrorMessage());
            }
        }
    }
}
