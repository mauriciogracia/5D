using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApi.Models;
using WebApi.Persistance;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors(Program.CORS_POLICY_NAME)]
    public class PermissionTypesController : ControllerBase
    {
        private readonly IRepository<PermissionType> permissionTypeRepo;

        public PermissionTypesController(IRepository<PermissionType> per)
        {
            permissionTypeRepo = per;
        }

        [HttpGet(Name = "GetPermissionTypes")]
        public IEnumerable<PermissionType> GetPermissionTypes()
        {
            IEnumerable<PermissionType> pts;
            pts = permissionTypeRepo.GetAll();

            if(!pts.Any())
            {
                PreparePermissionTypes();
                pts = permissionTypeRepo.GetAll();
            }
            return pts;
        }

        private void PreparePermissionTypes()
        {
            permissionTypeRepo.Add(new PermissionType { Descripcion = "Lectura" });
            permissionTypeRepo.Add(new PermissionType { Descripcion = "Modificacion" });
            permissionTypeRepo.Add(new PermissionType { Descripcion = "Admin" });
        }

        [HttpPost(Name = "AddPermissionType")]
        public IActionResult Post([FromBody] PermissionType pt)
        {
            if (permissionTypeRepo.Add(pt))
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
