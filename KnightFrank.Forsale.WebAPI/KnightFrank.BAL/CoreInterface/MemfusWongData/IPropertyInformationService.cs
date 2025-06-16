using KnightFrank.BAL.Dtos.MemfusWongData;
using KnightFrank.DAL.Entities.Models.MemfusWongData;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KnightFrank.BAL.CoreInterface.MemfusWongData
{
    public interface IPropertyInformationService : IMemfusWongDataService<PropertyInformation>
    {
        Task<IEnumerable<PropertyInformationDto>> GetPropertyInformationAsync();
        Task<IEnumerable<PropertyInformationDto>> GetPropertyInformationAsync(int? pageNumber, int? pageSize);

        Task<IEnumerable<PropertyInformationDto>> GetPropertyInformationAsync(int? pageNumber, int? pageSize,
            string filterZoneID = null);
        Task<IEnumerable<PropertyInformationDto>> GetPropertyInformationAsync(int? pageNumber, int? pageSize,
            string filterZoneID = null, string filterDistrictID = null);
        Task<IEnumerable<PropertyInformationDto>> GetPropertyInformationAsync(int? pageNumber, int? pageSize,
            string filterZoneID = null, string filterDistrictID = null, Guid? filterStreetID = null);
        Task<IEnumerable<PropertyInformationDto>> GetPropertyInformationAsync(int? pageNumber, int? pageSize,
            string filterZoneID = null, string filterDistrictID = null, Guid? filterStreetID = null, int? filterStreetNumberFrom = null, int? filterStreetNumberTo = null);
        Task<IEnumerable<PropertyInformationDto>> GetPropertyInformationAsync(int? pageNumber, int? pageSize,
            string filterZoneID = null, string filterDistrictID = null, Guid? filterStreetID = null, int? filterStreetNumberFrom = null, int? filterStreetNumberTo = null, Guid? filterEstateID = null);
        Task<IEnumerable<PropertyInformationDto>> GetPropertyInformationAsync(int? pageNumber, int? pageSize,
            string filterZoneID = null, string filterDistrictID = null, Guid? filterStreetID = null, int? filterStreetNumberFrom = null, int? filterStreetNumberTo = null, Guid? filterEstateID = null, Guid? filterBuildingID = null);
        Task<IEnumerable<PropertyInformationDto>> GetPropertyInformationAsync(int? pageNumber, int? pageSize,
            string filterZoneID = null, string filterDistrictID = null, Guid? filterStreetID = null, int? filterStreetNumberFrom = null, int? filterStreetNumberTo = null, Guid? filterEstateID = null, Guid? filterBuildingID = null, int? filterPhaseID = null);
        Task<IEnumerable<PropertyInformationDto>> GetPropertyInformationAsync(int? pageNumber, int? pageSize,
            string filterZoneID = null, string filterDistrictID = null, Guid? filterStreetID = null, int? filterStreetNumberFrom = null, int? filterStreetNumberTo = null, Guid? filterEstateID = null, Guid? filterBuildingID = null, int? filterPhaseID = null, int? filterBlockID = null);
        Task<IEnumerable<PropertyInformationDto>> GetPropertyInformationAsync(int? pageNumber, int? pageSize,
            string filterZoneID = null, string filterDistrictID = null, Guid? filterStreetID = null, int? filterStreetNumberFrom = null, int? filterStreetNumberTo = null, Guid? filterEstateID = null, Guid? filterBuildingID = null, int? filterPhaseID = null, int? filterBlockID = null, int? filterFloorID = null);
        Task<IEnumerable<PropertyInformationDto>> GetPropertyInformationAsync(int? pageNumber, int? pageSize,
            string filterZoneID = null, string filterDistrictID = null, Guid? filterStreetID = null, int? filterStreetNumberFrom = null, int? filterStreetNumberTo = null, Guid? filterEstateID = null, Guid? filterBuildingID = null, int? filterPhaseID = null, int? filterBlockID = null, int? filterFloorID = null, int? filterUnitID = null);
    }
}
