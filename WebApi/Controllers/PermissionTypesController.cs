using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Persistance;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("AllowAnyOriginPolicy")]
    public class PermissionTypesController : ControllerBase
    {
        private readonly IRepository<PermissionType> persist;

        public PermissionTypesController(IRepository<PermissionType> per)
        {
            persist = per;
        }

        [HttpGet(Name = "GetPermissionTypes")]
        public IEnumerable<PermissionType> GetPermissionTypes()
        {
            return persist.GetAll() ;
        }

        [HttpPost(Name = "AddPermissionType")]
        public IActionResult Post([FromBody] PermissionType pt)
        {
            if (persist.Add(pt))
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
