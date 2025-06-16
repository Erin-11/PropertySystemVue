using KnightFrank.BAL.Dtos.MemfusWongData;
using KnightFrank.DAL.Entities.Models.MemfusWongData;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KnightFrank.BAL.CoreInterface.MemfusWongData
{
    public interface IFloorService : IMemfusWongDataService<Floor>
    {
        Task<IEnumerable<FloorDropdownDto>> SearchFloorAsync();
        Task<IEnumerable<FloorDropdownDto>> SearchFloorAsync(int? pageNumber, int? pageSize);
        Task<IEnumerable<FloorDropdownDto>> SearchFloorAsync(int? pageNumber, int? pageSize, string zoneID);
        Task<IEnumerable<FloorDropdownDto>> SearchFloorAsync(int? pageNumber, int? pageSize, string zoneID, string districtID);
        Task<IEnumerable<FloorDropdownDto>> SearchFloorAsync(int? pageNumber, int? pageSize, string zoneID, string districtID, Guid? streetID);
        Task<IEnumerable<FloorDropdownDto>> SearchFloorAsync(int? pageNumber, int? pageSize, string zoneID, string districtID, Guid? streetID, Guid? estateID);
        Task<IEnumerable<FloorDropdownDto>> SearchFloorAsync(int? pageNumber, int? pageSize, string zoneID, string districtID, Guid? streetID, Guid? estateID, Guid? buildingID);
        Task<IEnumerable<FloorDropdownDto>> SearchFloorAsync(int? pageNumber, int? pageSize, string zoneID, string districtID, Guid? streetID, Guid? estateID, Guid? buildingID, int? phaseID);
        Task<IEnumerable<FloorDropdownDto>> SearchFloorAsync(int? pageNumber, int? pageSize, string zoneID, string districtID, Guid? streetID, Guid? estateID, Guid? buildingID, int? phaseID, int? BlockID);
    }
}
