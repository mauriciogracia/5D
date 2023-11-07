using WebApi.Models;

namespace WebApi.Persistance.CQRS
{
    public class RepositoryCQRS : IRepository<Permission>
    {
        IRepository<Permission> _db;
        IRepository<Permission> _cache ;
        public RepositoryCQRS(IRepository<Permission> database, IRepository<Permission> cache)
        {
            this._db = database;
            this._cache = cache;
        } 
        public bool Add(Permission entity)
        {
            return _db.Add(entity) && _cache.Add(entity);
        }

        public IEnumerable<Permission> GetAll()
        {
            return _cache.GetAll();
        }

        public Permission? GetById(int id)
        {
            return _cache.GetById(id);
        }

        public bool Update(Permission entity)
        {
            return _db.Update(entity) && _cache.Update(entity);
        }
    }
}
