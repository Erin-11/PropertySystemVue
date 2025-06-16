using AutoMapper;
using KnightFrank.BAL.Dtos.MemfusWongData;
using KnightFrank.DAL.Entities.Models.MemfusWongData;

namespace KnightFrank.BAL.AutoMapperProfiles.MemfusWongData
{
    public class LocationProfile : Profile
    {
        public LocationProfile()
        {
            #region Location Information
            CreateMap<Location, LocationDto>()
                .ReverseMap();
            CreateMap<Location, LocationFilterDto>()
                .ReverseMap();
            #endregion
        }
    }
}
