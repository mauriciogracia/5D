using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Persistance;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class PermissionController : ControllerBase
    {
        private readonly IPersistPermissions persist;

        public PermissionController(IPersistPermissions per)
        {
            persist = per;
        }

        [HttpGet(Name = "GetPermissions")]
        public IEnumerable<Permission> Get()
        {
            return persist.GetPermissions() ;
        }

        [HttpPost(Name = "AddPermission")]
        public IActionResult Post([FromBody] Permission permiso)
        {
            if (persist.AddPermission(permiso))
            {
                return CreatedAtRoute("GetPermissions", new { id = permiso.Id }, permiso);
            }
            else
            {
                return BadRequest("Failed to add permission.");
            }
        }

        [HttpGet("{id}", Name = "GetPermission")]
        public IActionResult Get(int id)
        {
            var permiso = persist.RequestPermission(id);
            if (permiso != null)
            {
                return Ok(permiso);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("{id}", Name = "UpdatePermission")]
        public IActionResult Put(int id, [FromBody] Permission permiso)
        {
            permiso.Id = id;
            if (persist.ModifyPermission(permiso))
            {
                return Ok(permiso);
            }
            else
            {
                return BadRequest("Failed to update permission.");
            }
        }
    }
}
