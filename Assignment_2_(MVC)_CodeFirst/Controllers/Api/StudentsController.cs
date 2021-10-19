using Assignment_2__MVC__CodeFirst.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Assignment_2__MVC__CodeFirst.Controllers.Api
{
    public class StudentsController : ApiController, IMyController<IHttpActionResult, Student>
    {
        public IHttpActionResult Create(Student obj)
        {
            throw new NotImplementedException();
        }

        public IHttpActionResult Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IHttpActionResult Get(int id)
        {
            throw new NotImplementedException();
        }

        public IHttpActionResult GetAll()
        {
            throw new NotImplementedException();
        }

        public IHttpActionResult Update(int id, Student obj)
        {
            throw new NotImplementedException();
        }
    }
}
