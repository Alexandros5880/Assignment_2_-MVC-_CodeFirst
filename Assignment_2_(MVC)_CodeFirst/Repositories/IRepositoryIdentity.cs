using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assignment_2__MVC__CodeFirst.Repositories
{
    public interface IRepositoryIdentity<T>
    {
        void Add(T obj);

        void Delete(T obj);

        T Get(string id);

        IEnumerable<T> GetAll();

        bool Save();

        Task<int> SaveAsync();

        void Update(T obj);
    }
}
