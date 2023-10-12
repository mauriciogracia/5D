using WebApi.Models;

namespace WebApi.Persistance
{
    public interface IPersistPermissions
    {
        public bool AddPermission(Permission p);
        public IEnumerable<Permission> GetPermissions();
        public Permission? RequestPermission(int id);
        public bool ModifyPermission(Permission p);

    }
}
