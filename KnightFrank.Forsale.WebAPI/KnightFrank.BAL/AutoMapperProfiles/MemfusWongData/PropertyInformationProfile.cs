using AutoMapper;
using KnightFrank.BAL.Common;
using KnightFrank.BAL.Dtos.MemfusWongData;
using KnightFrank.DAL.Entities.Models.MemfusWongData;

namespace KnightFrank.BAL.AutoMapperProfiles.MemfusWongData
{
    public class PropertyInformationProfile : Profile
    {
        public PropertyInformationProfile()
        {
            #region Property Information
            CreateMap<PropertyInformation, PropertyInformationDto>()
                .ReverseMap();
            #endregion
        }
    }
}
