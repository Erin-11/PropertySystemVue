using AutoMapper;
using KnightFrank.BAL.Common;
using KnightFrank.BAL.Dtos.MemfusWongData;
using KnightFrank.DAL.Entities.Models.MemfusWongData;

namespace KnightFrank.BAL.AutoMapperProfiles.MemfusWongData
{
    public class BuildingProfile : Profile
    {
        public BuildingProfile()
        {
            #region Building Information
            CreateMap<Building, BuildingDropdownDto>()
                .ReverseMap();
            #endregion
        }
    }
}
