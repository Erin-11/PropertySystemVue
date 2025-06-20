using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnightFrank.BAL.Dtos.MemfusWongData;
using KnightFrank.DataAccessLayer.EF;

namespace KnightFrank.BAL.CoreInterface.MemfusWongData
{
    public interface IPropertyService
    {
        Task<PagedResultDto<PropertyDto>> SearchPropertiesAsync();
        Task<PagedResultDto<PropertyDto>> SearchPropertiesAsync(string? region, string? district, List<string>? propertyTypes, decimal? minPrice, decimal? maxPrice, int? page, int? pageSize);
    }
}
