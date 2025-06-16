using KnightFrank.BAL.CoreInterface.MemfusWongData;
using KnightFrank.BAL.Dtos.MemfusWongData;
using KnightFrank.MemfusWongData.Api.Requests.MemfusWongData;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KnightFrank.MemfusWongData.Api.Controllers.MemfusWongData
{
    public class BlockController : BaseController
    {
        private readonly IBlockService _service;

        public BlockController(IBlockService service)
        {
            _service = service;
        }

        [HttpPost("")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BlockDropdownDto>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> SearchAllBlocks([FromBody] BlockRequest request)
        {
            var dtos = await _service.SearchBlocksAsync(request.Start, request.Length, request.ZoneID, request.DistrictID, request.StreetID, request.StreetNumberFrom, request.StreetNumberTo, request.EstateID, request.BuildingID, request.PhaseID);

            if (dtos != null)
                return Ok(dtos);
            else
                return NotFound();
        }
    }
}
