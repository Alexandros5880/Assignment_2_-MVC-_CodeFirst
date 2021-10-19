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
            Globals.schoolRepo.Add(obj);
            Globals.DbHundler.Save();
            return Ok(obj);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            Globals.schoolRepo.Delete(Globals.schoolRepo.Get(id));
            Globals.DbHundler.Save();
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            School school = Globals.schoolRepo.Get(id);
            if (school == null) return NotFound();
            //school.Courses = Globals.schoolRepo.GetCourses(school.ID);
            //school.Assignments = Globals.schoolRepo.GetAssignments(school.ID);
            //school.Trainers = Globals.schoolRepo.GetTrainers(school.ID);
            //school.Students = Globals.schoolRepo.GetStudents(school.ID);
            return Ok(school);
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var schools = Globals.schoolRepo.GetAll();
            //foreach (School school in schools)
            //{
            //    school.Courses = Globals.schoolRepo.GetCourses(school.ID);
            //    school.Assignments = Globals.schoolRepo.GetAssignments(school.ID);
            //    school.Trainers = Globals.schoolRepo.GetTrainers(school.ID);
            //    school.Students = Globals.schoolRepo.GetStudents(school.ID);
            //}
            return Ok(schools);
        }

        [HttpPut]
        public IHttpActionResult Update(int id, School obj)
        {
            var schoolDB = Globals.schoolRepo.Get(id);
            schoolDB.Name = obj.Name;
            schoolDB.StartDate = obj.StartDate;
            //schoolDB.Courses = obj.Courses;
            //schoolDB.Assignments = obj.Assignments;
            //schoolDB.Trainers = obj.Trainers;
            //schoolDB.Students = obj.Students;
            Globals.schoolRepo.Update(schoolDB);
            Globals.DbHundler.Save();
            return Ok(schoolDB);
        }
    }
}
