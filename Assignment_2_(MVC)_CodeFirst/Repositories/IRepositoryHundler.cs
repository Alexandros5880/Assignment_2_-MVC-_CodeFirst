using System.Threading.Tasks;

namespace Assignment_2__MVC__CodeFirst.Repositories
{
    interface IRepositoryHundler
    {
        bool Save();
        Task<int> SaveAsync();
    }
}
