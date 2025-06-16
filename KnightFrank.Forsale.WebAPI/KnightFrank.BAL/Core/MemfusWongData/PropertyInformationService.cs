using AutoMapper;
using KnightFrank.BAL.Common;
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
    public class PropertyInformationService : MemfusWongDataService<PropertyInformation>, IPropertyInformationService
    {
        public PropertyInformationService(ILogger<PropertyInformationService> logger, IMapper mapper, IMemfusWongDataBaseRepositoryAsync<PropertyInformation> repository)
            : base(logger, mapper, repository) { }

        //private const string FilterColumnZone = "zone";
        //private const string FilterColumnDistrict = "district";
        //private const string FilterColumnStreet = "street";
        //private const string FilterColumnEstate = "estate";
        //private const string FilterColumnBlock = "block";
        //private const string FilterColumnBuilding = "building";
        //private const string FilterColumnFloor = "floor";
        //private const string FilterColumnUnit = "unit";

        public async Task<IEnumerable<PropertyInformationDto>> GetPropertyInformationAsync()
            => await GetPropertyInformationAsync(1, 10);
        public async Task<IEnumerable<PropertyInformationDto>> GetPropertyInformationAsync(int? pageNumber, int? pageSize)
            => await GetPropertyInformationAsync(pageNumber, pageSize, null);
        public async Task<IEnumerable<PropertyInformationDto>> GetPropertyInformationAsync(int? pageNumber, int? pageSize,
            string filterZoneID = null)
            => await GetPropertyInformationAsync(pageNumber, pageSize, filterZoneID, null);
        public async Task<IEnumerable<PropertyInformationDto>> GetPropertyInformationAsync(int? pageNumber, int? pageSize,
            string filterZoneID = null, string filterDistrictID = null)
            => await GetPropertyInformationAsync(pageNumber, pageSize, filterZoneID, filterDistrictID, null);
        public async Task<IEnumerable<PropertyInformationDto>> GetPropertyInformationAsync(int? pageNumber, int? pageSize,
            string filterZoneID = null, string filterDistrictID = null, Guid? filterStreetID = null)
            => await GetPropertyInformationAsync(pageNumber, pageSize, filterZoneID, filterDistrictID, filterStreetID, null, null);
        public async Task<IEnumerable<PropertyInformationDto>> GetPropertyInformationAsync(int? pageNumber, int? pageSize,
            string filterZoneID = null, string filterDistrictID = null, Guid? filterStreetID = null, int? filterStreetNumberFrom = null, int? filterStreetNumberTo = null)
            => await GetPropertyInformationAsync(pageNumber, pageSize, filterZoneID, filterDistrictID, filterStreetID, filterStreetNumberFrom, filterStreetNumberTo, null);
        public async Task<IEnumerable<PropertyInformationDto>> GetPropertyInformationAsync(int? pageNumber, int? pageSize,
            string filterZoneID = null, string filterDistrictID = null, Guid? filterStreetID = null, int? filterStreetNumberFrom = null, int? filterStreetNumberTo = null, Guid? filterEstateID = null)
            => await GetPropertyInformationAsync(pageNumber, pageSize, filterZoneID, filterDistrictID, filterStreetID, filterStreetNumberFrom, filterStreetNumberTo, filterEstateID, null);
        public async Task<IEnumerable<PropertyInformationDto>> GetPropertyInformationAsync(int? pageNumber, int? pageSize,
            string filterZoneID = null, string filterDistrictID = null, Guid? filterStreetID = null, int? filterStreetNumberFrom = null, int? filterStreetNumberTo = null, Guid? filterEstateID = null, Guid? filterBuildingID = null)
            => await GetPropertyInformationAsync(pageNumber, pageSize, filterZoneID, filterDistrictID, filterStreetID, filterStreetNumberFrom, filterStreetNumberTo, filterEstateID, filterBuildingID, null);
        public async Task<IEnumerable<PropertyInformationDto>> GetPropertyInformationAsync(int? pageNumber, int? pageSize,
            string filterZoneID = null, string filterDistrictID = null, Guid? filterStreetID = null, int? filterStreetNumberFrom = null, int? filterStreetNumberTo = null, Guid? filterEstateID = null, Guid? filterBuildingID = null, int? filterPhaseID = null)
            => await GetPropertyInformationAsync(pageNumber, pageSize, filterZoneID, filterDistrictID, filterStreetID, filterStreetNumberFrom, filterStreetNumberTo, filterEstateID, filterBuildingID, filterPhaseID, null);
        public async Task<IEnumerable<PropertyInformationDto>> GetPropertyInformationAsync(int? pageNumber, int? pageSize,
            string filterZoneID = null, string filterDistrictID = null, Guid? filterStreetID = null, int? filterStreetNumberFrom = null, int? filterStreetNumberTo = null, Guid? filterEstateID = null, Guid? filterBuildingID = null, int? filterPhaseID = null, int? filterBlockID = null)
            => await GetPropertyInformationAsync(pageNumber, pageSize, filterZoneID, filterDistrictID, filterStreetID, filterStreetNumberFrom, filterStreetNumberTo, filterEstateID, filterBuildingID, filterPhaseID, filterBlockID, null);
        public async Task<IEnumerable<PropertyInformationDto>> GetPropertyInformationAsync(int? pageNumber, int? pageSize,
            string filterZoneID = null, string filterDistrictID = null, Guid? filterStreetID = null, int? filterStreetNumberFrom = null, int? filterStreetNumberTo = null, Guid? filterEstateID = null, Guid? filterBuildingID = null, int? filterPhaseID = null, int? filterBlockID = null, int? filterFloorID = null)
            => await GetPropertyInformationAsync(pageNumber, pageSize, filterZoneID, filterDistrictID, filterStreetID, filterStreetNumberFrom, filterStreetNumberTo, filterEstateID, filterBuildingID, filterPhaseID, filterBlockID, filterFloorID, null);
        public async Task<IEnumerable<PropertyInformationDto>> GetPropertyInformationAsync(int? pageNumber, int? pageSize,
            string filterZoneID = null, string filterDistrictID = null, Guid? filterStreetID = null, int? filterStreetNumberFrom = null, int? filterStreetNumberTo = null, Guid? filterEstateID = null, Guid? filterBuildingID = null, int? filterPhaseID = null, int? filterBlockID = null, int? filterFloorID = null, int? filterUnitID = null)
        {
            try
            {
                IEnumerable<PropertyInformation> data;

                bool requirePaging = pageNumber.HasValue && pageNumber.Value > 0 && pageSize.HasValue && pageSize.Value > 0;
                Page page = new Page(1, 10);

                var query = Query(e => e.IsActive);

                query.Filter(e => e.Location != null && e.Location.IsActive);

                query.Include(i => i.Location, z => ((Location)z).District.Zone);
                query.Include(i => i.Location, d => ((Location)d).District);
                query.Include(i => i.Location, s => ((Location)s).Street1);
                query.Include(i => i.Location, s => ((Location)s).Street2);
                query.Include(i => i.Location, e => ((Location)e).Estate);
                query.Include(i => i.Location, b => ((Location)b).Building);
                query.Include(i => i.Location, b => ((Location)b).Phase);
                query.Include(i => i.Location, b => ((Location)b).Block);
                query.Include(i => i.Floor);
                query.Include(i => i.Unit);

                if (!string.IsNullOrEmpty(filterZoneID))
                {
                    filterZoneID = filterZoneID.Trim();
                    query.Filter(e => e.Location.District != null && e.Location.District.IsActive && e.Location.District.Zone != null && e.Location.District.Zone.IsActive && e.Location.District.Zone.ZoneId == filterZoneID);
                }
                if (!string.IsNullOrEmpty(filterDistrictID))
                {
                    filterDistrictID = filterDistrictID.Trim();
                    query.Filter(e => e.Location.District != null && e.Location.District.IsActive && e.Location.District.DistrictId == filterDistrictID);
                }
                if (filterStreetID.HasValue)
                {
                    query.Filter(e => (e.Location.Street1 != null && e.Location.Street1.IsActive && e.Location.Street1.StreetId == filterStreetID.Value) || (e.Location.Street2 != null && e.Location.Street2.IsActive && e.Location.Street2.StreetId == filterStreetID.Value));
                }

                if (filterStreetNumberFrom.HasValue && filterStreetNumberTo.HasValue)
                {
                    query.Filter(e => (e.Location.Street1 != null && e.Location.Street1.IsActive && ((e.Location.StreetNumberFromNumeric1.HasValue && e.Location.StreetNumberFromNumeric1 >= filterStreetNumberFrom.Value && e.Location.StreetNumberFromNumeric1 <= filterStreetNumberTo.Value) || (e.Location.StreetNumberToNumeric1.HasValue && e.Location.StreetNumberToNumeric1 >= filterStreetNumberFrom.Value && e.Location.StreetNumberToNumeric1 <= filterStreetNumberTo.Value))) 
                                   || (e.Location.Street2 != null && e.Location.Street2.IsActive && ((e.Location.StreetNumberFromNumeric2.HasValue && e.Location.StreetNumberFromNumeric2 >= filterStreetNumberFrom.Value && e.Location.StreetNumberFromNumeric2 <= filterStreetNumberTo.Value) || (e.Location.StreetNumberToNumeric2.HasValue && e.Location.StreetNumberToNumeric2 >= filterStreetNumberFrom.Value && e.Location.StreetNumberToNumeric2 <= filterStreetNumberTo.Value))));
                }
                else if (filterStreetNumberFrom.HasValue)
                {
                    query.Filter(e => (e.Location.Street1 != null && e.Location.Street1.IsActive && ((e.Location.StreetNumberFromNumeric1.HasValue && e.Location.StreetNumberFromNumeric1 >= filterStreetNumberFrom.Value) || (e.Location.StreetNumberToNumeric1.HasValue && e.Location.StreetNumberToNumeric1 >= filterStreetNumberFrom.Value)))
                                   || (e.Location.Street2 != null && e.Location.Street2.IsActive && ((e.Location.StreetNumberFromNumeric2.HasValue && e.Location.StreetNumberFromNumeric2 >= filterStreetNumberFrom.Value) || (e.Location.StreetNumberToNumeric2.HasValue && e.Location.StreetNumberToNumeric2 >= filterStreetNumberFrom.Value))));
                }
                else if (filterStreetNumberTo.HasValue)
                {
                    query.Filter(e => (e.Location.Street1 != null && e.Location.Street1.IsActive && ((e.Location.StreetNumberFromNumeric1.HasValue && e.Location.StreetNumberFromNumeric1 <= filterStreetNumberTo.Value) || (e.Location.StreetNumberToNumeric1.HasValue && e.Location.StreetNumberToNumeric1 <= filterStreetNumberTo.Value)))
                                   || (e.Location.Street2 != null && e.Location.Street2.IsActive && ((e.Location.StreetNumberFromNumeric2.HasValue && e.Location.StreetNumberFromNumeric2 <= filterStreetNumberTo.Value) || (e.Location.StreetNumberToNumeric2.HasValue && e.Location.StreetNumberToNumeric2 <= filterStreetNumberTo.Value))));
                }

                if (filterEstateID.HasValue)
                {
                    query.Filter(e => e.Location.Estate != null && e.Location.Estate.IsActive && e.Location.Estate.EstateId == filterEstateID.Value);
                }
                if (filterBuildingID.HasValue)
                {
                    query.Filter(e => e.Location.Building != null && e.Location.Building.IsActive && e.Location.Building.BuildingId == filterBuildingID.Value);
                }
                if (filterPhaseID.HasValue)
                {
                    query.Filter(e => e.Location.Phase != null && e.Location.Phase.IsActive && e.Location.Phase.PhaseId == filterPhaseID.Value);
                }
                if (filterBlockID.HasValue)
                {
                    query.Filter(e => e.Location.Block != null && e.Location.Block.IsActive && e.Location.Block.BlockId == filterBlockID.Value);
                }
                if (filterFloorID.HasValue)
                {
                    query.Filter(e => e.Floor != null && e.Floor.IsActive && e.Floor.FloorId == filterFloorID.Value);
                }
                if (filterUnitID.HasValue)
                {
                    query.Filter(e => e.Unit != null && e.Unit.IsActive && e.Unit.UnitId == filterUnitID.Value);
                }

                //if (filterColumns != null && filterColumns.Any())
                //{
                //    if (!string.IsNullOrEmpty(filterSearch))
                //    {
                //        var _search = filterColumns.AsFilterColumnEnumerable<PropertyInformationDto>(LogicalOperatorEnum.Or, filterSearch);

                //        if (_search != null)
                //        {
                //            query.Filter(_search.ComposeFiltering<PropertyInformation>());

                //            if (_search.Any(e => e.FilterColumnName == FilterColumnDistrict ||
                //                                    e.FilterColumnName == FilterColumnEstate ||
                //                                    e.FilterColumnName == FilterColumnBlock ||
                //                                    e.FilterColumnName == FilterColumnBuilding ||
                //                                    e.FilterColumnName == FilterColumnFloor ||
                //                                    e.FilterColumnName == FilterColumnUnit))
                //            {
                //                query.Filter(e => (!string.IsNullOrEmpty(e.Location.District.Zone.ZoneName) && e.Location.District.Zone.ZoneName.Trim().ToLower().Replace(" ", string.Empty).Contains(filterSearch.Trim().ToLower().Replace(" ", string.Empty))) ||
                //                                    (!string.IsNullOrEmpty(e.Location.District.DistrictName) && e.Location.District.DistrictName.Trim().ToLower().Replace(" ", string.Empty).Contains(filterSearch.Trim().ToLower().Replace(" ", string.Empty))) ||
                //                                    (!string.IsNullOrEmpty(e.Location.Street1.StreetName) && e.Location.Street1.StreetName.Trim().ToLower().Replace(" ", string.Empty).Contains(filterSearch.Trim().ToLower().Replace(" ", string.Empty))) ||
                //                                    (!string.IsNullOrEmpty(e.Location.Street2.StreetName) && e.Location.Street2.StreetName.Trim().ToLower().Replace(" ", string.Empty).Contains(filterSearch.Trim().ToLower().Replace(" ", string.Empty))) ||
                //                                    (!string.IsNullOrEmpty(e.Location.Estate.EstateName) && e.Location.Estate.EstateName.Trim().ToLower().Replace(" ", string.Empty).Contains(filterSearch.Trim().ToLower().Replace(" ", string.Empty))) ||
                //                                    (!string.IsNullOrEmpty(e.Location.Building.BuildingName) && e.Location.Building.BuildingName.Trim().ToLower().Replace(" ", string.Empty).Contains(filterSearch.Trim().ToLower().Replace(" ", string.Empty))) ||
                //                                    (!string.IsNullOrEmpty(e.Location.Building.Block) && e.Location.Building.Block.Trim().ToLower().Replace(" ", string.Empty) == filterSearch.Trim().ToLower().Replace(" ", string.Empty)) ||
                //                                    (!string.IsNullOrEmpty(e.Floor.FloorName) && e.Floor.FloorName.Trim().ToLower().Replace(" ", string.Empty) == filterSearch.Trim().ToLower().Replace(" ", string.Empty)) ||
                //                                    (!string.IsNullOrEmpty(e.Unit.UnitName) && e.Unit.UnitName.Trim().ToLower().Replace(" ", string.Empty) == filterSearch.Trim().ToLower().Replace(" ", string.Empty)));
                //            }
                //        }
                //    }

                //    var _propertyFilters = filterColumns.AsFilterColumnEnumerable<PropertyInformationDto>(LogicalOperatorEnum.And);

                //    if (_propertyFilters != null)
                //    {
                //        query.Filter(_propertyFilters.ComposeFiltering<PropertyInformation>());

                //        var searchByZone = _propertyFilters.SingleOrDefault(e => e.FilterColumnName == FilterColumnZone);
                //        if (searchByZone != null && !string.IsNullOrEmpty(searchByZone.FilterText))
                //        {
                //            query.Filter(e => !string.IsNullOrEmpty(e.Location.District.Zone.ZoneName) && e.Location.District.Zone.ZoneName.Trim().ToLower().Replace(" ", string.Empty).Contains(searchByZone.FilterText.Trim().ToLower().Replace(" ", string.Empty)));
                //        }

                //        var searchByDistrict = _propertyFilters.SingleOrDefault(e => e.FilterColumnName == FilterColumnDistrict);
                //        if (searchByDistrict != null && !string.IsNullOrEmpty(searchByDistrict.FilterText))
                //        {
                //            query.Filter(e => !string.IsNullOrEmpty(e.Location.District.DistrictName) && e.Location.District.DistrictName.Trim().ToLower().Replace(" ", string.Empty).Contains(searchByDistrict.FilterText.Trim().ToLower().Replace(" ", string.Empty)));
                //        }

                //        var searchByStreet = _propertyFilters.SingleOrDefault(e => e.FilterColumnName == FilterColumnStreet);
                //        if (searchByStreet != null && !string.IsNullOrEmpty(searchByStreet.FilterText))
                //        {
                //            query.Filter(e => (!string.IsNullOrEmpty(e.Location.Street1.StreetName) && e.Location.Street1.StreetName.Trim().ToLower().Replace(" ", string.Empty).Contains(searchByStreet.FilterText))
                //            || (!string.IsNullOrEmpty(e.Location.Street2.StreetName) && e.Location.Street2.StreetName.Trim().ToLower().Replace(" ", string.Empty).Contains(searchByStreet.FilterText)));
                //        }

                //        var searchByEstate = _propertyFilters.SingleOrDefault(e => e.FilterColumnName == FilterColumnEstate);
                //        if (searchByEstate != null && !string.IsNullOrEmpty(searchByEstate.FilterText))
                //        {
                //            query.Filter(e => !string.IsNullOrEmpty(e.Location.Estate.EstateName) && e.Location.Estate.EstateName.Trim().ToLower().Replace(" ", string.Empty).Contains(searchByEstate.FilterText));
                //        }

                //        var searchByBuilding = _propertyFilters.SingleOrDefault(e => e.FilterColumnName == FilterColumnBuilding);
                //        if (searchByBuilding != null && !string.IsNullOrEmpty(searchByBuilding.FilterText))
                //        {
                //            query.Filter(e => !string.IsNullOrEmpty(e.Location.Building.BuildingName) && e.Location.Building.BuildingName.Trim().ToLower().Replace(" ", string.Empty).Contains(searchByBuilding.FilterText.Trim().ToLower().Replace(" ", string.Empty)));
                //        }

                //        var searchByBlock = _propertyFilters.SingleOrDefault(e => e.FilterColumnName == FilterColumnBlock);
                //        if (searchByBlock != null && !string.IsNullOrEmpty(searchByBlock.FilterText))
                //        {
                //            query.Filter(e => !string.IsNullOrEmpty(e.Location.Building.Block) && e.Location.Building.Block.Trim().ToLower().Replace(" ", string.Empty) == searchByBlock.FilterText.Trim().ToLower().Replace(" ", string.Empty));
                //        }

                //        var searchByFloor = _propertyFilters.SingleOrDefault(e => e.FilterColumnName == FilterColumnFloor);
                //        if (searchByFloor != null && !string.IsNullOrEmpty(searchByFloor.FilterText))
                //        {
                //            query.Filter(e => !string.IsNullOrEmpty(e.Floor.FloorName) && e.Floor.FloorName.Trim().ToLower().Replace(" ", string.Empty) == searchByFloor.FilterText.Trim().ToLower().Replace(" ", string.Empty));
                //        }

                //        var searchByUnit = _propertyFilters.SingleOrDefault(e => e.FilterColumnName == FilterColumnUnit);
                //        if (searchByUnit != null && !string.IsNullOrEmpty(searchByUnit.FilterText))
                //        {
                //            query.Filter(e => !string.IsNullOrEmpty(e.Unit.UnitName) && e.Unit.UnitName.Trim().ToLower().Replace(" ", string.Empty) == searchByUnit.FilterText.Trim().ToLower().Replace(" ", string.Empty));
                //        }
                //    }
                //}

                query.OrderBy(e => e.OrderBy(o => o.Location.District != null && o.Location.District.Zone != null && o.Location.District.Zone.ZoneName != null ? o.Location.District.Zone.ZoneName : string.Empty)
                                    .ThenBy(o => o.Location.District != null ? o.Location.District.DistrictName : string.Empty)
                                    .ThenBy(tb => tb.Location.Street1 != null ? tb.Location.Street1.StreetName : string.Empty)
                                    .ThenBy(tb => tb.Location.Street2 != null ? tb.Location.Street2.StreetName : string.Empty)
                                    .ThenBy(tb => tb.Location.Estate != null ? tb.Location.Estate.EstateName : string.Empty)
                                    .ThenBy(tb => tb.Location.Building != null ? tb.Location.Building.BuildingName : string.Empty)
                                    .ThenBy(tb => tb.Location.Phase != null ? tb.Location.Phase.PhaseName : string.Empty)
                                    .ThenBy(tb => tb.Location.Block != null ? tb.Location.Block.BlockName : string.Empty)
                                    .ThenBy(tb => tb.Floor != null ? tb.Floor.FloorName : string.Empty)
                                    .ThenBy(tb => tb.Unit != null ? tb.Unit.UnitName : string.Empty));


                if (requirePaging)
                {
                    page = new Page(pageNumber.Value, pageSize.Value);
                    data = await query.SelectPageAsync(page);
                }
                else
                {
                    data = await query.SelectAsync();
                }

                return data.Select(p => Mapper.Map<PropertyInformation, PropertyInformationDto>(p,
                    map => map.AfterMap(async (s, d) =>
                    {
                        var ppaQuery = await GetPPAQueryByInfo(s.Location?.Estate?.EstateName, s.Location?.Building?.BuildingName, s.Location?.Block?.BlockName, s.Floor?.FloorName, s.Unit?.UnitName);
                        var memNos = ppaQuery.Select(ppa => ppa.MemNo);
                        var mdbimpfls = await GetMDBIMPFLQuery(memNos);
                        var averageConsideration = mdbimpfls.Where(mdb => mdb.Considertion.HasValue).Average(a => a.Considertion);
                        d.AverageConsideration_MegaUnitFormat = averageConsideration.HasValue ? string.Format("{0:C4}", Math.Ceiling(averageConsideration.Value * 10000) / 10000) : string.Format("{0:C4}", 0);
                        d.AverageConsideration = averageConsideration.HasValue ? string.Format("{0:C0}", averageConsideration * 1000000) : string.Format("{0:C0}", 0);

                        var avgGrossPsf = mdbimpfls.Where(mdb => mdb.GrossPsf.HasValue).Average(a => a.GrossPsf);
                        var avgNetPsf = mdbimpfls.Where(mdb => mdb.NetPsf.HasValue).Average(a => a.NetPsf);
                        d.ValuationHKD_GFA = s.Gfa.HasValue && s.Gfa > 0 ? string.Format("{0:C0}", averageConsideration * 1000000 / s.Gfa) : string.Format("{0:C0}", 0);
                        d.ValuationHKD_NFA = s.Nfa.HasValue && s.Nfa > 0 ? string.Format("{0:C0}", averageConsideration * 1000000 / s.Nfa) : string.Format("{0:C0}", 0);
                        d.ValuationDate = DateTime.Now.ToDisplayDate();
                    })));
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.GetExceptionErrorMessage());
                throw new Exception(ex.GetExceptionErrorMessage());
            }
        }
    }
}
