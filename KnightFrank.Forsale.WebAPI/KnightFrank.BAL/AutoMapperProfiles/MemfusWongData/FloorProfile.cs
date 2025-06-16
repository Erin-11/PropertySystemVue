using AutoMapper;
using KnightFrank.BAL.Common;
using KnightFrank.BAL.Dtos.MemfusWongData;
using KnightFrank.DAL.Entities.Models.MemfusWongData;

namespace KnightFrank.BAL.AutoMapperProfiles.MemfusWongData
{
    public class FloorProfile : Profile
    {
        public FloorProfile()
        {
            #region Floor Information
            CreateMap<Floor, FloorDropdownDto>()
                .ReverseMap();
            #endregion
        }
    }
}
