using KnightFrank.BAL.CoreInterface.MemfusWongData;
using KnightFrank.BAL.Dtos.MemfusWongData;
using KnightFrank.MemfusWongData.Api.Requests.MemfusWongData;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KnightFrank.MemfusWongData.Api.Controllers.MemfusWongData
{
    public class BuildingController : BaseController
    {
        private readonly IBuildingService _service;

        public BuildingController(IBuildingService service)
        {
            _service = service;
        }

        [HttpPost("")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BuildingDropdownDto>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> SearchAllBuildings([FromBody] BuildingRequest request)
        {
            var dtos = await _service.SearchBuildingsAsync(request.Start, request.Length, request.ZoneID, request.DistrictID, request.StreetID, request.StreetNumberFrom, request.StreetNumberTo, request.EstateID, request.BuildingID);

            if (dtos != null)
                return Ok(dtos);
            else
                return NotFound();
        }
    }
}
