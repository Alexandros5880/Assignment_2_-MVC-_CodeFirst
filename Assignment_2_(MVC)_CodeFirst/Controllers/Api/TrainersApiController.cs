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
    public class TrainersApiController : ApiController, IMyController<IHttpActionResult, TrainerDto>
    {
        private Repos _repositories;
        private TrainerRepo _trainerRepo;
        private CourseRepo _courseRepo;
        public TrainersApiController(IRepos repo)
        {
            this._repositories = (Repos)repo;
            this._trainerRepo = this._repositories.Trainers;
            this._courseRepo = this._repositories.Courses;
        }
        [HttpPost]
        public IHttpActionResult Create(TrainerDto trainerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var trainer = Mapper.Map<TrainerDto, Trainer>(trainerDto);
            this._trainerRepo.Add(trainer);
            this._trainerRepo.Save();
            return Ok(trainerDto);
        }
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var trainer = this._trainerRepo.Get(id);
            if (trainer == null)
                return NotFound();
            this._trainerRepo.Delete(trainer);
            this._trainerRepo.Save();
            var trainerDto = Mapper.Map<Trainer, TrainerDto>(trainer);
            return Ok(trainerDto);
        }
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var trainer = this._trainerRepo.GetEmpty(id);
            if (trainer == null)
                return NotFound();
            var trainerDto = Mapper.Map<Trainer, TrainerDto>(trainer);
            return Ok(trainerDto);
        }
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var trainers = this._trainerRepo.GetAllEmpty().Select(Mapper.Map<Trainer, TrainerDto>);
            return Ok(trainers);
        }
        [HttpPut]
        public IHttpActionResult Update(int id, TrainerDto trainerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var trainer = this._trainerRepo.Get(id);
            if (trainer == null)
                return NotFound();
            Mapper.Map(trainerDto, trainer);
            this._trainerRepo.Save();
            return StatusCode(HttpStatusCode.NoContent);
        }
        [Route("api/Trainers/RemoveCourse"), HttpPost]
        public async Task<IHttpActionResult> RemoveCourseAsync([FromBody] TrainerCourseData data)
        {
            if (data.trainerId == null || data.courseId == null)
                return BadRequest();
            Trainer trainer = this._trainerRepo.Get(data.trainerId);
            Course course = this._courseRepo.Get(data.courseId);
            if (trainer == null || course == null)
                return BadRequest();

            trainer.Courses.Remove(course);
            course.Trainer = null;

            if (ModelState.IsValid)
            {
                this._trainerRepo.Update(trainer);
                this._courseRepo.Update(course);
                _ = await this._trainerRepo.SaveAsync();
                return Ok(200);
            }
            return BadRequest("Record Failed");
        }
        [Route("api/Trainers/AddCourse"), HttpPost]
        public async Task<IHttpActionResult> AddCoursesAsync([FromBody] List<TrainerCourseData> data)
        {
            if (data.Count == 0)
                return BadRequest("data.Count == 0");
            var trainer = this._trainerRepo.GetEmpty(data[0].trainerId);
            if (trainer == null)
                return BadRequest("assignment == null");
            var coursesIds = data.Select(d => d.courseId).ToList();
            var courses = this._courseRepo.GetAllByIdsEmpty(coursesIds);
            foreach (var course in courses)
            {
                trainer.Courses.Add(course);
            }
            _ = await this._trainerRepo.SaveAsync();
            return Ok(200);
        }
    }
}
