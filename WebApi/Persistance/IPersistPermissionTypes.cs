using WebApi.Models;

namespace WebApi.Persistance
{
    public interface IPersistPermissionTypes
    {
        public IEnumerable<PermissionType> GetPermissionTypes();

        public bool AddPermissionType(PermissionType pt);
    }
}
