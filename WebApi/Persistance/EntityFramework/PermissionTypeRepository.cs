using WebApi.Models;

namespace WebApi.Persistance.EntityFramework
{
    public class PermissionTypeRepository : IRepository<PermissionType>
    {
        private readonly ApiDbContext _context;

        public PermissionTypeRepository(ApiDbContext context)
        {
            _context = context;
        }

        public IEnumerable<PermissionType> GetAll()
        {
            return _context.TiposPermisos.ToList();
        }

        public bool Add(PermissionType pt)
        {
            bool success = false;

            try
            {
                _context.TiposPermisos.Add(pt);
                _context.SaveChanges();
                success = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return success;
        }

        public PermissionType? GetById(int id)
        {
            return _context.TiposPermisos.FirstOrDefault(p => p.Id == id);
        }

        public bool Update(PermissionType pt)
        {
            bool success = false;

            try
            {
                var existingPermission = _context.TiposPermisos.FirstOrDefault(pt => pt.Id == pt.Id);

                if (existingPermission != null)
                {
                    // Use Entity Framework Core to mark the entity as modified
                    _context.Entry(existingPermission).CurrentValues.SetValues(pt);
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
