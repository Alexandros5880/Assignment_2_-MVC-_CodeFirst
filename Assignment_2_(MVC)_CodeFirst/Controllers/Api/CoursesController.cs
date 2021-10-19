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
    public class CoursesController : ApiController, IMyController<IHttpActionResult, Course>
    {
        public IHttpActionResult Create(Course obj)
        {
            Globals.courseRepo.Add(obj);
            Globals.DbHundler.Save();
            return Ok(obj);
        }

        public IHttpActionResult Delete(int id)
        {
            Globals.courseRepo.Delete(Globals.courseRepo.Get(id));
            Globals.DbHundler.Save();
            return Ok();
        }

        public IHttpActionResult Get(int id)
        {
            Course course = Globals.courseRepo.Get(id);
            //course.Students = Globals.courseRepo.GetStudents(course.ID);
            return Ok(course);
        }

        public IHttpActionResult GetAll()
        {
            var courses = Globals.courseRepo.GetAll();
            //foreach (Course course in courses)
            //{
            //    course.Students = Globals.courseRepo.GetStudents(course.ID);
            //}
            return Ok(courses);
        }

        public IHttpActionResult Update(int id, Course obj)
        {
            Course course = Globals.courseRepo.Get(id);
            course.Title = obj.Title;
            course.StartDate = obj.StartDate;
            course.EndDate = obj.EndDate;
            course.School = obj.School;
            course.Trainer = obj.Trainer;
            //course.Students = obj.Students;
            Globals.courseRepo.Update(course);
            Globals.DbHundler.Save();
            return Ok(course);
        }
    }
}
