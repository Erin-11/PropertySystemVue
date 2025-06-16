using KnightFrank.BAL.CoreInterface.MemfusWongData;
using KnightFrank.BAL.Dtos.MemfusWongData;
using KnightFrank.MemfusWongData.Api.Requests.MemfusWongData;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KnightFrank.MemfusWongData.Api.Controllers.MemfusWongData
{
    public class ZoneController : BaseController
    {
        private readonly IZoneService _service;

        private const string GetAllZonesActionName = "GetAllZones";

        public ZoneController(IZoneService service)
        {
            _service = service;
        }

        [HttpPost("", Name = GetAllZonesActionName)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ZoneDropdownDto>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAllZones([FromBody] ZoneRequest request)
        {
            var dtos = await _service.GetZonesAsync(request.Start, request.Length);

            if (dtos != null)
                return Ok(dtos);
            else
                return NotFound();
        }
    }
}
