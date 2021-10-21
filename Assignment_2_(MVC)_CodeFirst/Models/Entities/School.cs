using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Assignment_2__MVC__CodeFirst.CustomAnotations;

namespace Assignment_2__MVC__CodeFirst.Models.Entities
{
    public class School : IEntity
    {
        public int ID { get; set; }

        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        public ICollection<Course> Courses { get; set; } = new List<Course>();
        public ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();
        public ICollection<Trainer> Trainers { get; set; } = new List<Trainer>();
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}