using Assignment_2__MVC__CodeFirst.Models.Entities;
using Assignment_2__MVC__CodeFirst.Static;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Assignment_2__MVC__CodeFirst.Controllers.Api
{
    public class TrainersController : ApiController, IMyController<IHttpActionResult, Trainer>
    {
        [HttpPost]
        public IHttpActionResult Create(Trainer obj)
        {
            Globals.trainerRepo.Add(obj);
            Globals.DbHundler.Save();
            return Ok(obj);
        }
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var trainer = Globals.trainerRepo.Get(id);
            Globals.trainerRepo.Delete(trainer);
            Globals.DbHundler.Save();
            return Ok(trainer);
        }
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var trainer = Globals.trainerRepo.Get(id);
            //trainer.Courses = Globals.trainerRepo.GetCourses(trainer.ID);
            return Ok(trainer);
        }
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var trainers = Globals.trainerRepo.GetAll();
            //foreach(var trainer in trainers)
            //{
            //    trainer.Courses = Globals.trainerRepo.GetCourses(trainer.ID);
            //}
            return Ok(trainers);
        }
        [HttpPut]
        public IHttpActionResult Update(int id, Trainer obj)
        {
            var trainer = Globals.trainerRepo.Get(id);
            trainer.FirstName = obj.FirstName;
            trainer.LastName = obj.LastName;
            trainer.StartDate = obj.StartDate;
            trainer.School = obj.School;
            trainer.Courses = obj.Courses;
            Globals.trainerRepo.Update(trainer);
            Globals.DbHundler.Save();
            return Ok(trainer);
        }
    }
}
