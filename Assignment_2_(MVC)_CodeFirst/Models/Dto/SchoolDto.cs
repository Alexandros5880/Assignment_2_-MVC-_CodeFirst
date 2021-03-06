using Assignment_2__MVC__CodeFirst.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment_2__MVC__CodeFirst.Models.Dto
{
    public class SchoolDto : IDto
    {
        public int ID { get; set; }

        [Required]
        [MinLength(5)]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        //public ICollection<Course> Courses { get; set; } = new List<Course>();
        //public ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();
        //public ICollection<Trainer> Trainers { get; set; } = new List<Trainer>();
        //public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}