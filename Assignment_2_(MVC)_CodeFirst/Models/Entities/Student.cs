using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Assignment_2__MVC__CodeFirst.CustomAnotations;

namespace Assignment_2__MVC__CodeFirst.Models.Entities
{
    public class Student : IEntity
    {
        public int ID { get; set; }

        [Required]
        [MinLength(3)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3)]
        public string LastName { get; set; }

        [NotMapped]
        public string FullName { get => $"{this.FirstName} {this.LastName}"; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true)]
        //[CurrentDate(ErrorMessage = "Date must be after or equal to current date")]
        public DateTime StartDate { get; set; }

        [Required]
        public School School { get; set; }

        public ICollection<Course> Courses { get; set; } = new List<Course>();

        public ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();
    }
}