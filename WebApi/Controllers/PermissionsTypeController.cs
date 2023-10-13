using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Persistance;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("AllowAnyOriginPolicy")]
    public class PermissionsTypeController : ControllerBase
    {
        private readonly IPersistPermissionTypes persist;

        public PermissionsTypeController(IPersistPermissionTypes per)
        {
            persist = per;
        }

        [HttpGet(Name = "GetPermissionTypes")]
        public IEnumerable<PermissionType> GetPermissionTypes()
        {
            return persist.GetPermissionTypes() ;
        }

        [HttpPost(Name = "AddPermissionType")]
        public IActionResult Post([FromBody] PermissionType pt)
        {
            if (persist.AddPermissionType(pt))
            {
                return CreatedAtRoute("GetPermissionTypes", new { id = pt.Id }, pt);
            }
            else
            {
                return BadRequest("Failed to add permission.");
            }
        }
    }
}
