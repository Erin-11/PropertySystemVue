using KnightFrank.BAL.CoreInterface.MemfusWongData;
using KnightFrank.BAL.Dtos.MemfusWongData;
using KnightFrank.MemfusWongData.Api.Requests.MemfusWongData;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KnightFrank.MemfusWongData.Api.Controllers.MemfusWongData
{
    public class UnitController : BaseController
    {
        private readonly IUnitService _service;

        public UnitController(IUnitService service)
        {
            _service = service;
        }

        [HttpPost("")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UnitDropdownDto>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> SearchAllUnit([FromBody] UnitRequest request)
        {
            var dtos = await _service.SearchUnitAsync(request.Start, request.Length, request.ZoneID, request.DistrictID, request.StreetID, request.EstateID, request.BuildingID, request.PhaseID, request.BlockID,request.FloorID);

            if (dtos != null)
                return Ok(dtos);
            else
                return NotFound();
        }
    }
}
