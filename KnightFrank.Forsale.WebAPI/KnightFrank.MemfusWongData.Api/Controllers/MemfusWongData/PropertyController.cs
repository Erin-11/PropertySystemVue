using KnightFrank.BAL.CoreInterface.MemfusWongData;
using KnightFrank.BAL.Dtos.MemfusWongData;
using KnightFrank.MemfusWongData.Api.Requests.MemfusWongData;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace KnightFrank.MemfusWongData.Api.Controllers.MemfusWongData
{
    [Route("property")]
    public class PropertyController : BaseController
    {
        private readonly IPropertyService _service;

        public PropertyController(IPropertyService service)
        {
            _service = service;
        }

        [HttpPost("")]
        [ProducesResponseType(200, Type = typeof(PagedResultDto<PropertyDto>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> SearchProperties([FromBody] PropertySearchRequest request)
        {
            var result = await _service.SearchPropertiesAsync(
                request.Region, 
                request.District, 
                request.PropertyTypes, 
                request.MinPrice, 
                request.MaxPrice, 
                request.Page, 
                request.PageSize);

            if (result?.Data == null || !result.Data.Any())
                return NotFound();

            return Ok(result);
        }
    }
}
