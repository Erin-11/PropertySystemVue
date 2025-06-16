using KnightFrank.BAL.Dtos.MemfusWongData;
using KnightFrank.DAL.Entities.Models.MemfusWongData;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KnightFrank.BAL.CoreInterface.MemfusWongData
{
    public interface IBuildingService : IMemfusWongDataService<Building>
    {
        Task<IEnumerable<BuildingDropdownDto>> SearchBuildingsAsync();
        Task<IEnumerable<BuildingDropdownDto>> SearchBuildingsAsync(int? pageNumber, int? pageSize);
        Task<IEnumerable<BuildingDropdownDto>> SearchBuildingsAsync(int? pageNumber, int? pageSize, string zoneID);
        Task<IEnumerable<BuildingDropdownDto>> SearchBuildingsAsync(int? pageNumber, int? pageSize, string zoneID, string districtID);
        Task<IEnumerable<BuildingDropdownDto>> SearchBuildingsAsync(int? pageNumber, int? pageSize, string zoneID, string districtID, Guid? streetID, string streetNumberFrom, string streetNumberTo);
        Task<IEnumerable<BuildingDropdownDto>> SearchBuildingsAsync(int? pageNumber, int? pageSize, string zoneID, string districtID, Guid? streetID, string streetNumberFrom, string streetNumberTo, Guid? estateID);
        Task<IEnumerable<BuildingDropdownDto>> SearchBuildingsAsync(int? pageNumber, int? pageSize, string zoneID, string districtID, Guid? streetID, string streetNumberFrom, string streetNumberTo, Guid? estateID, Guid? buildingID);
    }
}
