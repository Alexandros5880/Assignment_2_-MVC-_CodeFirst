using Assignment_2__MVC__CodeFirst.Models.Dto;
using Assignment_2__MVC__CodeFirst.Models.Entities;
using Assignment_2__MVC__CodeFirst.Models.Other;
using Assignment_2__MVC__CodeFirst.Repositories;
using Assignment_2__MVC__CodeFirst.Static;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace Assignment_2__MVC__CodeFirst.Controllers.Api
{
    public class StudentsApiController : ApiController, IMyController<IHttpActionResult, StudentDto>
    {
        private Repos _repositories;
        private StudentRepo _studentRepo;
        private CourseRepo _courseRepo;
        private AssignmentRepo _assignmentRepo;
        public StudentsApiController(IRepos repo)
        {
            this._repositories = (Repos)repo;
            this._studentRepo = this._repositories.Students;
            this._courseRepo = this._repositories.Courses;
            this._assignmentRepo = this._repositories.Assignments;
        }
        [HttpPost]
        public IHttpActionResult Create(StudentDto studentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var student = Mapper.Map<StudentDto, Student>(studentDto);
            this._studentRepo.Add(student);
            this._studentRepo.Save();
            return Ok(studentDto);
        }
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var student = this._studentRepo.Get(id);
            if (student == null)
                return BadRequest();
            this._studentRepo.Delete(student);
            this._studentRepo.Save();
            var studentDto = Mapper.Map<Student, StudentDto>(student);
            return Ok(studentDto);
        }
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var student = this._studentRepo.GetEmpty(id);
            if (student == null)
                return NotFound();
            var studentDto = Mapper.Map<Student, StudentDto>(student);
            return Ok(studentDto);
        }
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var students = this._studentRepo.GetAllEmpty().Select(Mapper.Map<Student, StudentDto>);
            return Ok(students);
        }
        [HttpPut]
        public IHttpActionResult Update(int id, StudentDto studentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var student = this._studentRepo.Get(id);
            if (student == null)
                return NotFound();
            Mapper.Map(studentDto, student);
            this._studentRepo.Save();
            return StatusCode(HttpStatusCode.NoContent);
        }
        [Route("api/Students/RemoveCourse"), HttpPost]
        public async Task<IHttpActionResult> RemoveCourseAsync([FromBody] StudentCourseData data)
        {
            if (data.studentId == null || data.courseId == null)
                return BadRequest();
            Student student = this._studentRepo.Get(data.studentId);
            Course course = this._courseRepo.Get(data.courseId);
            if (student == null || course == null)
                return BadRequest();
            student.Courses.Remove(course);
            course.Students.Remove(student);
            if (ModelState.IsValid)
            {
                this._studentRepo.Update(student);
                this._courseRepo.Update(course);
                _ = await this._studentRepo.SaveAsync();
                return Ok(200);
            }
            return BadRequest("Record Failed");
        }
        [Route("api/Students/RemoveAssignment"), HttpPost]
        public async Task<IHttpActionResult> RemoveAssignmentAsync([FromBody] StudentAssignmentData data)
        {
            if (data.studentId == null || data.assignmentId == null)
                return BadRequest();
            Student student = this._studentRepo.Get(data.studentId);
            Assignment assignment = this._assignmentRepo.Get(data.assignmentId);
            if (student == null || assignment == null)
                return BadRequest();
            student.Assignments.Remove(assignment);
            assignment.Students.Remove(student);
            if (ModelState.IsValid)
            {
                this._studentRepo.Update(student);
                this._assignmentRepo.Update(assignment);
                _ = await this._studentRepo.SaveAsync();
                return Ok(200);
            }
            return BadRequest("Record Failed");
        }
        [Route("api/Students/AddCourse"), HttpPost]
        public async Task<IHttpActionResult> AddCoursesAsync([FromBody] List<StudentCourseData> data)
        {
            if (data.Count == 0)
                return BadRequest("data.Count == 0");
            var student = this._studentRepo.GetEmpty(data[0].studentId);
            if (student == null)
                return BadRequest("student == null");
            var coursesIds = data.Select(d => d.courseId).ToList();
            var courses = this._courseRepo.GetAllByIdsEmpty(coursesIds);
            foreach (var course in courses)
            {
                student.Courses.Add(course);
            }
            _ = await this._studentRepo.SaveAsync();
            return Ok(200);
        }
        [Route("api/Students/AddAssingment"), HttpPost]
        public async Task<IHttpActionResult> AddAssignmentsAsync([FromBody] List<StudentAssignmentData> data)
        {
            if (data.Count == 0)
                return BadRequest("data.Count == 0");
            var student = this._studentRepo.GetEmpty(data[0].studentId);
            if (student == null)
                return BadRequest("student == null");
            var assignmentsIds = data.Select(d => d.assignmentId).ToList();
            var assignments = this._assignmentRepo.GetAllByIdsEmpty(assignmentsIds);
            foreach (var assignment in assignments)
            {
                student.Assignments.Add(assignment);
            }
            _ = await this._studentRepo.SaveAsync();
            return Ok(200);
        }
    }
}
