using Assignment_2__MVC__CodeFirst.Models.Dto;
using Assignment_2__MVC__CodeFirst.Models.Entities;
using Assignment_2__MVC__CodeFirst.Models.Other;
using Assignment_2__MVC__CodeFirst.Static;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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
            Repos.assignmentRepo.Add(assignment);
            Repos.DbHundler.Save();
            return Ok(assignmentDto);
        }
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var assignment = Repos.assignmentRepo.Get(id);
            if (assignment == null)
                return NotFound();
            Repos.assignmentRepo.Delete(assignment);
            Repos.DbHundler.Save();
            var assignmentDto = Mapper.Map<Assignment, AssignmentDto>(assignment);
            return Ok(assignmentDto);
        }
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var assignment = Repos.assignmentRepo.GetEmpty(id);
            if (assignment == null)
                return NotFound();
            var assignmentDto = Mapper.Map<Assignment, AssignmentDto>(assignment);
            return Ok(assignmentDto);
        }
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var assignments = Repos.assignmentRepo.GetAllEmpty().Select(Mapper.Map<Assignment, AssignmentDto>);
            return Ok(assignments);
        }
        [HttpPut]
        public IHttpActionResult Update(int id, AssignmentDto assignmentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var assignment = Repos.assignmentRepo.Get(id);
            if (assignment == null)
                return NotFound();
            Mapper.Map(assignmentDto, assignment);
            Repos.DbHundler.Save();
            return StatusCode(HttpStatusCode.NoContent);
        }
        [Route("api/Assignments/RemoveStudent"), HttpPost]
        public async Task<IHttpActionResult> RemoveStudentAsync([FromBody] AssignmentStudentData data)
        {
            if (data.assignmentId == null || data.studentId == null)
                return BadRequest();
            Assignment assignment = Repos.assignmentRepo.Get(data.assignmentId);
            Student student = Repos.studentRepo.Get(data.studentId);
            if (assignment == null || student == null)
                return BadRequest();

            assignment.Students.Remove(student);
            student.Assignments.Remove(assignment);

            if (ModelState.IsValid)
            {
                Repos.assignmentRepo.Update(assignment);
                Repos.studentRepo.Update(student);
                _ = await Repos.DbHundler.SaveAsync();
                return Ok(200);
            }

            return BadRequest("Record Failed");
        }
        [Route("api/Assignments/AddStudents"), HttpPost]
        public async Task<IHttpActionResult> AddStudentsAsync([FromBody] List<AssignmentStudentData> data)
        {
            if (data.Count == 0)
                return BadRequest("data.Count == 0");
            var assignment = Repos.assignmentRepo.GetEmpty(data[0].assignmentId);
            if (assignment == null)
                return BadRequest("assignment == null");
            var studentsIds = data.Select(d => d.studentId).ToList();
            var students = Repos.studentRepo.GetAllByIdsEmpty(studentsIds);
            foreach (var student in students)
            {
                assignment.Students.Add(student);
            }
            _ = await Repos.DbHundler.SaveAsync();
            return Ok(200);
        }
    }
}
