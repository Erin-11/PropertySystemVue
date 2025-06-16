using KnightFrank.BAL.Dtos.MemfusWongData;
using KnightFrank.DAL.Entities.Models.MemfusWongData;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KnightFrank.BAL.CoreInterface.MemfusWongData
{
    public interface IUnitService : IMemfusWongDataService<Unit>
    {
        Task<IEnumerable<UnitDropdownDto>> SearchUnitAsync();
        Task<IEnumerable<UnitDropdownDto>> SearchUnitAsync(int? pageNumber, int? pageSize);
        Task<IEnumerable<UnitDropdownDto>> SearchUnitAsync(int? pageNumber, int? pageSize, string zoneID);
        Task<IEnumerable<UnitDropdownDto>> SearchUnitAsync(int? pageNumber, int? pageSize, string zoneID, string districtID);
        Task<IEnumerable<UnitDropdownDto>> SearchUnitAsync(int? pageNumber, int? pageSize, string zoneID, string districtID, Guid? streetID);
        Task<IEnumerable<UnitDropdownDto>> SearchUnitAsync(int? pageNumber, int? pageSize, string zoneID, string districtID, Guid? streetID, Guid? estateID);
        Task<IEnumerable<UnitDropdownDto>> SearchUnitAsync(int? pageNumber, int? pageSize, string zoneID, string districtID, Guid? streetID, Guid? estateID, Guid? buildingID);
        Task<IEnumerable<UnitDropdownDto>> SearchUnitAsync(int? pageNumber, int? pageSize, string zoneID, string districtID, Guid? streetID, Guid? estateID, Guid? buildingID, int? phaseID);
        Task<IEnumerable<UnitDropdownDto>> SearchUnitAsync(int? pageNumber, int? pageSize, string zoneID, string districtID, Guid? streetID, Guid? estateID, Guid? buildingID, int? phaseID, int? blockID);
        Task<IEnumerable<UnitDropdownDto>> SearchUnitAsync(int? pageNumber, int? pageSize, string zoneID, string districtID, Guid? streetID, Guid? estateID, Guid? buildingID, int? phaseID, int? blockID, int? floorID);
    }
}
