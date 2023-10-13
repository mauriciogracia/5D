using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Persistance;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("AllowAnyOriginPolicy")]
    public class PermissionsController : ControllerBase
    {
        private readonly IRepository<Permission> permisionRepo;

        public PermissionsController(IRepository<Permission> per)
        {
            permisionRepo = per;
        }

        [HttpGet(Name = "GetPermissions")]
        public IEnumerable<Permission> Get()
        {
            return permisionRepo.GetAll() ;
        }

        [HttpPost(Name = "AddPermission")]
        public IActionResult Post([FromBody] Permission permiso)
        {
            if (permisionRepo.Add(permiso))
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
            var permiso = permisionRepo.GetById(id);
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
            if (permisionRepo.Update(permiso))
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
