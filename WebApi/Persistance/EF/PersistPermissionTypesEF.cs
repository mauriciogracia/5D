using WebApi.Models;
using WebApi.Persistance;

public class PersistPermissionTypesEF : IPersistPermissionTypes
{
    private readonly ApiDbContext _context;

    public PersistPermissionTypesEF(ApiDbContext context)
    {
        _context = context;
    }

    public IEnumerable<PermissionType> GetPermissionTypes()
    {
        return _context.TiposPermisos.ToList();
    }

    
}
