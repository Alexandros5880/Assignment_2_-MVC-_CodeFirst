using Assignment_2__MVC__CodeFirst.Models.Dto;
using Assignment_2__MVC__CodeFirst.Models.Entities;
using Assignment_2__MVC__CodeFirst.Models.Other;
using Assignment_2__MVC__CodeFirst.Repositories;
using Assignment_2__MVC__CodeFirst.Static;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace Assignment_2__MVC__CodeFirst.Controllers.Api
{
    public class CoursesApiController : ApiController, IMyController<IHttpActionResult, CourseDto>, IDisposable
    {
        private Repos _repositories;
        private CourseRepo _courseRepo;
        private StudentRepo _studentRepo;
        public CoursesApiController(IRepos repo)
        {
            this._repositories = (Repos)repo;
            this._courseRepo = this._repositories.Courses;
            this._studentRepo = this._repositories.Students;
        }
        [HttpPost]
        public IHttpActionResult Create(CourseDto courseDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var course = Mapper.Map<CourseDto, Course>(courseDto);
            this._courseRepo.Add(course);
            this._courseRepo.Save();
            return Ok(courseDto);
        }
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var course = this._courseRepo.Get(id);
            if (course == null)
                return NotFound();
            this._courseRepo.Delete(course);
            this._courseRepo.Save();
            var courseDto = Mapper.Map<Course, CourseDto>(course);
            return Ok(courseDto);
        }
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            Course course = this._courseRepo.GetEmpty(id);
            if (course == null)
                return NotFound();
            var courseDto = Mapper.Map<Course, CourseDto>(course);
            return Ok(courseDto);
        }
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var courses = this._courseRepo.GetAllEmpty().Select(Mapper.Map<Course, CourseDto>);
            return Ok(courses);
        }
        [HttpPut]
        public IHttpActionResult Update(int id, CourseDto courseDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var course = this._courseRepo.Get(id);
            if (course == null)
                return NotFound();
            Mapper.Map(courseDto, course);
            this._courseRepo.Save();
            return StatusCode(HttpStatusCode.NoContent);
        }
        [Route("api/Courses/RemoveStudent"), HttpPost]
        public async Task<IHttpActionResult> RemoveStudentAsync([FromBody] CourseStudentData data)
        {
            if (data.studentId == null || data.courseId == null)
                return BadRequest();
            Course course = this._courseRepo.Get(data.courseId);
            Student student = this._studentRepo.Get(data.studentId);
            if (course == null || student == null)
                return BadRequest();

            course.Students.Remove(student);
            student.Courses.Remove(course);

            if (ModelState.IsValid)
            {
                this._courseRepo.Update(course);
                this._studentRepo.Update(student);
                _ = await this._courseRepo.SaveAsync();
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
            var course = this._courseRepo.GetEmpty(data[0].courseId);
            if (course == null)
                return BadRequest();
            var studentsIds = data.Select(d => d.studentId).ToList();
            var students = this._studentRepo.GetAllByIdsEmpty(studentsIds);
            foreach (var student in students)
            {
                course.Students.Add(student);
            }
            _ = await this._courseRepo.SaveAsync();
            return Ok(200);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this._repositories.Dispose();
                this._courseRepo.Dispose();
                this._studentRepo.Dispose();
                base.Dispose(disposing);
            }
            base.Dispose(disposing);
        }
    }
}
