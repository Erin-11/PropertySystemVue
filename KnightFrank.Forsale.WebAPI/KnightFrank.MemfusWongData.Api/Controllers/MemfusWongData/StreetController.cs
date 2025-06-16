using KnightFrank.BAL.CoreInterface.MemfusWongData;
using KnightFrank.BAL.Dtos.MemfusWongData;
using KnightFrank.MemfusWongData.Api.Requests.MemfusWongData;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KnightFrank.MemfusWongData.Api.Controllers.MemfusWongData
{
    public class StreetController : BaseController
    {
        private readonly IStreetService _service;

        private const string GetAllStreetsActionName = "GetAllStreets";

        public StreetController(IStreetService service)
        {
            _service = service;
        }

        [HttpPost("", Name = GetAllStreetsActionName)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<StreetDropdownDto>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAllStreets([FromBody] StreetRequest request)
        {
            var dtos = await _service.GetStreetsAsync(request.Start, request.Length, request.ZoneId, request.DistrictId, request.StreetID, request.StreetNumberFrom, request.StreetNumberTo, request.EstateID, request.BuildingID);

            if (dtos != null)
                return Ok(dtos);
            else
                return NotFound();
        }
    }
}
