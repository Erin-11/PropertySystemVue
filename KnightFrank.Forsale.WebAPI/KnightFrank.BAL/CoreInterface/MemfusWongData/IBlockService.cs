using KnightFrank.BAL.Dtos.MemfusWongData;
using KnightFrank.DAL.Entities.Models.MemfusWongData;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KnightFrank.BAL.CoreInterface.MemfusWongData
{
    public interface IBlockService : IMemfusWongDataService<Block>
    {
        Task<IEnumerable<BlockDropdownDto>> SearchBlocksAsync();
        Task<IEnumerable<BlockDropdownDto>> SearchBlocksAsync(int? pageNumber, int? pageSize);
        Task<IEnumerable<BlockDropdownDto>> SearchBlocksAsync(int? pageNumber, int? pageSize, string zoneID);
        Task<IEnumerable<BlockDropdownDto>> SearchBlocksAsync(int? pageNumber, int? pageSize, string zoneID, string districtID);
        Task<IEnumerable<BlockDropdownDto>> SearchBlocksAsync(int? pageNumber, int? pageSize, string zoneID, string districtID, Guid? streetID, string streetNumberFrom, string streetNumberTo);
        Task<IEnumerable<BlockDropdownDto>> SearchBlocksAsync(int? pageNumber, int? pageSize, string zoneID, string districtID, Guid? streetID, string streetNumberFrom, string streetNumberTo, Guid? estateID);
        Task<IEnumerable<BlockDropdownDto>> SearchBlocksAsync(int? pageNumber, int? pageSize, string zoneID, string districtID, Guid? streetID, string streetNumberFrom, string streetNumberTo, Guid? estateID, Guid? buildingId);
        Task<IEnumerable<BlockDropdownDto>> SearchBlocksAsync(int? pageNumber, int? pageSize, string zoneID, string districtID, Guid? streetID, string streetNumberFrom, string streetNumberTo, Guid? estateID, Guid? buildingId, int? phaseId);
    }
}
