using KnightFrank.BAL.Dtos.MemfusWongData;
using KnightFrank.DAL.Entities.Models.MemfusWongData;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KnightFrank.BAL.CoreInterface.MemfusWongData
{
    public interface IEstateService : IMemfusWongDataService<Estate>
    {
        Task<IEnumerable<EstateDropdownDto>> GetEstatesAsync();
        Task<IEnumerable<EstateDropdownDto>> GetEstatesAsync(int? pageNumber, int? pageSize);
        Task<IEnumerable<EstateDropdownDto>> GetEstatesAsync(int? pageNumber, int? pageSize, string zoneID);
        Task<IEnumerable<EstateDropdownDto>> GetEstatesAsync(int? pageNumber, int? pageSize, string zoneID, string districtID);
        Task<IEnumerable<EstateDropdownDto>> GetEstatesAsync(int? pageNumber, int? pageSize, string zoneID, string districtID, Guid? streetID, string streetNumberFrom, string streetNumberTo);
        Task<IEnumerable<EstateDropdownDto>> GetEstatesAsync(int? pageNumber, int? pageSize, string zoneID, string districtID, Guid? streetID, string streetNumberFrom, string streetNumberTo, Guid? estateID);
        Task<IEnumerable<EstateDropdownDto>> GetEstatesAsync(int? pageNumber, int? pageSize, string zoneID, string districtID, Guid? streetID, string streetNumberFrom, string streetNumberTo, Guid? estateID, Guid? buildingID);
    }
}
