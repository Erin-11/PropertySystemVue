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

namespace KnightFrank.BAL.Core.MemfusWongData
{
    public class FloorService : MemfusWongDataService<Floor>, IFloorService
    {
        public FloorService(ILogger<MemfusWongDataService<Floor>> logger, IMapper mapper, IMemfusWongDataBaseRepositoryAsync<Floor> repository)
           : base(logger, mapper, repository) { }
        
        public async Task<IEnumerable<FloorDropdownDto>> SearchFloorAsync()
            => await SearchFloorAsync(1, 10);
        public async Task<IEnumerable<FloorDropdownDto>> SearchFloorAsync(int? pageNumber, int? pageSize)
            => await SearchFloorAsync(pageNumber, pageSize, null);
        public async Task<IEnumerable<FloorDropdownDto>> SearchFloorAsync(int? pageNumber, int? pageSize, string zoneID)
            => await SearchFloorAsync(pageNumber, pageSize, zoneID, null);
        public async Task<IEnumerable<FloorDropdownDto>> SearchFloorAsync(int? pageNumber, int? pageSize, string zoneID, string districtID)
            => await SearchFloorAsync(pageNumber, pageSize, zoneID, districtID, null);
        public async Task<IEnumerable<FloorDropdownDto>> SearchFloorAsync(int? pageNumber, int? pageSize, string zoneID, string districtID, Guid? streetID)
            => await SearchFloorAsync(pageNumber, pageSize, zoneID, districtID, streetID, null);
        public async Task<IEnumerable<FloorDropdownDto>> SearchFloorAsync(int? pageNumber, int? pageSize, string zoneID, string districtID, Guid? streetID, Guid? estateID)
            => await SearchFloorAsync(pageNumber, pageSize, zoneID, districtID, streetID, estateID, null);
        public async Task<IEnumerable<FloorDropdownDto>> SearchFloorAsync(int? pageNumber, int? pageSize, string zoneID, string districtID, Guid? streetID, Guid? estateID, Guid? buildingID)
            => await SearchFloorAsync(pageNumber, pageSize, zoneID, districtID, streetID, estateID, buildingID, null);
        public async Task<IEnumerable<FloorDropdownDto>> SearchFloorAsync(int? pageNumber, int? pageSize, string zoneID, string districtID, Guid? streetID, Guid? estateID, Guid? buildingID, int? phaseID)
            => await SearchFloorAsync(pageNumber, pageSize, zoneID, districtID, streetID, estateID, buildingID, phaseID, null);
        public async Task<IEnumerable<FloorDropdownDto>> SearchFloorAsync(int? pageNumber, int? pageSize, string zoneID, string districtID, Guid? streetID, Guid? estateID, Guid? buildingID, int? phaseID, int? blockID)
        {
            try
            {
                var query = GetRepository<PropertyInformation>().Query(e => e.IsActive);

                query.Filter(f => f.Location != null && f.Location.IsActive);
                query.Filter(f => f.Location.District != null && f.Location.District.IsActive);
                query.Filter(f => f.Location.District.Zone != null && f.Location.District.Zone.IsActive);

                query.Filter(f => f.Floor != null && f.Floor.IsActive);

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

                var data = await query.SelectAsync(s => s.Floor);
                data = data.GroupBy(g => g.FloorId)
                    .Select(s => s.FirstOrDefault())
                    .OrderBy(ob => !string.IsNullOrWhiteSpace(ob.FloorName) ? Regex.Replace(ob.FloorName, @"\d+", n => n.Value.PadLeft(4, '0')) : string.Empty);

                bool requirePaging = pageNumber.HasValue && pageNumber.Value > 0 && pageSize.HasValue && pageSize.Value > 0;
                if (requirePaging)
                {
                    data = data.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value);
                }

                return Mapper.Map<IEnumerable<FloorDropdownDto>>(data);


                //IEnumerable<Floor> data;

                //bool requirePaging = pageNumber.HasValue && pageNumber.Value > 0 && pageSize.HasValue && pageSize.Value > 0;
                //var page = new Page(1, 10);

                //var query = GetRepository<Floor>().Query(e => e.IsActive);

                ////query.Include(i => i.PropertyInformations, p => ((PropertyInformation)p).Location.District);
                ////query.Include(i => i.PropertyInformations, p => ((PropertyInformation)p).Location.District.Zone);
                ////query.Include(i => i.PropertyInformations, p => ((PropertyInformation)p).Location.Street1);
                ////query.Include(i => i.PropertyInformations, p => ((PropertyInformation)p).Location.Street2);
                ////query.Include(i => i.PropertyInformations, p => ((PropertyInformation)p).Location.Estate);
                ////query.Include(i => i.PropertyInformations, p => ((PropertyInformation)p).Location.Building);
                ////query.Include(i => i.PropertyInformations, p => ((PropertyInformation)p).Location.Phase);
                ////query.Include(i => i.PropertyInformations, p => ((PropertyInformation)p).Location.Block);
                ////query.Include(i => i.PropertyInformations, p => ((PropertyInformation)p).Unit);

                //query.Filter(fFloor => fFloor.PropertyInformations != null && fFloor.PropertyInformations.Any(anyPropInfo => anyPropInfo.IsActive));

                //if (!string.IsNullOrWhiteSpace(zoneID))
                //{
                //    query.Filter(fFloor => fFloor.PropertyInformations.Any(anyPropInfo => anyPropInfo.IsActive && anyPropInfo.Location != null && anyPropInfo.Location.IsActive && anyPropInfo.Location.District != null && anyPropInfo.Location.District.IsActive && anyPropInfo.Location.District.Zone != null && anyPropInfo.Location.District.Zone.IsActive && anyPropInfo.Location.District.Zone.ZoneId == zoneID));
                //}

                //if (!string.IsNullOrWhiteSpace(districtID))
                //{
                //    query.Filter(fFloor => fFloor.PropertyInformations.Any(anyPropInfo => anyPropInfo.IsActive && anyPropInfo.Location != null && anyPropInfo.Location.IsActive && anyPropInfo.Location.District != null && anyPropInfo.Location.District.IsActive && anyPropInfo.Location.District.DistrictId == districtID));
                //}

                ////if (streetID.HasValue)
                ////{
                ////    query.Filter(fFloor => fFloor.PropertyInformations.Any(anyPropInfo => anyPropInfo.IsActive && anyPropInfo.Location != null && anyPropInfo.Location.IsActive &&
                ////    ((anyPropInfo.Location.Street1 != null && anyPropInfo.Location.Street1.IsActive && anyPropInfo.Location.Street1.StreetId == streetID.Value)
                ////    || (anyPropInfo.Location.Street2 != null && anyPropInfo.Location.Street2.IsActive && anyPropInfo.Location.Street2.StreetId == streetID.Value))));
                ////}

                //if (estateID.HasValue)
                //{
                //    query.Filter(fFloor => fFloor.PropertyInformations.Any(anyPropInfo => anyPropInfo.IsActive && anyPropInfo.Location != null && anyPropInfo.Location.IsActive && anyPropInfo.Location.Estate != null && anyPropInfo.Location.Estate.IsActive && anyPropInfo.Location.Estate.EstateId == estateID.Value));
                //}

                //if (buildingID.HasValue)
                //{
                //    query.Filter(fFloor => fFloor.PropertyInformations.Any(anyPropInfo => anyPropInfo.IsActive && anyPropInfo.Location != null && anyPropInfo.Location.IsActive && anyPropInfo.Location.Building != null && anyPropInfo.Location.Building.IsActive && anyPropInfo.Location.Building.BuildingId == buildingID.Value));
                //}

                //if (phaseID.HasValue)
                //{
                //    query.Filter(fFloor => fFloor.PropertyInformations.Any(anyPropInfo => anyPropInfo.IsActive && anyPropInfo.Location != null && anyPropInfo.Location.IsActive && anyPropInfo.Location.Phase != null && anyPropInfo.Location.Phase.IsActive && anyPropInfo.Location.Phase.PhaseId == phaseID.Value));
                //}

                //if (blockID.HasValue)
                //{
                //    query.Filter(fFloor => fFloor.PropertyInformations.Any(anyPropInfo => anyPropInfo.IsActive && anyPropInfo.Location != null && anyPropInfo.Location.IsActive && anyPropInfo.Location.Block != null && anyPropInfo.Location.Block.IsActive && anyPropInfo.Location.Block.BlockId == blockID.Value));
                //}

                //if (requirePaging)
                //{
                //    page = new Page(pageNumber.Value, pageSize.Value);
                //    data = await query.SelectPageAsync(page);
                //}
                //else
                //{
                //    data = await query.SelectAsync();
                //}

                //data = data.OrderBy(obFloor => !string.IsNullOrWhiteSpace(obFloor.FloorName) ? Regex.Replace(obFloor.FloorName, @"\d+", n => n.Value.PadLeft(4, '0')) : string.Empty);

                //return Mapper.Map<IEnumerable<FloorDropdownDto>>(data);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.GetExceptionErrorMessage());
                throw new Exception(ex.GetExceptionErrorMessage());
            }
        }
    }
}
