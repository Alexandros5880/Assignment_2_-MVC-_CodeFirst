using Assignment_2__MVC__CodeFirst.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2__MVC__CodeFirst.Controllers.Api
{
    public interface IMyController<R, T> where R : class
                                  where T : IMyEntities
    {
        R GetAll();
        R Get(int id);
        R Create(T obj);
        R Update(int id, T obj);
        R Delete(int id);
    }
}
