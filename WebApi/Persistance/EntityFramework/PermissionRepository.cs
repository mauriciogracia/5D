using WebApi.Models;
using WebApi.Persistance;

namespace WebApi.Persistance.EntityFramework
{
    public class PermissionRepository : IRepository<Permission>
    {
        private readonly ApiDbContext _context;

        public PermissionRepository(ApiDbContext context)
        {
            _context = context;
        }

        public bool Add(Permission permiso)
        {
            bool success = false;

            try
            {
                _context.Permisos.Add(permiso);
                _context.SaveChanges();
                success = true;
            }
            catch
            {
                // TODO Handle any exceptions, e.g., log the error
            }

            return success;
        }

        public IEnumerable<Permission> GetAll()
        {
            return _context.Permisos.ToList();
        }

        public Permission? GetById(int id)
        {
            return _context.Permisos.FirstOrDefault(p => p.Id == id);
        }

        public bool Update(Permission permiso)
        {
            bool success = false;

            try
            {
                var existingPermission = _context.Permisos.FirstOrDefault(p => p.Id == permiso.Id);

                if (existingPermission != null)
                {
                    // Use Entity Framework Core to mark the entity as modified
                    _context.Entry(existingPermission).CurrentValues.SetValues(permiso);
                    _context.SaveChanges();
                    success = true;
                }
            }
            catch
            {
                // Handle any exceptions, e.g., log the error
            }

            return success;
        }
    }
}
