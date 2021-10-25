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
    //[Route("api/[controller]")]
    public class CoursesController : ApiController, IMyController<IHttpActionResult, CourseDto>
    {
        [HttpPost]
        public IHttpActionResult Create(CourseDto courseDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var course = Mapper.Map<CourseDto, Course>(courseDto);
            Globals.courseRepo.Add(course);
            Globals.DbHundler.Save();
            return Ok(courseDto);
        }
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var course = Globals.courseRepo.Get(id);
            if (course == null)
                return NotFound();
            Globals.courseRepo.Delete(course);
            Globals.DbHundler.Save();
            var courseDto = Mapper.Map<Course, CourseDto>(course);
            return Ok(courseDto);
        }
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            Course course = Globals.courseRepo.GetEmpty(id);
            if (course == null)
                return NotFound();
            var courseDto = Mapper.Map<Course, CourseDto>(course);
            return Ok(courseDto);
        }
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var courses = Globals.courseRepo.GetAllEmpty().Select(Mapper.Map<Course, CourseDto>);
            return Ok(courses);
        }
        [HttpPut]
        public IHttpActionResult Update(int id, CourseDto courseDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var course = Globals.courseRepo.Get(id);
            if (course == null)
                return NotFound();
            Mapper.Map(courseDto, course);
            Globals.DbHundler.Save();
            return StatusCode(HttpStatusCode.NoContent);
        }
        [Route("api/Courses/RemoveStudent"), HttpPost]
        public IHttpActionResult RemoveStudent([FromBody] CourseStudentData data)
        {
            if (data.studentId == null || data.courseId == null)
                return BadRequest();
            Course course = Globals.courseRepo.Get(data.courseId);
            Student student = Globals.studentRepo.Get(data.studentId);
            if (course == null || student == null)
                return BadRequest();

            course.Students.Remove(student);
            student.Courses.Remove(course);

            if (ModelState.IsValid)
            {
                Globals.courseRepo.Update(course);
                Globals.studentRepo.Update(student);
                Globals.DbHundler.Save();
                return Ok(200);
            }
            else
            {
                return BadRequest("Record Failed");
            }
        }
    }
}
