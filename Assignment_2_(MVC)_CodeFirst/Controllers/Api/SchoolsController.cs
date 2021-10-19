using Assignment_2__MVC__CodeFirst.Models.Entities;
using Assignment_2__MVC__CodeFirst.Static;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Assignment_2__MVC__CodeFirst.Controllers.Api
{
    public class SchoolsController : ApiController, IMyController<IHttpActionResult, School>
    {
        [HttpPost]
        public IHttpActionResult Create(School obj)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            School school = Globals.schoolRepo.Get(id);
            if (school == null) return NotFound();
            return Ok(school);
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var schools = Globals.schoolRepo.GetAll();
            return Ok(schools);
        }

        [HttpPut]
        public IHttpActionResult Update(int id, School obj)
        {
            throw new NotImplementedException();
        }
    }
}
