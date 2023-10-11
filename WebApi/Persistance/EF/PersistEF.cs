using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Persistance;

public class PersistEF : IPersist
{
    private readonly ApiDbContext _context;

    public PersistEF(ApiDbContext context)
    {
        _context = context;
    }

    public bool AddPermission(Permiso permiso)
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

    public IEnumerable<Permiso> GetPermissions()
    {
        return _context.Permisos.ToList();
    }

    public Permiso? RequestPermission(int id)
    {
        return _context.Permisos.FirstOrDefault(p => p.Id == id);
    }

    public bool ModifyPermission(Permiso permiso)
    {
        bool success = false;

        try
        {
            _context.Permisos.Update(permiso);
            _context.SaveChanges();
            success = true;
        }
        catch
        {
            // Handle any exceptions, e.g., log the error
        }

        return success;
    }
}
