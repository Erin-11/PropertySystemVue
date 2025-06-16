using AutoMapper;
using KnightFrank.BAL.CoreInterface.MemfusWongData;
using KnightFrank.BAL.Dtos.MemfusWongData;
using KnightFrank.BAL.Extensions;
using KnightFrank.DAL.Entities.Models.MemfusWongData;
using KnightFrank.DAL;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnightFrank.DataAccessLayer.EF.Common;

namespace KnightFrank.BAL.Core.MemfusWongData
{
    public class PhaseService : MemfusWongDataService<Phase>, IPhaseService
    {
        public PhaseService(ILogger<MemfusWongDataService<Phase>> logger, IMapper mapper, IMemfusWongDataBaseRepositoryAsync<Phase> repository)
            : base(logger, mapper, repository) { }

        public async Task<IEnumerable<PhaseDropdownDto>> SearchPhasesAsync()
            => await SearchPhasesAsync(1, 10);
        public async Task<IEnumerable<PhaseDropdownDto>> SearchPhasesAsync(int? pageNumber, int? pageSize)
            => await SearchPhasesAsync(pageNumber, pageSize, null);
        public async Task<IEnumerable<PhaseDropdownDto>> SearchPhasesAsync(int? pageNumber, int? pageSize, string zoneID)
            => await SearchPhasesAsync(pageNumber, pageSize, zoneID, null);
        public async Task<IEnumerable<PhaseDropdownDto>> SearchPhasesAsync(int? pageNumber, int? pageSize, string zoneID, string districtID)
            => await SearchPhasesAsync(pageNumber, pageSize, zoneID, districtID, null, null, null);
        public async Task<IEnumerable<PhaseDropdownDto>> SearchPhasesAsync(int? pageNumber, int? pageSize, string zoneID, string districtID, Guid? streetID, string streetNumberFrom, string streetNumberTo)
            => await SearchPhasesAsync(pageNumber, pageSize, zoneID, districtID, streetID, streetNumberFrom, streetNumberTo, null);
        public async Task<IEnumerable<PhaseDropdownDto>> SearchPhasesAsync(int? pageNumber, int? pageSize, string zoneID, string districtID, Guid? streetID, string streetNumberFrom, string streetNumberTo, Guid? estateID)
            => await SearchPhasesAsync(pageNumber, pageSize, zoneID, districtID, streetID, streetNumberFrom, streetNumberTo, estateID, null);
        public async Task<IEnumerable<PhaseDropdownDto>> SearchPhasesAsync(int? pageNumber, int? pageSize, string zoneID, string districtID, Guid? streetID, string streetNumberFrom, string streetNumberTo, Guid? estateID, Guid? buildingID)
        {
            try
            {
                IEnumerable<Phase> data;

                bool requirePaging = pageNumber.HasValue && pageNumber.Value > 0 && pageSize.HasValue && pageSize.Value > 0;
                var page = new Page(1, 10);

                var query = Query(e => e.IsActive);

                query.Filter(fPhase => fPhase.Locations != null && fPhase.Locations.Any(anyLocation => anyLocation.IsActive));

                if (!string.IsNullOrWhiteSpace(zoneID))
                {
                    zoneID = zoneID.Trim();
                    query.Filter(fPhase => fPhase.Locations.Any(anyLocation => anyLocation.IsActive && anyLocation.District != null && anyLocation.District.IsActive && anyLocation.District.Zone != null && anyLocation.District.Zone.IsActive && anyLocation.District.Zone.ZoneId == zoneID));
                }

                if (!string.IsNullOrWhiteSpace(districtID))
                {
                    districtID = districtID.Trim();
                    query.Filter(fPhase => fPhase.Locations.Any(anyLocation => anyLocation.IsActive && anyLocation.District != null && anyLocation.District.IsActive && anyLocation.DistrictId == districtID));
                }

                if (streetID.HasValue)
                {
                    query.Filter(fPhase => fPhase.Locations.Any(anyLocation => anyLocation.IsActive && (anyLocation.Street1 != null && anyLocation.Street1.IsActive && anyLocation.Street1.StreetId == streetID.Value)
                    || (anyLocation.Street2 != null && anyLocation.Street2.IsActive && anyLocation.Street2.StreetId == streetID.Value)));
                }

                if (estateID.HasValue)
                {
                    query.Filter(fPhase => fPhase.Locations.Any(anyLocation => anyLocation.IsActive && anyLocation.Estate != null && anyLocation.Estate.IsActive && anyLocation.Estate.EstateId == estateID.Value));
                }

                if (buildingID.HasValue)
                {
                    query.Filter(fPhase => fPhase.Locations.Any(anyLocation => anyLocation.IsActive && anyLocation.Building != null && anyLocation.Building.IsActive && anyLocation.Building.BuildingId == buildingID.Value));
                }

                query.Filter(fPhase => (fPhase.Locations.Any(anyLocation => anyLocation.IsActive && anyLocation.PropertyInformations != null && anyLocation.PropertyInformations.Any(anyPropInfo => anyPropInfo.IsActive))));

                query.OrderBy(obQuery => obQuery.OrderBy(obPhase => !string.IsNullOrWhiteSpace(obPhase.PhaseName) ? obPhase.PhaseName : string.Empty));


                if (requirePaging)
                {
                    page = new Page(pageNumber.Value, pageSize.Value);
                    data = await query.SelectPageAsync(page);
                }
                else
                {
                    data = await query.SelectAsync();
                }


                return Mapper.Map<IEnumerable<PhaseDropdownDto>>(data);



                //var query = GetRepository<PropertyInformation>().Query(e => e.IsActive);

                //query.Filter(f => f.Location != null && f.Location.IsActive);
                //query.Filter(f => f.Location.District != null && f.Location.District.IsActive);
                //query.Filter(f => f.Location.District.Zone != null && f.Location.District.Zone.IsActive);
                //query.Filter(f => f.Location.Street1 != null && f.Location.Street1.IsActive);
                //query.Filter(f => f.Location.Street2 != null && f.Location.Street2.IsActive);
                //query.Filter(f => f.Location.Estate != null && f.Location.Estate.IsActive);
                //query.Filter(f => f.Location.Building != null && f.Location.Building.IsActive);

                //query.Filter(f => f.Location.Phase != null && f.Location.Phase.IsActive);

                //if (!string.IsNullOrWhiteSpace(zoneID))
                //{
                //    query.Filter(f => f.Location.District.ZoneId == zoneID);
                //}

                //if (!string.IsNullOrWhiteSpace(districtID))
                //{
                //    query.Filter(f => f.Location.DistrictId == districtID);
                //}

                //if (streetID.HasValue)
                //{
                //    query.Filter(f => (f.Location.Street1Id.HasValue && f.Location.Street1Id == streetID) || (f.Location.Street2Id.HasValue && f.Location.Street2Id == streetID));
                //}

                //if (estateID.HasValue)
                //{
                //    query.Filter(f => f.Location.EstateId == estateID);
                //}

                //if (buildingID.HasValue)
                //{
                //    query.Filter(f => f.Location.BuildingId == buildingID);
                //}

                //var data = await query.SelectAsync(s => s.Location.Phase);
                //data = data.GroupBy(g => g.PhaseId)
                //    .Select(s => s.FirstOrDefault())
                //    .OrderBy(ob => ob.PhaseName);

                //bool requirePaging = pageNumber.HasValue && pageNumber.Value >= 0 && pageSize.HasValue && pageSize.Value > 0;
                //if (requirePaging)
                //{
                //    data = data.Skip(pageNumber.Value * pageSize.Value).Take(pageSize.Value);
                //}

                //return Mapper.Map<IEnumerable<PhaseDropdownDto>>(data);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.GetExceptionErrorMessage());
                throw new Exception(ex.GetExceptionErrorMessage());
            }
        }
    }
}
