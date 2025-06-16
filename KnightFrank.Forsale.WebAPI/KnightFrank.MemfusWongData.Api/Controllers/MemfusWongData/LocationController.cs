using KnightFrank.BAL.CoreInterface.MemfusWongData;
using KnightFrank.BAL.Dtos.MemfusWongData;
using KnightFrank.MemfusWongData.Api.Requests.MemfusWongData;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnightFrank.MemfusWongData.Api.Controllers.MemfusWongData
{
    public class LocationController : BaseController
    {
        private readonly ILocationService _service;

        public LocationController(ILocationService service)
        {
            _service = service;
        }

        [HttpPost("")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<LocationDto>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> SearchAllLocations([FromBody] LocationRequest request)
        {
            var dtos = await _service.SearchLocationsAsync(request.Start, request.Length, request.ZoneID, request.DistrictID, request.StreetKeyword, request.EstateKeyword, request.BuildingKeyword);

            if (dtos != null)
                return Ok(dtos);
            else
                return NotFound();
        }
    }
}
