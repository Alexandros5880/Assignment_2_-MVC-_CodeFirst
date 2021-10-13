using Assignment_2__MVC__CodeFirst.Models;
using Assignment_2__MVC__CodeFirst.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment_2__MVC__CodeFirst.Static
{
    public static class Globals
    {
        public static RepositoryHundler DbHundler = new RepositoryHundler();
        public static SchoolRepo schoolRepo;
        public static CourseRepo courseRepo;
        public static AssignmentRepo assignmentRepo;
        public static TrainerRepo trainerRepo;
        public static StudentRepo studentRepo;
    }
}