namespace WebApi.Persistance
{
    public interface IPersist
    {
        public bool AddPermission(Permiso p);
        public IEnumerable<Permiso> GetPermissions();
        public Permiso? RequestPermission(int id);
        public bool ModifyPermission(Permiso p);

    }
}
