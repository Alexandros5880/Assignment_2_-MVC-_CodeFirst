using Assignment_2__MVC__CodeFirst.Models.Dto;
using Assignment_2__MVC__CodeFirst.Models.Entities;
using Assignment_2__MVC__CodeFirst.Models.Other;
using Assignment_2__MVC__CodeFirst.Static;
using AutoMapper;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace Assignment_2__MVC__CodeFirst.Controllers.Api
{
    public class StudentsController : ApiController, IMyController<IHttpActionResult, StudentDto>
    {
        [HttpPost]
        public IHttpActionResult Create(StudentDto studentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var student = Mapper.Map<StudentDto, Student>(studentDto);
            Globals.studentRepo.Add(student);
            Globals.DbHundler.Save();
            return Ok(studentDto);
        }
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var student = Globals.studentRepo.Get(id);
            if (student == null)
                return BadRequest();
            Globals.studentRepo.Delete(student);
            Globals.DbHundler.Save();
            var studentDto = Mapper.Map<Student, StudentDto>(student);
            return Ok(studentDto);
        }
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var student = Globals.studentRepo.GetEmpty(id);
            if (student == null)
                return NotFound();
            var studentDto = Mapper.Map<Student, StudentDto>(student);
            return Ok(studentDto);
        }
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var students = Globals.studentRepo.GetAllEmpty().Select(Mapper.Map<Student, StudentDto>);
            return Ok(students);
        }
        [HttpPut]
        public IHttpActionResult Update(int id, StudentDto studentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var student = Globals.studentRepo.Get(id);
            if (student == null)
                return NotFound();
            Mapper.Map(studentDto, student);
            Globals.DbHundler.Save();
            return StatusCode(HttpStatusCode.NoContent);
        }
        [Route("api/Students/RemoveCourse"), HttpPost]
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
                return Ok(200);
            }
            return BadRequest("Record Failed");
        }
        [Route("api/Students/RemoveAssignment"), HttpPost]
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
                return Ok(200);
            }
            return BadRequest("Record Failed");
        }
    }
}
