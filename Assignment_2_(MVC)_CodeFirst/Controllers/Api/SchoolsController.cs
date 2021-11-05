using Assignment_2__MVC__CodeFirst.Models.Dto;
using Assignment_2__MVC__CodeFirst.Models.Entities;
using Assignment_2__MVC__CodeFirst.Static;
using AutoMapper;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace Assignment_2__MVC__CodeFirst.Controllers.Api
{
    public class SchoolsController : ApiController, IMyController<IHttpActionResult, SchoolDto>
    {
        [HttpPost]
        public IHttpActionResult Create(SchoolDto schoolDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var school = Mapper.Map<SchoolDto, School>(schoolDto);
            Repos.schoolRepo.Add(school);
            Repos.DbHundler.Save();
            return Ok(schoolDto);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var school = Repos.schoolRepo.Get(id);
            if (school == null)
                return NotFound();
            Repos.schoolRepo.Delete(school);
            Repos.DbHundler.Save();
            var schoolDto = Mapper.Map<School, SchoolDto>(school);
            return Ok(schoolDto);
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            School school = Repos.schoolRepo.GetEmpty(id);
            if (school == null) return NotFound();
            return Ok(Mapper.Map<School, SchoolDto>(school));
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var schools = Repos.schoolRepo.GetAllEmpty().Select(Mapper.Map<School, SchoolDto>);
            return Ok(schools);
        }

        [HttpPut]
        public IHttpActionResult Update(int id, SchoolDto schoolDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var schoolDB = Repos.schoolRepo.Get(id);
            if (schoolDB == null)
                return NotFound();
            Mapper.Map(schoolDto, schoolDB);
            Repos.DbHundler.Save();
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
