using System.Security.Policy;
using AutoMapper;
using KnightFrank.BAL.Common;
using KnightFrank.BAL.Dtos.MemfusWongData;
using KnightFrank.DAL.Entities.Models.MemfusWongData;

namespace KnightFrank.BAL.AutoMapperProfiles.MemfusWongData
{
    public class PropertyProfile : Profile
    {
        public PropertyProfile()
        {
            #region Property Information
            CreateMap<Property, PropertyDto>()
                .ReverseMap();
            #endregion
        }
    }
}
