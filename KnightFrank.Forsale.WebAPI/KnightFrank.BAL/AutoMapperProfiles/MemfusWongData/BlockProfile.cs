using AutoMapper;
using KnightFrank.BAL.Common;
using KnightFrank.BAL.Dtos.MemfusWongData;
using KnightFrank.DAL.Entities.Models.MemfusWongData;

namespace KnightFrank.BAL.AutoMapperProfiles.MemfusWongData
{
    public class BlockProfile : Profile
    {
        public BlockProfile()
        {
            #region Block Information
            CreateMap<Block, BlockDropdownDto>()
                .ReverseMap();
            #endregion
        }
    }
}
