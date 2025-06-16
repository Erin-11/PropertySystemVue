using KnightFrank.BAL.CoreInterface.MemfusWongData;
using KnightFrank.BAL.Dtos.MemfusWongData;
using KnightFrank.MemfusWongData.Api.Requests.MemfusWongData;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KnightFrank.MemfusWongData.Api.Controllers.MemfusWongData
{
    public class DistrictController : BaseController
    {
        private readonly IDistrictService _service;

        private const string GetAllDistrictsActionName = "GetAllDistricts";

        public DistrictController(IDistrictService service)
        {
            _service = service;
        }

        [HttpPost("", Name = GetAllDistrictsActionName)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DistrictDropdownDto>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAllDistricts([FromBody] DistrictRequest request)
        {
            var dtos = await _service.GetDistrictsAsync(request.Start, request.Length, request.ZoneId);

            if (dtos != null)
                return Ok(dtos);
            else
                return NotFound();
        }
    }
}
