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
    public class TrainersController : ApiController, IMyController<IHttpActionResult, TrainerDto>
    {
        [HttpPost]
        public IHttpActionResult Create(TrainerDto trainerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var trainer = Mapper.Map<TrainerDto, Trainer>(trainerDto);
            Globals.trainerRepo.Add(trainer);
            Globals.DbHundler.Save();
            return Ok(trainerDto);
        }
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var trainer = Globals.trainerRepo.Get(id);
            if (trainer == null)
                return NotFound();
            Globals.trainerRepo.Delete(trainer);
            Globals.DbHundler.Save();
            var trainerDto = Mapper.Map<Trainer, TrainerDto>(trainer);
            return Ok(trainerDto);
        }
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var trainer = Globals.trainerRepo.Get(id);
            if (trainer == null)
                return NotFound();
            var trainerDto = Mapper.Map<Trainer, TrainerDto>(trainer);
            return Ok(trainerDto);
        }
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var trainers = Globals.trainerRepo.GetAll().Select(Mapper.Map<Trainer, TrainerDto>);
            return Ok(trainers);
        }
        [HttpPut]
        public IHttpActionResult Update(int id, TrainerDto trainerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var trainer = Globals.trainerRepo.Get(id);
            if (trainer == null)
                return NotFound();
            Mapper.Map(trainerDto, trainer);
            Globals.DbHundler.Save();
            return StatusCode(HttpStatusCode.NoContent);
        }
        [Route("api/{Trainers}/{RemoveCourse}"), HttpPost]
        public IHttpActionResult RemoveCourse([FromBody] TrainerCourseData data)
        {
            if (data.trainerId == null || data.courseId == null)
                return BadRequest();
            Trainer trainer = Globals.trainerRepo.Get(data.trainerId);
            Course course = Globals.courseRepo.Get(data.courseId);
            if (trainer == null || course == null)
                return BadRequest();

            trainer.Courses.Remove(course);
            course.Trainer = null;

            if (ModelState.IsValid)
            {
                Globals.trainerRepo.Update(trainer);
                Globals.courseRepo.Update(course);
                Globals.DbHundler.Save();
                return Ok(trainer);
            }
            return BadRequest("Record Failed");
        }
    }
}
