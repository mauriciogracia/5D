using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Persistance;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
    }
}
