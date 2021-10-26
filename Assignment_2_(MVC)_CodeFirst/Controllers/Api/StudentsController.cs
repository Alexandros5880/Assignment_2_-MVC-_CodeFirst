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
        public async Task<IHttpActionResult> RemoveCourseAsync([FromBody] StudentCourseData data)
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
                _ = await Globals.DbHundler.SaveAsync();
                return Ok(200);
            }
            return BadRequest("Record Failed");
        }
        [Route("api/Students/RemoveAssignment"), HttpPost]
        public async Task<IHttpActionResult> RemoveAssignmentAsync([FromBody] StudentAssignmentData data)
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
                _ = await Globals.DbHundler.SaveAsync();
                return Ok(200);
            }
            return BadRequest("Record Failed");
        }
        [Route("api/Students/AddCourse"), HttpPost]
        public async Task<IHttpActionResult> AddCoursesAsync([FromBody] List<StudentCourseData> data)
        {
            if (data.Count == 0)
                return BadRequest("data.Count == 0");
            var student = Globals.studentRepo.GetEmpty(data[0].studentId);
            if (student == null)
                return BadRequest("student == null");
            var coursesIds = data.Select(d => d.courseId).ToList();
            var courses = Globals.courseRepo.GetAllByIdsEmpty(coursesIds);
            foreach (var course in courses)
            {
                student.Courses.Add(course);
            }
            _ = await Globals.DbHundler.SaveAsync();
            return Ok(200);
        }
        [Route("api/Students/AddAssingment"), HttpPost]
        public async Task<IHttpActionResult> AddAssignmentsAsync([FromBody] List<StudentAssignmentData> data)
        {
            if (data.Count == 0)
                return BadRequest("data.Count == 0");
            var student = Globals.studentRepo.GetEmpty(data[0].studentId);
            if (student == null)
                return BadRequest("student == null");
            var assignmentsIds = data.Select(d => d.assignmentId).ToList();
            var assignments = Globals.assignmentRepo.GetAllByIdsEmpty(assignmentsIds);
            foreach (var assignment in assignments)
            {
                student.Assignments.Add(assignment);
            }
            _ = await Globals.DbHundler.SaveAsync();
            return Ok(200);
        }
    }
}
