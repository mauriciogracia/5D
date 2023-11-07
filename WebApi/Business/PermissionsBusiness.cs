using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Persistance;

namespace WebApi.Business
{
    public class PermissionsBusiness
    {
        private readonly IRepository<Permission> permissionRepo;

        public PermissionsBusiness(IRepository<Permission> per)
        {
            permissionRepo = per;
        }

        public IEnumerable<Permission> Get()
        {
            return permissionRepo.GetAll() ;
        }

        public bool Add(Permission permiso)
        {
            return permissionRepo.Add(permiso);
        }

        public Permission? Get(int id)
        {
            return permissionRepo.GetById(id);
        }

        public bool Update(int id, [FromBody] Permission permiso)
        {
            return permissionRepo.Update(permiso) ;
        }
    }
}
