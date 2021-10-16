using Assignment_2__MVC__CodeFirst.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment_2__MVC__CodeFirst.ViewModels
{
    public class TrainerViewModel
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        public int SchoolId { get; set; }
        public SelectList Schools { get; set; }
        public IEnumerable<int> SelectedCourses { get; set; }
        public IEnumerable<Course> MyCourses { get; set; }
        public IEnumerable<SelectListItem> Courses { get; set; }
    }
}