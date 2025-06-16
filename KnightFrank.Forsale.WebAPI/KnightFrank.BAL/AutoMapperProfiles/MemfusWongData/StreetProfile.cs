using AutoMapper;
using KnightFrank.BAL.Common;
using KnightFrank.BAL.Dtos.MemfusWongData;
using KnightFrank.DAL.Entities.Models.MemfusWongData;

namespace KnightFrank.BAL.AutoMapperProfiles.MemfusWongData
{
    public class StreetProfile : Profile
    {
        public StreetProfile()
        {
            #region Street Information
            CreateMap<Street, StreetDropdownDto>()
                .ReverseMap();
            #endregion
        }
    }
}
