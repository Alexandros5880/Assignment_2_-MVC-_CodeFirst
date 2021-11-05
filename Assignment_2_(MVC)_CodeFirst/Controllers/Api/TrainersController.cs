﻿using Assignment_2__MVC__CodeFirst.Models.Dto;
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
    public class TrainersController : ApiController, IMyController<IHttpActionResult, TrainerDto>
    {
        [HttpPost]
        public IHttpActionResult Create(TrainerDto trainerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var trainer = Mapper.Map<TrainerDto, Trainer>(trainerDto);
            Repos.trainerRepo.Add(trainer);
            Repos.DbHundler.Save();
            return Ok(trainerDto);
        }
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var trainer = Repos.trainerRepo.Get(id);
            if (trainer == null)
                return NotFound();
            Repos.trainerRepo.Delete(trainer);
            Repos.DbHundler.Save();
            var trainerDto = Mapper.Map<Trainer, TrainerDto>(trainer);
            return Ok(trainerDto);
        }
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var trainer = Repos.trainerRepo.GetEmpty(id);
            if (trainer == null)
                return NotFound();
            var trainerDto = Mapper.Map<Trainer, TrainerDto>(trainer);
            return Ok(trainerDto);
        }
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var trainers = Repos.trainerRepo.GetAllEmpty().Select(Mapper.Map<Trainer, TrainerDto>);
            return Ok(trainers);
        }
        [HttpPut]
        public IHttpActionResult Update(int id, TrainerDto trainerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var trainer = Repos.trainerRepo.Get(id);
            if (trainer == null)
                return NotFound();
            Mapper.Map(trainerDto, trainer);
            Repos.DbHundler.Save();
            return StatusCode(HttpStatusCode.NoContent);
        }
        [Route("api/Trainers/RemoveCourse"), HttpPost]
        public async Task<IHttpActionResult> RemoveCourseAsync([FromBody] TrainerCourseData data)
        {
            if (data.trainerId == null || data.courseId == null)
                return BadRequest();
            Trainer trainer = Repos.trainerRepo.Get(data.trainerId);
            Course course = Repos.courseRepo.Get(data.courseId);
            if (trainer == null || course == null)
                return BadRequest();

            trainer.Courses.Remove(course);
            course.Trainer = null;

            if (ModelState.IsValid)
            {
                Repos.trainerRepo.Update(trainer);
                Repos.courseRepo.Update(course);
                _ = await Repos.DbHundler.SaveAsync();
                return Ok(200);
            }
            return BadRequest("Record Failed");
        }
        [Route("api/Trainers/AddCourse"), HttpPost]
        public async Task<IHttpActionResult> AddCoursesAsync([FromBody] List<TrainerCourseData> data)
        {
            if (data.Count == 0)
                return BadRequest("data.Count == 0");
            var trainer = Repos.trainerRepo.GetEmpty(data[0].trainerId);
            if (trainer == null)
                return BadRequest("assignment == null");
            var coursesIds = data.Select(d => d.courseId).ToList();
            var courses = Repos.courseRepo.GetAllByIdsEmpty(coursesIds);
            foreach (var course in courses)
            {
                trainer.Courses.Add(course);
            }
            _ = await Repos.DbHundler.SaveAsync();
            return Ok(200);
        }
    }
}
