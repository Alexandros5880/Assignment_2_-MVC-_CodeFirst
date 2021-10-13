using System.Collections.Generic;

namespace Assignment_2__MVC__CodeFirst.Repositories
{
    interface IRepository<T> where T : class
    {
        T Get(int? id);
        IEnumerable<T> GetAll();
        void Add(T obj);
        void Delete(T obj);
        void Update(T obj);
    }
}
