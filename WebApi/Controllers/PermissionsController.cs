using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebApi.Business;
using WebApi.Models;
using WebApi.Persistance;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors(Config.CORS_POLICY_NAME)]
    public class PermissionsController : ControllerBase
    {
        private readonly IBusiness<Permission> permissionBusiness;

        public PermissionsController(IBusiness<Permission> per)
        {
            permissionBusiness = per;
        }

        [HttpGet(Name = "GetPermissions")]
        public IEnumerable<Permission> Get()
        {
            return permissionBusiness.GetAll() ;
        }

        [HttpPost(Name = "AddPermission")]
        public IActionResult Post([FromBody] Permission permiso)
        {
            return permissionBusiness.Add(permiso)
               ? CreatedAtRoute("GetPermissions", new { id = permiso.Id }, permiso)
               : BadRequest("Failed to add permission.");
        }

        [HttpGet("{id}", Name = "GetPermission")]
        public IActionResult Get(int id)
        {
            var permiso = permissionBusiness.GetById(id);

            return (permiso != null) ? Ok(permiso): NotFound();
        }

        [HttpPut("{id}", Name = "UpdatePermission")]
        public IActionResult Put(int id, [FromBody] Permission permiso)
        {
            permiso.Id = id;

            return (permissionBusiness.Update(permiso)) ? Ok(permiso): BadRequest("Failed to update permission.");
        }
    }
}
