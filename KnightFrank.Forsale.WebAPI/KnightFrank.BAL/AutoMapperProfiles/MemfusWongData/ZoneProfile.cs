using AutoMapper;
using KnightFrank.BAL.Common;
using KnightFrank.BAL.Dtos.MemfusWongData;
using KnightFrank.DAL.Entities.Models.MemfusWongData;

namespace KnightFrank.BAL.AutoMapperProfiles.MemfusWongData
{
    public class ZoneProfile : Profile
    {
        public ZoneProfile()
        {
            #region Zone Information
            CreateMap<Zone, ZoneDropdownDto>()
                .ReverseMap();
            #endregion
        }
    }
}
