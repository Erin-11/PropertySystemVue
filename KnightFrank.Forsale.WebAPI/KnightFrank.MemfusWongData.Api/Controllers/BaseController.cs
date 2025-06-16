using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace KnightFrank.MemfusWongData.Api.Controllers
{
    [Route("[controller]")]
    [Authorize]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected void AddError(IEnumerable<string> errors, string key = "")
        {
            foreach (var error in errors)
            {
                AddError(error, key);
            }
        }

        protected void AddError(string error, string key = "")
        {
            ModelState.AddModelError(key, error);
        }
    }
}
