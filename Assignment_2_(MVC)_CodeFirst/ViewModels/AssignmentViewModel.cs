using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Assignment_2__MVC__CodeFirst.CustomAnotations;
using Assignment_2__MVC__CodeFirst.Models.Entities;
using System.Web.Mvc;

namespace Assignment_2__MVC__CodeFirst.ViewModels
{
    public class AssignmentViewModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DateBigger(AddMonths = 1, ErrorMessage = "Date must be smaller than date")]
        public DateTime EndDate { get; set; }
        public IEnumerable<int> SelectedStudents { get; set; }
        public IEnumerable<Student> MyStudents { get; set; }
        public IEnumerable<SelectListItem> Students { get; set; }
        public int SchoolId { get; set; }
        public SelectList Schools { get; set; }
    }
}