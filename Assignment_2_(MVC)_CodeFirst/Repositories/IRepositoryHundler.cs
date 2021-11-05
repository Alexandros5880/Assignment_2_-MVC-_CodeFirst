using System.Threading.Tasks;

namespace Assignment_2__MVC__CodeFirst.Repositories
{
    public interface IRepositoryHundler
    {
        bool Save();
        Task<int> SaveAsync();
    }
}
