﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Assignment_2__MVC__CodeFirst.CustomAnotations;

namespace Assignment_2__MVC__CodeFirst.Models.Entities
{
    public class School
    {
        public int ID { get; set; }

        [Required]
        [MinLength(5)]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [CurrentDate(ErrorMessage = "Date must be after or equal to current date")]
        public DateTime StartDate { get; set; }

        public ICollection<Course> Courses { get; set; }
        public ICollection<Trainer> Trainers { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}