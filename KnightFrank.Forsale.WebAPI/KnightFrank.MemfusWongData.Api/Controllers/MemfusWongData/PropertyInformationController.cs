using KnightFrank.BAL.CoreInterface.MemfusWongData;
using KnightFrank.BAL.Dtos.MemfusWongData;
using KnightFrank.MemfusWongData.Api.Common;
using KnightFrank.MemfusWongData.Api.Requests.MemfusWongData;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnightFrank.MemfusWongData.Api.Controllers.MemfusWongData
{
    public class PropertyInformationController : BaseController
    {
        private readonly IPropertyInformationService _service;

        public PropertyInformationController(IPropertyInformationService service)
        {
            _service = service;
        }

        [HttpPost("search")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PropertyInformationDto>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Search([FromBody] PropertyInformationRequest request)
        {
            try
            {
                int? _pageNumber = null;
                int? _pageSize = null;

                if (request.Start.HasValue && request.Length.HasValue)
                {
                    _pageNumber = (request.Start / request.Length) + 1;
                    _pageSize = request.Length;
                }

                var dtos = await _service.GetPropertyInformationAsync(_pageNumber, _pageSize, request.ZoneID, request.DistrictID, request.StreetID, request.FilterStreetNumberFrom, request.FilterStreetNumberTo, request.EstateID, request.BuildingID, request.PhaseID, request.BlockID, request.FloorID, request.UnitID);

                if (dtos != null)
                    return Ok(dtos);
                else
                    return NotFound(request);
            }
            catch (System.Exception ex)
             {
                return NotFound(request);
            }
        }
    }
}
