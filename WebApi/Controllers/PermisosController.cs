using Microsoft.AspNetCore.Mvc;
using WebApi.Persistance;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PermisosController : ControllerBase
    {
        private readonly IPersist persist;

        public PermisosController(IPersist per)
        {
            persist = per;
        }

        [HttpGet(Name = "GetPermissions")]
        public IEnumerable<Permiso> Get()
        {
            return persist.GetPermissions() ;
        }

        [HttpPost(Name = "AddPermission")]
        public IActionResult Post([FromBody] Permiso permiso)
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
        public IActionResult Put([FromBody] Permiso permiso)
        {
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
