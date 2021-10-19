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
    public class StudentsController : ApiController, IMyController<IHttpActionResult, Student>
    {
        public IHttpActionResult Create(Student obj)
        {
            Globals.studentRepo.Add(obj);
            Globals.DbHundler.Save();
            return Ok(obj);
        }

        public IHttpActionResult Delete(int id)
        {
            var student = Globals.studentRepo.Get(id);
            Globals.studentRepo.Delete(student);
            Globals.DbHundler.Save();
            return Ok(student);
        }

        public IHttpActionResult Get(int id)
        {
            var student = Globals.studentRepo.Get(id);
            //student.Courses = Globals.studentRepo.GetCourses(student.ID);
            //student.Assignments = Globals.studentRepo.GetAssignments(student.ID);
            return Ok(student);
        }

        public IHttpActionResult GetAll()
        {
            var students = Globals.studentRepo.GetAll();
            //foreach(var student in students)
            //{
            //    student.Courses = Globals.studentRepo.GetCourses(student.ID);
            //    student.Assignments = Globals.studentRepo.GetAssignments(student.ID);
            //}
            return Ok(students);
        }

        public IHttpActionResult Update(int id, Student obj)
        {
            var student = Globals.studentRepo.Get(id);
            student.FirstName = obj.FirstName;
            student.LastName = obj.LastName;
            student.StartDate = obj.StartDate;
            student.School = obj.School;
            student.Courses = obj.Courses;
            student.Assignments = obj.Assignments;
            Globals.studentRepo.Update(student);
            Globals.DbHundler.Save();
            return Ok(student);
        }
    }
}
