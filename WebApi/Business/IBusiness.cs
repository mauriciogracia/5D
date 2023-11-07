namespace WebApi.Business
{
    public interface IBusiness<T>
    {
        IEnumerable<T> GetAll();
        T? GetById(int id);
        bool Add(T entity);
        bool Update(T entity);
    }

}
