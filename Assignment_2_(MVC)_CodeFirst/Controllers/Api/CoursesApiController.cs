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
    public class CoursesApiController : ApiController, IMyController<IHttpActionResult, CourseDto>
    {
        [HttpPost]
        public IHttpActionResult Create(CourseDto courseDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var course = Mapper.Map<CourseDto, Course>(courseDto);
            Repos.courseRepo.Add(course);
            Repos.DbHundler.Save();
            return Ok(courseDto);
        }
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var course = Repos.courseRepo.Get(id);
            if (course == null)
                return NotFound();
            Repos.courseRepo.Delete(course);
            Repos.DbHundler.Save();
            var courseDto = Mapper.Map<Course, CourseDto>(course);
            return Ok(courseDto);
        }
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            Course course = Repos.courseRepo.GetEmpty(id);
            if (course == null)
                return NotFound();
            var courseDto = Mapper.Map<Course, CourseDto>(course);
            return Ok(courseDto);
        }
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var courses = Repos.courseRepo.GetAllEmpty().Select(Mapper.Map<Course, CourseDto>);
            return Ok(courses);
        }
        [HttpPut]
        public IHttpActionResult Update(int id, CourseDto courseDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var course = Repos.courseRepo.Get(id);
            if (course == null)
                return NotFound();
            Mapper.Map(courseDto, course);
            Repos.DbHundler.Save();
            return StatusCode(HttpStatusCode.NoContent);
        }
        [Route("api/Courses/RemoveStudent"), HttpPost]
        public async Task<IHttpActionResult> RemoveStudentAsync([FromBody] CourseStudentData data)
        {
            if (data.studentId == null || data.courseId == null)
                return BadRequest();
            Course course = Repos.courseRepo.Get(data.courseId);
            Student student = Repos.studentRepo.Get(data.studentId);
            if (course == null || student == null)
                return BadRequest();

            course.Students.Remove(student);
            student.Courses.Remove(course);

            if (ModelState.IsValid)
            {
                Repos.courseRepo.Update(course);
                Repos.studentRepo.Update(student);
                _ = await Repos.DbHundler.SaveAsync();
                return Ok(200);
            }
            else
            {
                return BadRequest("Record Failed");
            }
        }
        [Route("api/Courses/AddStudents"), HttpPost]
        public async Task<IHttpActionResult> AddStudentsAsync([FromBody] List<CourseStudentData> data)
        {
            if (data.Count == 0)
                return BadRequest();
            var course = Repos.courseRepo.GetEmpty(data[0].courseId);
            if (course == null)
                return BadRequest();
            var studentsIds = data.Select(d => d.studentId).ToList();
            var students = Repos.studentRepo.GetAllByIdsEmpty(studentsIds);
            foreach (var student in students)
            {
                course.Students.Add(student);
            }
            _ = await Repos.DbHundler.SaveAsync();
            return Ok(200);
        }
    }
}
