using WebApi.Models;

namespace WebApi.Persistance
{
    public interface IPersistPermissionsTypes
    {
        public IEnumerable<TipoPermiso> GetPermissionsTypes();

    }
}
