using AutoMapper;
using KnightFrank.BAL.Common;
using KnightFrank.BAL.Dtos.MemfusWongData;
using KnightFrank.DAL.Entities.Models.MemfusWongData;

namespace KnightFrank.BAL.AutoMapperProfiles.MemfusWongData
{
    public class DistrictProfile : Profile
    {
        public DistrictProfile()
        {
            #region District Information
            CreateMap<District, DistrictDropdownDto>()
                .ReverseMap();
            #endregion
        }
    }
}
