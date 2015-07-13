
namespace CQRSProfile
{
    public interface IRepository<T>
    {
        T Get(int id);
        void Save(T entity);
    }

    public class Repository<T> : IRepository<T>
    {
        public T Get(int id)
        {
            return default(T);
        }

        public void Save(T entity)
        {
        }
    }
}