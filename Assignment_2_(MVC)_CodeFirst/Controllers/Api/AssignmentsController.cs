using Assignment_2__MVC__CodeFirst.Models.Dto;
using Assignment_2__MVC__CodeFirst.Models.Entities;
using Assignment_2__MVC__CodeFirst.Models.Other;
using Assignment_2__MVC__CodeFirst.Static;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Assignment_2__MVC__CodeFirst.Controllers.Api
{
    public class AssignmentsController : ApiController, IMyController<IHttpActionResult, AssignmentDto>
    {
        [HttpPost]
        public IHttpActionResult Create(AssignmentDto assignmentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var assignment = Mapper.Map<AssignmentDto, Assignment>(assignmentDto);
            Globals.assignmentRepo.Add(assignment);
            Globals.DbHundler.Save();
            return Ok(assignmentDto);
        }
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var assignment = Globals.assignmentRepo.Get(id);
            if (assignment == null)
                return NotFound();
            Globals.assignmentRepo.Delete(assignment);
            Globals.DbHundler.Save();
            var assignmentDto = Mapper.Map<Assignment, AssignmentDto>(assignment);
            return Ok(assignmentDto);
        }
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var assignment = Globals.assignmentRepo.GetEmpty(id);
            if (assignment == null)
                return NotFound();
            var assignmentDto = Mapper.Map<Assignment, AssignmentDto>(assignment);
            return Ok(assignmentDto);
        }
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var assignments = Globals.assignmentRepo.GetAllEmpty().Select(Mapper.Map<Assignment, AssignmentDto>);
            return Ok(assignments);
        }
        [HttpPut]
        public IHttpActionResult Update(int id, AssignmentDto assignmentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var assignment = Globals.assignmentRepo.Get(id);
            if (assignment == null)
                return NotFound();
            Mapper.Map(assignmentDto, assignment);
            Globals.DbHundler.Save();
            return StatusCode(HttpStatusCode.NoContent);
        }

        [Route("api/v1/{Assignments}/{RemoveStudent}"), HttpPost]
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
