using Assignment_2__MVC__CodeFirst.Models.Dto;
using Assignment_2__MVC__CodeFirst.Models.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment_2__MVC__CodeFirst.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Mapper.CreateMap<Album, AlbumDto>()
            //    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Title));

            Mapper.CreateMap<School, SchoolDto>();
            Mapper.CreateMap<SchoolDto, School>();

            Mapper.CreateMap<Course, CourseDto>();
            Mapper.CreateMap<CourseDto, Course>();

            Mapper.CreateMap<Assignment, AssignmentDto>();
            Mapper.CreateMap<AssignmentDto, Assignment>();

            Mapper.CreateMap<Trainer, TrainerDto>();
            Mapper.CreateMap<TrainerDto, Trainer>();

            Mapper.CreateMap<Student, StudentDto>();
            Mapper.CreateMap<StudentDto, Student>();
        }
    }
}