using Assignment_2__MVC__CodeFirst.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment_2__MVC__CodeFirst.ViewModels
{
    public class CourseViewModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
        public int SchoolId { get; set; }
        public int TrainerId { get; set; }
        public IEnumerable<int> SelectedStudents { get; set; }
        public IEnumerable<Student> MyStudents { get; set; }
        public IEnumerable<SelectListItem> Students { get; set; }
        public SelectList Trainers { get; set; }
        public SelectList Schools { get; set; }
    }
}