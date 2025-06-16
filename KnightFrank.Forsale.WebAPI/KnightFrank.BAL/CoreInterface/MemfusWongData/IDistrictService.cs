using KnightFrank.BAL.Dtos.MemfusWongData;
using KnightFrank.DAL.Entities.Models.MemfusWongData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightFrank.BAL.CoreInterface.MemfusWongData
{
    public interface IDistrictService : IMemfusWongDataService<District>
    {
        Task<IEnumerable<DistrictDropdownDto>> GetDistrictsAsync();
        Task<IEnumerable<DistrictDropdownDto>> GetDistrictsAsync(int? pageNumber, int? pageSize);
        Task<IEnumerable<DistrictDropdownDto>> GetDistrictsAsync(int? pageNumber, int? pageSize, string zoneID);
    }
}
