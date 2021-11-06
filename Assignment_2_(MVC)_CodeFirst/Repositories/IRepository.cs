using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assignment_2__MVC__CodeFirst.Repositories
{
    public interface IRepository<T> where T : class
    {
        T Get(int? id);
        IEnumerable<T> GetAll();
        T GetEmpty(int? id);
        IEnumerable<T> GetAllEmpty();
        void Add(T obj);
        void Delete(T obj);
        void Update(T obj);
        bool Save();
        Task<int> SaveAsync();
    }
}
