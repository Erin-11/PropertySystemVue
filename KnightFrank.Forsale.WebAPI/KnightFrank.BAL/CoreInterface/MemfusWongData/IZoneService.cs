using KnightFrank.BAL.Dtos.MemfusWongData;
using KnightFrank.DAL.Entities.Models.MemfusWongData;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KnightFrank.BAL.CoreInterface.MemfusWongData
{
    public interface IZoneService : IMemfusWongDataService<Zone>
    {
        Task<IEnumerable<ZoneDropdownDto>> GetZonesAsync();
        Task<IEnumerable<ZoneDropdownDto>> GetZonesAsync(int? pageNumber, int? pageSize);
    }
}
