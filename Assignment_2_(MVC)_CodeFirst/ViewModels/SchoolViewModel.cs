using Assignment_2__MVC__CodeFirst.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment_2__MVC__CodeFirst.ViewModels
{
    public class SchoolViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        public ICollection<Course> Courses { get; set; }
        public ICollection<Assignment> Assignments { get; set; }
        public ICollection<Trainer> Trainers { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}