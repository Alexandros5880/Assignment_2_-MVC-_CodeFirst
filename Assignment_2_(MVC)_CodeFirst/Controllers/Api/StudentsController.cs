using Assignment_2__MVC__CodeFirst.Models.Entities;
using Assignment_2__MVC__CodeFirst.Models.Other;
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
        [HttpPost]
        public IHttpActionResult Create(Student obj)
        {
            Globals.studentRepo.Add(obj);
            Globals.DbHundler.Save();
            return Ok(obj);
        }
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var student = Globals.studentRepo.Get(id);
            Globals.studentRepo.Delete(student);
            Globals.DbHundler.Save();
            return Ok(student);
        }
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var student = Globals.studentRepo.Get(id);
            //student.Courses = Globals.studentRepo.GetCourses(student.ID);
            //student.Assignments = Globals.studentRepo.GetAssignments(student.ID);
            return Ok(student);
        }
        [HttpGet]
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
        [HttpPut]
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

        [Route("api/{Students}/{RemoveCourse}"), HttpPost]
        public IHttpActionResult RemoveCourse([FromBody] StudentCourseData data)
        {
            if (data.studentId == null || data.courseId == null)
                return BadRequest();
            Student student = Globals.studentRepo.Get(data.studentId);
            Course course = Globals.courseRepo.Get(data.courseId);
            if (student == null || course == null)
                return BadRequest();
            student.Courses.Remove(course);
            course.Students.Remove(student);
            if (ModelState.IsValid)
            {
                Globals.studentRepo.Update(student);
                Globals.courseRepo.Update(course);
                Globals.DbHundler.Save();
                return Ok(student);
            }
            return BadRequest("Record Failed");
        }

        [Route("api/{Students}/{RemoveAssignment}"), HttpPost]
        public IHttpActionResult RemoveAssignment([FromBody] StudentAssignmentData data)
        {
            if (data.studentId == null || data.assignmentId == null)
                return BadRequest();
            Student student = Globals.studentRepo.Get(data.studentId);
            Assignment assignment = Globals.assignmentRepo.Get(data.assignmentId);
            if (student == null || assignment == null)
                return BadRequest();
            student.Assignments.Remove(assignment);
            assignment.Students.Remove(student);
            if (ModelState.IsValid)
            {
                Globals.studentRepo.Update(student);
                Globals.assignmentRepo.Update(assignment);
                Globals.DbHundler.Save();
                return Ok(student);
            }
            return BadRequest("Record Failed");
        }
    }
}
