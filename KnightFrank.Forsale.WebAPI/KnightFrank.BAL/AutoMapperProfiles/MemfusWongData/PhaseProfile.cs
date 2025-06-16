using AutoMapper;
using KnightFrank.BAL.Common;
using KnightFrank.BAL.Dtos.MemfusWongData;
using KnightFrank.DAL.Entities.Models.MemfusWongData;

namespace KnightFrank.BAL.AutoMapperProfiles.MemfusWongData
{
    public class PhaseProfile : Profile
    {
        public PhaseProfile()
        {
            #region Phase Information
            CreateMap<Phase, PhaseDropdownDto>()
                .ReverseMap();
            #endregion
        }
    }
}
