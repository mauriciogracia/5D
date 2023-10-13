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

    public bool AddPermissionType(PermissionType pt)
    {
        bool success = false;

        try
        {
            _context.TiposPermisos.Add(pt);
            _context.SaveChanges();
            success = true;
        }
        catch
        {
            // TODO Handle any exceptions, e.g., log the error
        }

        return success;
    }
    
}
