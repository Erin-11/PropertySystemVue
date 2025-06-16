using KnightFrank.BAL.Dtos.MemfusWongData;
using KnightFrank.DAL.Entities.Models.MemfusWongData;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KnightFrank.BAL.CoreInterface.MemfusWongData
{
    public interface IStreetService : IMemfusWongDataService<Street>
    {
        Task<IEnumerable<StreetDropdownDto>> GetStreetsAsync();
        Task<IEnumerable<StreetDropdownDto>> GetStreetsAsync(int? pageNumber, int? pageSize);
        Task<IEnumerable<StreetDropdownDto>> GetStreetsAsync(int? pageNumber, int? pageSize, string zoneID);
        Task<IEnumerable<StreetDropdownDto>> GetStreetsAsync(int? pageNumber, int? pageSize, string zoneID, string districtID);
        Task<IEnumerable<StreetDropdownDto>> GetStreetsAsync(int? pageNumber, int? pageSize, string zoneID, string districtID, Guid? streetID, string streetNumberFrom, string streetNumberTo);
        Task<IEnumerable<StreetDropdownDto>> GetStreetsAsync(int? pageNumber, int? pageSize, string zoneID, string districtID, Guid? streetID, string streetNumberFrom, string streetNumberTo, Guid? estateID);
        Task<IEnumerable<StreetDropdownDto>> GetStreetsAsync(int? pageNumber, int? pageSize, string zoneID, string districtID, Guid? streetID, string streetNumberFrom, string streetNumberTo, Guid? estateID, Guid? buildingID);
    }
}
