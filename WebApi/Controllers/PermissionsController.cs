using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Persistance;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors(Config.CORS_POLICY_NAME)]
    public class PermissionsController : ControllerBase
    {
        private readonly IRepository<Permission> permissionRepo;

        public PermissionsController(IRepository<Permission> per)
        {
            permissionRepo = per;
        }

        [HttpGet(Name = "GetPermissions")]
        public IEnumerable<Permission> Get()
        {
            return permissionRepo.GetAll() ;
        }

        [HttpPost(Name = "AddPermission")]
        public IActionResult Post([FromBody] Permission permiso)
        {
            if (permissionRepo.Add(permiso))
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
            var permiso = permissionRepo.GetById(id);
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
            if (permissionRepo.Update(permiso))
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
