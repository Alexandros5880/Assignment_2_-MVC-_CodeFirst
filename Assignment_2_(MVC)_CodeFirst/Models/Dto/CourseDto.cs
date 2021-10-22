using Assignment_2__MVC__CodeFirst.CustomAnotations;
using Assignment_2__MVC__CodeFirst.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment_2__MVC__CodeFirst.Models.Dto
{
    public class CourseDto : IDto
    {
        public int ID { get; set; }

        [Required]
        [MinLength(5)]
        public string Title { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //[CurrentDate(ErrorMessage = "Date must be after or equal to current date")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DateBigger(AddMonths = 1, ErrorMessage = "Date must be upper than date")]
        public DateTime EndDate { get; set; }

        [Required]
        public SchoolDto School { get; set; }

        [Required]
        public TrainerDto Trainer { get; set; }

        //public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}