using Assignment_2__MVC__CodeFirst.CustomAnotations;
using Assignment_2__MVC__CodeFirst.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment_2__MVC__CodeFirst.ViewModels
{
    public class StudentViewModel
    {
        public int ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        public int SchoolID { get; set; }

        public ICollection<Course> Courses { get; set; }

        public ICollection<Assignment> Assignments { get; set; }

        public SelectList AllCourses { get; set; }
        public SelectList AllSchools { get; set; }
    }
}