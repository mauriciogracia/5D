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
    }
}