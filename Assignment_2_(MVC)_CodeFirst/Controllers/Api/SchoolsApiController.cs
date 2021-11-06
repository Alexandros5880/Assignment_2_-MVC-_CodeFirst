using Assignment_2__MVC__CodeFirst.Models.Dto;
using Assignment_2__MVC__CodeFirst.Models.Entities;
using Assignment_2__MVC__CodeFirst.Repositories;
using Assignment_2__MVC__CodeFirst.Static;
using AutoMapper;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace Assignment_2__MVC__CodeFirst.Controllers.Api
{
    public class SchoolsApiController : ApiController, IMyController<IHttpActionResult, SchoolDto>, IDisposable
    {
        private Repos _repositories;
        private SchoolRepo _schoolRepo;
        public SchoolsApiController(IRepos repo)
        {
            this._repositories = (Repos)repo;
            this._schoolRepo = this._repositories.Schools;
        }
        [HttpPost]
        public IHttpActionResult Create(SchoolDto schoolDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var school = Mapper.Map<SchoolDto, School>(schoolDto);
            this._schoolRepo.Add(school);
            this._schoolRepo.Save();
            return Ok(schoolDto);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var school = this._schoolRepo.Get(id);
            if (school == null)
                return NotFound();
            this._schoolRepo.Delete(school);
            this._schoolRepo.Save();
            var schoolDto = Mapper.Map<School, SchoolDto>(school);
            return Ok(schoolDto);
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            School school = this._schoolRepo.GetEmpty(id);
            if (school == null) return NotFound();
            return Ok(Mapper.Map<School, SchoolDto>(school));
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var schools = this._schoolRepo.GetAllEmpty().Select(Mapper.Map<School, SchoolDto>);
            return Ok(schools);
        }

        [HttpPut]
        public IHttpActionResult Update(int id, SchoolDto schoolDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var schoolDB = this._schoolRepo.Get(id);
            if (schoolDB == null)
                return NotFound();
            Mapper.Map(schoolDto, schoolDB);
            this._schoolRepo.Save();
            return StatusCode(HttpStatusCode.NoContent);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this._repositories.Dispose();
                this._schoolRepo.Dispose();
                base.Dispose(disposing);
            }
            base.Dispose(disposing);
        }
    }
}
