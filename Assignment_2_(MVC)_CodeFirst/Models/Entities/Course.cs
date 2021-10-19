using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Assignment_2__MVC__CodeFirst.CustomAnotations;

namespace Assignment_2__MVC__CodeFirst.Models.Entities
{
    public class Course : IMyEntities
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
        public School School { get; set; }

        [Required]
        public Trainer Trainer { get; set; }

        public ICollection<Student> Students { get; set; }

    }
}