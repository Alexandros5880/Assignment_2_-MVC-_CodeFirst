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
    public class AssignmentsController : ApiController, IMyController<IHttpActionResult, Assignment>
    {
        [HttpPost]
        public IHttpActionResult Create(Assignment obj)
        {
            Globals.assignmentRepo.Add(obj);
            Globals.DbHundler.Save();
            return Ok(obj);
        }
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var assignment = Globals.assignmentRepo.Get(id);
            Globals.assignmentRepo.Delete(assignment);
            Globals.DbHundler.Save();
            return Ok(assignment);
        }
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var assignment = Globals.assignmentRepo.Get(id);
            //assignment.Students = Globals.assignmentRepo.GetStudents(assignment.ID);
            return Ok(assignment);
        }
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var assignments = Globals.assignmentRepo.GetAll();
            //foreach (var assignment in assignments)
            //{
            //    assignment.Students = Globals.assignmentRepo.GetStudents(assignment.ID);
            //}
            return Ok(assignments);
        }
        [HttpPut]
        public IHttpActionResult Update(int id, Assignment obj)
        {
            var assignment = Globals.assignmentRepo.Get(id);
            assignment.Title = obj.Title;
            assignment.StartDate = obj.StartDate;
            assignment.EndDate = obj.EndDate;
            assignment.Students = obj.Students;
            assignment.School = obj.School;
            Globals.assignmentRepo.Update(assignment);
            Globals.DbHundler.Save();
            return Ok(assignment);
        }

        [Route("api/{Assignments}/{RemoveStudent}"), HttpPost]
        public IHttpActionResult RemoveStudent([FromBody] AssignmentStudentData data)
        {
            if (data.assignmentId == null || data.studentId == null)
                return BadRequest();
            Assignment assignment = Globals.assignmentRepo.Get(data.assignmentId);
            Student student = Globals.studentRepo.Get(data.studentId);
            if (assignment == null || student == null)
                return BadRequest();

            assignment.Students.Remove(student);
            student.Assignments.Remove(assignment);

            if (ModelState.IsValid)
            {
                Globals.assignmentRepo.Update(assignment);
                Globals.studentRepo.Update(student);
                Globals.DbHundler.Save();
                return Ok(assignment);
            }

            return BadRequest("Record Failed");
        }
    }
}
