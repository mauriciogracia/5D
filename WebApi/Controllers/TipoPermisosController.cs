using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Persistance;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TipoPermisosController : ControllerBase
    {
        private readonly IPersistPermissionsTypes persist;

        public TipoPermisosController(IPersistPermissionsTypes per)
        {
            persist = per;
        }

        [HttpGet(Name = "GetPermissionsTypes")]
        public IEnumerable<TipoPermiso> GetPermissionsTypes()
        {
            return persist.GetPermissionsTypes() ;
        }
    }
}
