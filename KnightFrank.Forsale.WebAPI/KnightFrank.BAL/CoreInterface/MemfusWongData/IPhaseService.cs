using KnightFrank.BAL.Dtos.MemfusWongData;
using KnightFrank.DAL.Entities.Models.MemfusWongData;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KnightFrank.BAL.CoreInterface.MemfusWongData
{
    public interface IPhaseService : IMemfusWongDataService<Phase>
    {
        Task<IEnumerable<PhaseDropdownDto>> SearchPhasesAsync();
        Task<IEnumerable<PhaseDropdownDto>> SearchPhasesAsync(int? pageNumber, int? pageSize);
        Task<IEnumerable<PhaseDropdownDto>> SearchPhasesAsync(int? pageNumber, int? pageSize, string zoneID);
        Task<IEnumerable<PhaseDropdownDto>> SearchPhasesAsync(int? pageNumber, int? pageSize, string zoneID, string districtID);
        Task<IEnumerable<PhaseDropdownDto>> SearchPhasesAsync(int? pageNumber, int? pageSize, string zoneID, string districtID, Guid? streetID, string streetNumberFrom, string streetNumberTo);
        Task<IEnumerable<PhaseDropdownDto>> SearchPhasesAsync(int? pageNumber, int? pageSize, string zoneID, string districtID, Guid? streetID, string streetNumberFrom, string streetNumberTo, Guid? estateID);
        Task<IEnumerable<PhaseDropdownDto>> SearchPhasesAsync(int? pageNumber, int? pageSize, string zoneID, string districtID, Guid? streetID, string streetNumberFrom, string streetNumberTo, Guid? estateID, Guid? buildingId);
    }
}
