using Assignment_2__MVC__CodeFirst.CustomAnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Assignment_2__MVC__CodeFirst.Models.Entities
{
    public class Assignment : IEntity
    {
        public int ID { get; set; }

        [Required]
        [MinLength(5)]
        public string Title { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true)]
        //[CurrentDate(ErrorMessage = "Date must be after or equal to current date")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true)]
        [DateBigger(AddMonths = 1, ErrorMessage = "Date must be smaller than date")]
        public DateTime EndDate { get; set; }

        public ICollection<Student> Students { get; set; } = new List<Student>();

        [Required]
        public School School { get; set; }
    }
}