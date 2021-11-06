using Assignment_2__MVC__CodeFirst.Models.Dto;

namespace Assignment_2__MVC__CodeFirst.Controllers.Api
{
    public interface IMyController<R, T> where R : class
                                  where T : IDto
    {
        R GetAll();
        R Get(int id);
        R Create(T obj);
        R Update(int id, T obj);
        R Delete(int id);
    }
}
