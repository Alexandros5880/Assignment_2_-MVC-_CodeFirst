using Assignment_2__MVC__CodeFirst.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment_2__MVC__CodeFirst.ViewModels
{
    public class SchoolViewModel
    {
        public School School { get; set; }
        public ICollection<Course> Courses { get; set; }
        public ICollection<Trainer> Trainers { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}