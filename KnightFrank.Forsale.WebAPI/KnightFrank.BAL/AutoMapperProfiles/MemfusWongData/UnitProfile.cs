using AutoMapper;
using KnightFrank.BAL.Common;
using KnightFrank.BAL.Dtos.MemfusWongData;
using KnightFrank.DAL.Entities.Models.MemfusWongData;

namespace KnightFrank.BAL.AutoMapperProfiles.MemfusWongData
{
    public class UnitProfile : Profile
    {
        public UnitProfile()
        {
            #region Zone Information
            CreateMap<Unit, UnitDropdownDto>()
                .ReverseMap();
            #endregion
        }
    }
}
