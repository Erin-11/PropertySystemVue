using AutoMapper;
using KnightFrank.BAL.Common;
using KnightFrank.BAL.Dtos.MemfusWongData;
using KnightFrank.DAL.Entities.Models.MemfusWongData;

namespace KnightFrank.BAL.AutoMapperProfiles.MemfusWongData
{
    public class EstateProfile : Profile
    {
        public EstateProfile()
        {
            #region Estate Information
            CreateMap<Estate, EstateDropdownDto>()
                .ReverseMap();
            #endregion
        }
    }
}
