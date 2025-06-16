using AutoMapper;
using KnightFrank.BAL.CoreInterface;
using KnightFrank.DAL;
using KnightFrank.DAL.Entities.Base;
using KnightFrank.DAL.Entities.Models.MemfusWongData;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnightFrank.BAL.Core
{
    public partial class MemfusWongDataService<TEntity> : Service<TEntity, MemfusWongDataEntities>, IMemfusWongDataService<TEntity> where TEntity : ModelEntityBase, new()
    {
        public MemfusWongDataService(ILogger<MemfusWongDataService<TEntity>> logger, IMapper mapper, IMemfusWongDataBaseRepositoryAsync<TEntity> repository)
            : base(logger, mapper, repository) { }

        #region PPA
        protected async Task<IEnumerable<Mdbimpfl>> GetMDBIMPFLQuery(string memNo)
        {
            var datas = await GetMDBIMPFLQuery(new List<string>() { memNo ?? string.Empty });
            return datas;
        }
        protected async Task<IEnumerable<Mdbimpfl>> GetMDBIMPFLQuery(IEnumerable<string> memNos)
        {
            var query = GetRepository<Mdbimpfl>().Query(e => e.IsActive);
            query.Filter(e => memNos.Contains(e.MemNo));
            var datas = await query.SelectAsync();
            return datas;
        }
        #endregion

        #region PPA
        protected async Task<IEnumerable<Ppa>> GetPPAQueryByInfo(string estateName, string buildingName, string blockName, string floor, string unit)
        {
            var query = GetRepository<Ppa>().Query(e => e.IsActive);

            // Must be nature of Residential
            query.Filter(e => e.Usage == Common.Constant.ResidentialNature);

            if (!string.IsNullOrWhiteSpace(estateName))
            {
                query.Filter(e => e.EstName == estateName);
            }

            if (!string.IsNullOrWhiteSpace(buildingName))
            {
                query.Filter(e => e.BldName == buildingName);
            }

            if (!string.IsNullOrWhiteSpace(blockName))
            {
                query.Filter(e => e.Block == blockName);
            }

            if (!string.IsNullOrWhiteSpace(floor))
            {
                query.Filter(e => e.Floor == floor);
            }

            if (!string.IsNullOrWhiteSpace(unit))
            {
                query.Filter(e => e.Unit == unit);
            }

            var datas = await query.SelectAsync();
            return datas;
        }
        #endregion
    }
}
