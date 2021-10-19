using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Assignment_2__MVC__CodeFirst.CustomAnotations;

namespace Assignment_2__MVC__CodeFirst.Models.Entities
{
    public class School : IMyEntities
    {
        public int ID { get; set; }

        [Required]
        [MinLength(5)]
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