using KnightFrank.BAL.Dtos.MemfusWongData;
using KnightFrank.DAL.Entities.Models.MemfusWongData;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KnightFrank.BAL.CoreInterface.MemfusWongData
{
    public interface ILocationService : IMemfusWongDataService<Location>
    {
        Task<IEnumerable<LocationFilterDto>> SearchLocationsAsync();
        Task<IEnumerable<LocationFilterDto>> SearchLocationsAsync(int? pageNumber, int? pageSize);
        Task<IEnumerable<LocationFilterDto>> SearchLocationsAsync(int? pageNumber, int? pageSize, string zoneID);
        Task<IEnumerable<LocationFilterDto>> SearchLocationsAsync(int? pageNumber, int? pageSize, string zoneID, string districtID);
        Task<IEnumerable<LocationFilterDto>> SearchLocationsAsync(int? pageNumber, int? pageSize, string zoneID, string districtID, string streetNameKeyword);
        Task<IEnumerable<LocationFilterDto>> SearchLocationsAsync(int? pageNumber, int? pageSize, string zoneID, string districtID, string streetNameKeyword, string estateNameKeyword);
        Task<IEnumerable<LocationFilterDto>> SearchLocationsAsync(int? pageNumber, int? pageSize, string zoneID, string districtID, string streetNameKeyword, string estateNameKeyword, string buildingNameKeyword);
    }
}
