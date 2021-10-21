namespace Assignment_2__MVC__CodeFirst.Migrations
{
    using Assignment_2__MVC__CodeFirst.Models.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Assignment_2__MVC__CodeFirst.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Assignment_2__MVC__CodeFirst.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            var schools = new List<School>()
            {
                new School()
                {
                    Name = "Ziridis",
                    StartDate = DateTime.Today.AddYears(-70)
                },
                new School()
                {
                    Name = "Geitonas",
                    StartDate = DateTime.Today.AddYears(-60)
                },
                new School()
                {
                    Name = "Kwsteas Geitonas",
                    StartDate = DateTime.Today.AddYears(-50)
                },
                new School()
                {
                    Name = "Collegio",
                    StartDate = DateTime.Today.AddYears(-60)
                },
                new School()
                {
                    Name = "Senlorance",
                    StartDate = DateTime.Today.AddYears(-50)
                }
            };
            schools.ForEach(s => context.Schools.AddOrUpdate(s));
            context.SaveChanges();

            // Fill School 1
            var trainers = new List<Trainer>()
            {
                new Trainer()
                {
                    FirstName = "Alexandros",
                    LastName = "Platanios",
                    StartDate = DateTime.Today.AddMonths(-3),
                    School = context.Schools.FirstOrDefault()
                },
                new Trainer()
                {
                    FirstName = "Antonis",
                    LastName = "Platanios",
                    StartDate = DateTime.Today.AddMonths(-3),
                    School = context.Schools.FirstOrDefault()
                },
                new Trainer()
                {
                    FirstName = "Miracle",
                    LastName = "Edwin",
                    StartDate = DateTime.Today.AddMonths(-3),
                    School = context.Schools.FirstOrDefault()
                },
                new Trainer()
                {
                    FirstName = "Alina",
                    LastName = "Matsa",
                    StartDate = DateTime.Today.AddMonths(-3),
                    School = context.Schools.FirstOrDefault()
                },
                new Trainer()
                {
                    FirstName = "Devine",
                    LastName = "Edwin",
                    StartDate = DateTime.Today.AddMonths(-3),
                    School = context.Schools.FirstOrDefault()
                },
                new Trainer()
                {
                    FirstName = "Mike",
                    LastName = "Matsas",
                    StartDate = DateTime.Today.AddMonths(-3),
                    School = context.Schools.FirstOrDefault()
                }
            };
            trainers.ForEach(t => context.Trainers.AddOrUpdate(t));
            context.SaveChanges();

            var courses = new List<Course>()
            {
                new Course()
                {
                    Title = "Mathimatika",
                    StartDate = DateTime.Today.AddYears(-5),
                    EndDate = DateTime.Today.AddYears(7),
                    School = context.Schools.FirstOrDefault(),
                    Trainer = context.Trainers.FirstOrDefault(t => t.ID == 1)
                },
                new Course()
                {
                    Title = "Algevra",
                    StartDate = DateTime.Today.AddYears(-3),
                    EndDate = DateTime.Today.AddYears(3),
                    School = context.Schools.FirstOrDefault(),
                    Trainer = context.Trainers.FirstOrDefault(t => t.ID == 2)
                },
                new Course()
                {
                    Title = "Biologia",
                    StartDate = DateTime.Today.AddYears(-7),
                    EndDate = DateTime.Today.AddYears(6),
                    School = context.Schools.FirstOrDefault(),
                    Trainer = context.Trainers.FirstOrDefault(t => t.ID == 3)
                },
                new Course()
                {
                    Title = "Programming",
                    StartDate = DateTime.Today.AddYears(-5),
                    EndDate = DateTime.Today.AddYears(11),
                    School = context.Schools.FirstOrDefault(),
                    Trainer = context.Trainers.FirstOrDefault(t => t.ID == 4)
                },
                new Course()
                {
                    Title = "Ekthesi",
                    StartDate = DateTime.Today.AddYears(-9),
                    EndDate = DateTime.Today.AddYears(9),
                    School = context.Schools.FirstOrDefault(),
                    Trainer = context.Trainers.FirstOrDefault(t => t.ID == 5)
                },
                new Course()
                {
                    Title = "History",
                    StartDate = DateTime.Today.AddYears(-10),
                    EndDate = DateTime.Today.AddYears(100),
                    School = context.Schools.FirstOrDefault(),
                    Trainer = context.Trainers.FirstOrDefault(t => t.ID == 6)
                }
            };
            courses.ForEach(c => context.Courses.AddOrUpdate(c));
            context.SaveChanges();

            var students = new List<Student>()
            {
                new Student()
                {
                    FirstName = "Alexandros",
                    LastName = "Platanios",
                    StartDate = DateTime.Today.AddMonths(-3),
                    School = context.Schools.FirstOrDefault(),
                    Courses = context.Courses.Where(c => c.School.ID == 1).ToList()
                },
                new Student()
                {
                    FirstName = "Antonis",
                    LastName = "Platanios",
                    StartDate = DateTime.Today.AddMonths(-3),
                    School = context.Schools.FirstOrDefault(),
                    Courses = context.Courses.Where(c => c.School.ID == 1).ToList()
                },
                new Student()
                {
                    FirstName = "Miracle",
                    LastName = "Edwin",
                    StartDate = DateTime.Today.AddMonths(-3),
                    School = context.Schools.FirstOrDefault(),
                    Courses = context.Courses.Where(c => c.School.ID == 1).ToList()
                },
                new Student()
                {
                    FirstName = "Alina",
                    LastName = "Matsa",
                    StartDate = DateTime.Today.AddMonths(-3),
                    School = context.Schools.FirstOrDefault(),
                    Courses = context.Courses.Where(c => c.School.ID == 1).ToList()
                },
                new Student()
                {
                    FirstName = "Devine",
                    LastName = "Edwin",
                    StartDate = DateTime.Today.AddMonths(-3),
                    School = context.Schools.FirstOrDefault(),
                    Courses = context.Courses.Where(c => c.School.ID == 1).ToList()
                },
                new Student()
                {
                    FirstName = "Mike",
                    LastName = "Matsas",
                    StartDate = DateTime.Today.AddMonths(-3),
                    School = context.Schools.FirstOrDefault(),
                    Courses = context.Courses.Where(c => c.School.ID == 1).ToList()
                }
            };
            students.ForEach(s => context.Students.AddOrUpdate(s));
            context.SaveChanges();

            var assignments = new List<Assignment>()
            {
                new Assignment()
                {
                    Title = "Assignment 1",
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddMonths(7),
                    School = context.Schools.FirstOrDefault(),
                    Students = context.Students.Where(s => s.School.ID == 1).ToList()
                },
                new Assignment()
                {
                    Title = "Assignment 2",
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddMonths(7),
                    School = context.Schools.FirstOrDefault(),
                    Students = context.Students.Where(s => s.School.ID == 1).ToList()
                },
                new Assignment()
                {
                    Title = "Assignment 3",
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddMonths(7),
                    School = context.Schools.FirstOrDefault(),
                    Students = context.Students.Where(s => s.School.ID == 1).ToList()
                },
                new Assignment()
                {
                    Title = "Assignment 4",
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddMonths(7),
                    School = context.Schools.FirstOrDefault(),
                    Students = context.Students.Where(s => s.School.ID == 1).ToList()
                },
                new Assignment()
                {
                    Title = "Assignment 5",
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddMonths(7),
                    School = context.Schools.FirstOrDefault(),
                    Students = context.Students.Where(s => s.School.ID == 1).ToList()
                },
                new Assignment()
                {
                    Title = "Assignment 6",
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddMonths(7),
                    School = context.Schools.FirstOrDefault(),
                    Students = context.Students.Where(s => s.School.ID == 1).ToList()
                }
            };
            assignments.ForEach(a => context.Assignments.AddOrUpdate(a));
            context.SaveChanges();

            // Fill School 2
            trainers = new List<Trainer>()
            {
                new Trainer()
                {
                    FirstName = "Alexandros2",
                    LastName = "Platanios2",
                    StartDate = DateTime.Today.AddMonths(-3),
                    School = context.Schools.FirstOrDefault(s => s.ID == 2)
                },
                new Trainer()
                {
                    FirstName = "Antonis2",
                    LastName = "Platanios2",
                    StartDate = DateTime.Today.AddMonths(-3),
                    School = context.Schools.FirstOrDefault(s => s.ID == 2)
                },
                new Trainer()
                {
                    FirstName = "Miracle2",
                    LastName = "Edwin2",
                    StartDate = DateTime.Today.AddMonths(-3),
                    School = context.Schools.FirstOrDefault(s => s.ID == 2)
                },
                new Trainer()
                {
                    FirstName = "Alina2",
                    LastName = "Matsa2",
                    StartDate = DateTime.Today.AddMonths(-3),
                    School = context.Schools.FirstOrDefault(s => s.ID == 2)
                },
                new Trainer()
                {
                    FirstName = "Devine2",
                    LastName = "Edwin2",
                    StartDate = DateTime.Today.AddMonths(-3),
                    School = context.Schools.FirstOrDefault(s => s.ID == 2)
                },
                new Trainer()
                {
                    FirstName = "Mike2",
                    LastName = "Matsas2",
                    StartDate = DateTime.Today.AddMonths(-3),
                    School = context.Schools.FirstOrDefault(s => s.ID == 2)
                }
            };
            trainers.ForEach(t => context.Trainers.AddOrUpdate(t));
            context.SaveChanges();

            courses = new List<Course>()
            {
                new Course()
                {
                    Title = "Mathimatika2",
                    StartDate = DateTime.Today.AddYears(-5),
                    EndDate = DateTime.Today.AddYears(7),
                    School = context.Schools.FirstOrDefault(s => s.ID == 2),
                    Trainer = context.Trainers.FirstOrDefault(t => t.ID == 7)
                },
                new Course()
                {
                    Title = "Algevra2",
                    StartDate = DateTime.Today.AddYears(-3),
                    EndDate = DateTime.Today.AddYears(3),
                    School = context.Schools.FirstOrDefault(s => s.ID == 2),
                    Trainer = context.Trainers.FirstOrDefault(t => t.ID == 8)
                },
                new Course()
                {
                    Title = "Biologia2",
                    StartDate = DateTime.Today.AddYears(-7),
                    EndDate = DateTime.Today.AddYears(6),
                    School = context.Schools.FirstOrDefault(s => s.ID == 2),
                    Trainer = context.Trainers.FirstOrDefault(t => t.ID == 9)
                },
                new Course()
                {
                    Title = "Programming2",
                    StartDate = DateTime.Today.AddYears(-5),
                    EndDate = DateTime.Today.AddYears(11),
                    School = context.Schools.FirstOrDefault(s => s.ID == 2),
                    Trainer = context.Trainers.FirstOrDefault(t => t.ID == 10)
                },
                new Course()
                {
                    Title = "Ekthesi2",
                    StartDate = DateTime.Today.AddYears(-9),
                    EndDate = DateTime.Today.AddYears(9),
                    School = context.Schools.FirstOrDefault(s => s.ID == 2),
                    Trainer = context.Trainers.FirstOrDefault(t => t.ID == 11)
                },
                new Course()
                {
                    Title = "History2",
                    StartDate = DateTime.Today.AddYears(-10),
                    EndDate = DateTime.Today.AddYears(100),
                    School = context.Schools.FirstOrDefault(s => s.ID == 2),
                    Trainer = context.Trainers.FirstOrDefault(t => t.ID == 12)
                }
            };
            courses.ForEach(c => context.Courses.AddOrUpdate(c));
            context.SaveChanges();

            students = new List<Student>()
            {
                new Student()
                {
                    FirstName = "Alexandros2",
                    LastName = "Platanios2",
                    StartDate = DateTime.Today.AddMonths(-3),
                    School = context.Schools.FirstOrDefault(s => s.ID == 2),
                    Courses = context.Courses.Where(s => s.School.ID == 2).ToList()
                },
                new Student()
                {
                    FirstName = "Antonis2",
                    LastName = "Platanios2",
                    StartDate = DateTime.Today.AddMonths(-3),
                    School = context.Schools.FirstOrDefault(s => s.ID == 2),
                    Courses = context.Courses.Where(s => s.School.ID == 2).ToList()
                },
                new Student()
                {
                    FirstName = "Miracle2",
                    LastName = "Edwin2",
                    StartDate = DateTime.Today.AddMonths(-3),
                    School = context.Schools.FirstOrDefault(s => s.ID == 2),
                    Courses = context.Courses.Where(s => s.School.ID == 2).ToList()
                },
                new Student()
                {
                    FirstName = "Alina2",
                    LastName = "Matsa2",
                    StartDate = DateTime.Today.AddMonths(-3),
                    School = context.Schools.FirstOrDefault(s => s.ID == 2),
                    Courses = context.Courses.Where(s => s.School.ID == 2).ToList()
                },
                new Student()
                {
                    FirstName = "Devine2",
                    LastName = "Edwin2",
                    StartDate = DateTime.Today.AddMonths(-3),
                    School = context.Schools.FirstOrDefault(s => s.ID == 2),
                    Courses = context.Courses.Where(s => s.School.ID == 2).ToList()
                },
                new Student()
                {
                    FirstName = "Mike2",
                    LastName = "Matsas2",
                    StartDate = DateTime.Today.AddMonths(-3),
                    School = context.Schools.FirstOrDefault(s => s.ID == 2),
                    Courses = context.Courses.Where(s => s.School.ID == 2).ToList()
                }
            };
            students.ForEach(s => context.Students.AddOrUpdate(s));
            context.SaveChanges();

            assignments = new List<Assignment>()
            {
                new Assignment()
                {
                    Title = "Assignment 1 2",
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddMonths(7),
                    School = context.Schools.FirstOrDefault(s => s.ID == 2),
                    Students = context.Students.Where(s => s.School.ID == 2).ToList()
                },
                new Assignment()
                {
                    Title = "Assignment 2 2",
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddMonths(7),
                    School = context.Schools.FirstOrDefault(s => s.ID == 2),
                    Students = context.Students.Where(s => s.School.ID == 2).ToList()
                },
                new Assignment()
                {
                    Title = "Assignment 3 2",
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddMonths(7),
                    School = context.Schools.FirstOrDefault(s => s.ID == 2),
                    Students = context.Students.Where(s => s.School.ID == 2).ToList()
                },
                new Assignment()
                {
                    Title = "Assignment 4 2",
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddMonths(7),
                    School = context.Schools.FirstOrDefault(s => s.ID == 2),
                    Students = context.Students.Where(s => s.School.ID == 2).ToList()
                },
                new Assignment()
                {
                    Title = "Assignment 5 2",
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddMonths(7),
                    School = context.Schools.FirstOrDefault(s => s.ID == 2),
                    Students = context.Students.Where(s => s.School.ID == 2).ToList()
                },
                new Assignment()
                {
                    Title = "Assignment 6 2",
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddMonths(7),
                    School = context.Schools.FirstOrDefault(s => s.ID == 2),
                    Students = context.Students.Where(s => s.School.ID == 2).ToList()
                }
            };
            assignments.ForEach(a => context.Assignments.AddOrUpdate(a));
            context.SaveChanges();

            // Fill School 3
            trainers = new List<Trainer>()
            {
                new Trainer()
                {
                    FirstName = "Alexandros3",
                    LastName = "Platanios3",
                    StartDate = DateTime.Today.AddMonths(-3),
                    School = context.Schools.FirstOrDefault(s => s.ID == 3)
                },
                new Trainer()
                {
                    FirstName = "Antonis3",
                    LastName = "Platanios3",
                    StartDate = DateTime.Today.AddMonths(-3),
                    School = context.Schools.FirstOrDefault(s => s.ID == 3)
                },
                new Trainer()
                {
                    FirstName = "Miracle3",
                    LastName = "Edwin3",
                    StartDate = DateTime.Today.AddMonths(-3),
                    School = context.Schools.FirstOrDefault(s => s.ID == 3)
                },
                new Trainer()
                {
                    FirstName = "Alina3",
                    LastName = "Matsa3",
                    StartDate = DateTime.Today.AddMonths(-3),
                    School = context.Schools.FirstOrDefault(s => s.ID == 3)
                },
                new Trainer()
                {
                    FirstName = "Devine3",
                    LastName = "Edwin3",
                    StartDate = DateTime.Today.AddMonths(-3),
                    School = context.Schools.FirstOrDefault(s => s.ID == 3)
                },
                new Trainer()
                {
                    FirstName = "Mike3",
                    LastName = "Matsas3",
                    StartDate = DateTime.Today.AddMonths(-3),
                    School = context.Schools.FirstOrDefault(s => s.ID == 3)
                }
            };
            trainers.ForEach(t => context.Trainers.AddOrUpdate(t));
            context.SaveChanges();

            courses = new List<Course>()
            {
                new Course()
                {
                    Title = "Mathimatika3",
                    StartDate = DateTime.Today.AddYears(-5),
                    EndDate = DateTime.Today.AddYears(7),
                    School = context.Schools.FirstOrDefault(s => s.ID == 3),
                    Trainer = context.Trainers.FirstOrDefault(t => t.ID == 13)
                },
                new Course()
                {
                    Title = "Algevra3",
                    StartDate = DateTime.Today.AddYears(-3),
                    EndDate = DateTime.Today.AddYears(3),
                    School = context.Schools.FirstOrDefault(s => s.ID == 3),
                    Trainer = context.Trainers.FirstOrDefault(t => t.ID == 14)
                },
                new Course()
                {
                    Title = "Biologia3",
                    StartDate = DateTime.Today.AddYears(-7),
                    EndDate = DateTime.Today.AddYears(6),
                    School = context.Schools.FirstOrDefault(s => s.ID == 3),
                    Trainer = context.Trainers.FirstOrDefault(t => t.ID == 15)
                },
                new Course()
                {
                    Title = "Programming3",
                    StartDate = DateTime.Today.AddYears(-5),
                    EndDate = DateTime.Today.AddYears(11),
                    School = context.Schools.FirstOrDefault(s => s.ID == 3),
                    Trainer = context.Trainers.FirstOrDefault(t => t.ID == 16)
                },
                new Course()
                {
                    Title = "Ekthesi3",
                    StartDate = DateTime.Today.AddYears(-9),
                    EndDate = DateTime.Today.AddYears(9),
                    School = context.Schools.FirstOrDefault(s => s.ID == 3),
                    Trainer = context.Trainers.FirstOrDefault(t => t.ID == 17)
                },
                new Course()
                {
                    Title = "History3",
                    StartDate = DateTime.Today.AddYears(-10),
                    EndDate = DateTime.Today.AddYears(100),
                    School = context.Schools.FirstOrDefault(s => s.ID == 3),
                    Trainer = context.Trainers.FirstOrDefault(t => t.ID == 18)
                }
            };
            courses.ForEach(c => context.Courses.AddOrUpdate(c));
            context.SaveChanges();

            students = new List<Student>()
            {
                new Student()
                {
                    FirstName = "Alexandros3",
                    LastName = "Platanios3",
                    StartDate = DateTime.Today.AddMonths(-3),
                    School = context.Schools.FirstOrDefault(s => s.ID == 3),
                    Courses = context.Courses.Where(s => s.School.ID == 3).ToList()
                },
                new Student()
                {
                    FirstName = "Antonis3",
                    LastName = "Platanios3",
                    StartDate = DateTime.Today.AddMonths(-3),
                    School = context.Schools.FirstOrDefault(s => s.ID == 3),
                    Courses = context.Courses.Where(s => s.School.ID == 3).ToList()
                },
                new Student()
                {
                    FirstName = "Miracle3",
                    LastName = "Edwin3",
                    StartDate = DateTime.Today.AddMonths(-3),
                    School = context.Schools.FirstOrDefault(s => s.ID == 3),
                    Courses = context.Courses.Where(s => s.School.ID == 3).ToList()
                },
                new Student()
                {
                    FirstName = "Alina3",
                    LastName = "Matsa3",
                    StartDate = DateTime.Today.AddMonths(-3),
                    School = context.Schools.FirstOrDefault(s => s.ID == 3),
                    Courses = context.Courses.Where(s => s.School.ID == 3).ToList()
                },
                new Student()
                {
                    FirstName = "Devine3",
                    LastName = "Edwin3",
                    StartDate = DateTime.Today.AddMonths(-3),
                    School = context.Schools.FirstOrDefault(s => s.ID == 3),
                    Courses = context.Courses.Where(s => s.School.ID == 3).ToList()
                },
                new Student()
                {
                    FirstName = "Mike3",
                    LastName = "Matsas3",
                    StartDate = DateTime.Today.AddMonths(-3),
                    School = context.Schools.FirstOrDefault(s => s.ID == 3),
                    Courses = context.Courses.Where(s => s.School.ID == 3).ToList()
                }
            };
            students.ForEach(s => context.Students.AddOrUpdate(s));
            context.SaveChanges();

            assignments = new List<Assignment>()
            {
                new Assignment()
                {
                    Title = "Assignment 1 3",
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddMonths(7),
                    School = context.Schools.FirstOrDefault(s => s.ID == 3),
                    Students = context.Students.Where(s => s.School.ID == 3).ToList()
                },
                new Assignment()
                {
                    Title = "Assignment 2 3",
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddMonths(7),
                    School = context.Schools.FirstOrDefault(s => s.ID == 3),
                    Students = context.Students.Where(s => s.School.ID == 3).ToList()
                },
                new Assignment()
                {
                    Title = "Assignment 3 3",
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddMonths(7),
                    School = context.Schools.FirstOrDefault(s => s.ID == 3),
                    Students = context.Students.Where(s => s.School.ID == 3).ToList()
                },
                new Assignment()
                {
                    Title = "Assignment 4 3",
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddMonths(7),
                    School = context.Schools.FirstOrDefault(s => s.ID == 3),
                    Students = context.Students.Where(s => s.School.ID == 3).ToList()
                },
                new Assignment()
                {
                    Title = "Assignment 5 3",
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddMonths(7),
                    School = context.Schools.FirstOrDefault(s => s.ID == 3),
                    Students = context.Students.Where(s => s.School.ID == 3).ToList()
                },
                new Assignment()
                {
                    Title = "Assignment 6 3",
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddMonths(7),
                    School = context.Schools.FirstOrDefault(s => s.ID == 3),
                    Students = context.Students.Where(s => s.School.ID == 3).ToList()
                }
            };
            assignments.ForEach(a => context.Assignments.AddOrUpdate(a));
            context.SaveChanges();

            // Fill School 4
            trainers = new List<Trainer>()
            {
                new Trainer()
                {
                    FirstName = "Alexandros4",
                    LastName = "Platanios4",
                    StartDate = DateTime.Today.AddMonths(-3),
                    School = context.Schools.FirstOrDefault(s => s.ID == 4)
                },
                new Trainer()
                {
                    FirstName = "Antonis4",
                    LastName = "Platanios4",
                    StartDate = DateTime.Today.AddMonths(-3),
                    School = context.Schools.FirstOrDefault(s => s.ID == 4)
                },
                new Trainer()
                {
                    FirstName = "Miracle4",
                    LastName = "Edwin4",
                    StartDate = DateTime.Today.AddMonths(-3),
                    School = context.Schools.FirstOrDefault(s => s.ID == 4)
                },
                new Trainer()
                {
                    FirstName = "Alina4",
                    LastName = "Matsa4",
                    StartDate = DateTime.Today.AddMonths(-3),
                    School = context.Schools.FirstOrDefault(s => s.ID == 4)
                },
                new Trainer()
                {
                    FirstName = "Devine4",
                    LastName = "Edwin4",
                    StartDate = DateTime.Today.AddMonths(-3),
                    School = context.Schools.FirstOrDefault(s => s.ID == 4)
                },
                new Trainer()
                {
                    FirstName = "Mike4",
                    LastName = "Matsas4",
                    StartDate = DateTime.Today.AddMonths(-3),
                    School = context.Schools.FirstOrDefault(s => s.ID == 4)
                }
            };
            trainers.ForEach(t => context.Trainers.AddOrUpdate(t));
            context.SaveChanges();

            courses = new List<Course>()
            {
                new Course()
                {
                    Title = "Mathimatika4",
                    StartDate = DateTime.Today.AddYears(-5),
                    EndDate = DateTime.Today.AddYears(7),
                    School = context.Schools.FirstOrDefault(s => s.ID == 4),
                    Trainer = context.Trainers.FirstOrDefault(t => t.ID == 19)
                },
                new Course()
                {
                    Title = "Algevra4",
                    StartDate = DateTime.Today.AddYears(-3),
                    EndDate = DateTime.Today.AddYears(3),
                    School = context.Schools.FirstOrDefault(s => s.ID == 4),
                    Trainer = context.Trainers.FirstOrDefault(t => t.ID == 20)
                },
                new Course()
                {
                    Title = "Biologia4",
                    StartDate = DateTime.Today.AddYears(-7),
                    EndDate = DateTime.Today.AddYears(6),
                    School = context.Schools.FirstOrDefault(s => s.ID == 4),
                    Trainer = context.Trainers.FirstOrDefault(t => t.ID == 21)
                },
                new Course()
                {
                    Title = "Programming4",
                    StartDate = DateTime.Today.AddYears(-5),
                    EndDate = DateTime.Today.AddYears(11),
                    School = context.Schools.FirstOrDefault(s => s.ID == 4),
                    Trainer = context.Trainers.FirstOrDefault(t => t.ID == 22)
                },
                new Course()
                {
                    Title = "Ekthesi4",
                    StartDate = DateTime.Today.AddYears(-9),
                    EndDate = DateTime.Today.AddYears(9),
                    School = context.Schools.FirstOrDefault(s => s.ID == 4),
                    Trainer = context.Trainers.FirstOrDefault(t => t.ID == 23)
                },
                new Course()
                {
                    Title = "History4",
                    StartDate = DateTime.Today.AddYears(-10),
                    EndDate = DateTime.Today.AddYears(100),
                    School = context.Schools.FirstOrDefault(s => s.ID == 4),
                    Trainer = context.Trainers.FirstOrDefault(t => t.ID == 24)
                }
            };
            courses.ForEach(c => context.Courses.AddOrUpdate(c));
            context.SaveChanges();

            students = new List<Student>()
            {
                new Student()
                {
                    FirstName = "Alexandros4",
                    LastName = "Platanios4",
                    StartDate = DateTime.Today.AddMonths(-3),
                    School = context.Schools.FirstOrDefault(s => s.ID == 4),
                    Courses = context.Courses.Where(s => s.School.ID == 4).ToList()
                },
                new Student()
                {
                    FirstName = "Antonis4",
                    LastName = "Platanios4",
                    StartDate = DateTime.Today.AddMonths(-3),
                    School = context.Schools.FirstOrDefault(s => s.ID == 4),
                    Courses = context.Courses.Where(s => s.School.ID == 4).ToList()
                },
                new Student()
                {
                    FirstName = "Miracle4",
                    LastName = "Edwin4",
                    StartDate = DateTime.Today.AddMonths(-3),
                    School = context.Schools.FirstOrDefault(s => s.ID == 4),
                    Courses = context.Courses.Where(s => s.School.ID == 4).ToList()
                },
                new Student()
                {
                    FirstName = "Alina4",
                    LastName = "Matsa4",
                    StartDate = DateTime.Today.AddMonths(-3),
                    School = context.Schools.FirstOrDefault(s => s.ID == 4),
                    Courses = context.Courses.Where(s => s.School.ID == 4).ToList()
                },
                new Student()
                {
                    FirstName = "Devine4",
                    LastName = "Edwin4",
                    StartDate = DateTime.Today.AddMonths(-3),
                    School = context.Schools.FirstOrDefault(s => s.ID == 4),
                    Courses = context.Courses.Where(s => s.School.ID == 4).ToList()
                },
                new Student()
                {
                    FirstName = "Mike4",
                    LastName = "Matsas4",
                    StartDate = DateTime.Today.AddMonths(-3),
                    School = context.Schools.FirstOrDefault(s => s.ID == 4),
                    Courses = context.Courses.Where(s => s.School.ID == 4).ToList()
                }
            };
            students.ForEach(s => context.Students.AddOrUpdate(s));
            context.SaveChanges();

            assignments = new List<Assignment>()
            {
                new Assignment()
                {
                    Title = "Assignment 1 4",
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddMonths(7),
                    School = context.Schools.FirstOrDefault(s => s.ID == 4),
                    Students = context.Students.Where(s => s.School.ID == 4).ToList()
                },
                new Assignment()
                {
                    Title = "Assignment 2 4",
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddMonths(7),
                    School = context.Schools.FirstOrDefault(s => s.ID == 4),
                    Students = context.Students.Where(s => s.School.ID == 4).ToList()
                },
                new Assignment()
                {
                    Title = "Assignment 3 4",
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddMonths(7),
                    School = context.Schools.FirstOrDefault(s => s.ID == 4),
                    Students = context.Students.Where(s => s.School.ID == 4).ToList()
                },
                new Assignment()
                {
                    Title = "Assignment 4 4",
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddMonths(7),
                    School = context.Schools.FirstOrDefault(s => s.ID == 4),
                    Students = context.Students.Where(s => s.School.ID == 4).ToList()
                },
                new Assignment()
                {
                    Title = "Assignment 5 4",
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddMonths(7),
                    School = context.Schools.FirstOrDefault(s => s.ID == 4),
                    Students = context.Students.Where(s => s.School.ID == 4).ToList()
                },
                new Assignment()
                {
                    Title = "Assignment 6 4",
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddMonths(7),
                    School = context.Schools.FirstOrDefault(s => s.ID == 4),
                    Students = context.Students.Where(s => s.School.ID == 4).ToList()
                }
            };
            assignments.ForEach(a => context.Assignments.AddOrUpdate(a));
            context.SaveChanges();

            // Fill School 5
            trainers = new List<Trainer>()
            {
                new Trainer()
                {
                    FirstName = "Alexandros5",
                    LastName = "Platanios5",
                    StartDate = DateTime.Today.AddMonths(-3),
                    School = context.Schools.FirstOrDefault(s => s.ID == 5)
                },
                new Trainer()
                {
                    FirstName = "Antonis5",
                    LastName = "Platanios5",
                    StartDate = DateTime.Today.AddMonths(-3),
                    School = context.Schools.FirstOrDefault(s => s.ID == 5)
                },
                new Trainer()
                {
                    FirstName = "Miracle5",
                    LastName = "Edwin5",
                    StartDate = DateTime.Today.AddMonths(-3),
                    School = context.Schools.FirstOrDefault(s => s.ID == 5)
                },
                new Trainer()
                {
                    FirstName = "Alina5",
                    LastName = "Matsa5",
                    StartDate = DateTime.Today.AddMonths(-3),
                    School = context.Schools.FirstOrDefault(s => s.ID == 5)
                },
                new Trainer()
                {
                    FirstName = "Devine5",
                    LastName = "Edwin5",
                    StartDate = DateTime.Today.AddMonths(-3),
                    School = context.Schools.FirstOrDefault(s => s.ID == 5)
                },
                new Trainer()
                {
                    FirstName = "Mike5",
                    LastName = "Matsas5",
                    StartDate = DateTime.Today.AddMonths(-3),
                    School = context.Schools.FirstOrDefault(s => s.ID == 5)
                }
            };
            trainers.ForEach(t => context.Trainers.AddOrUpdate(t));
            context.SaveChanges();

            courses = new List<Course>()
            {
                new Course()
                {
                    Title = "Mathimatika5",
                    StartDate = DateTime.Today.AddYears(-5),
                    EndDate = DateTime.Today.AddYears(7),
                    School = context.Schools.FirstOrDefault(s => s.ID == 5),
                    Trainer = context.Trainers.FirstOrDefault(t => t.ID == 25)
                },
                new Course()
                {
                    Title = "Algevra5",
                    StartDate = DateTime.Today.AddYears(-3),
                    EndDate = DateTime.Today.AddYears(3),
                    School = context.Schools.FirstOrDefault(s => s.ID == 5),
                    Trainer = context.Trainers.FirstOrDefault(t => t.ID == 26)
                },
                new Course()
                {
                    Title = "Biologia5",
                    StartDate = DateTime.Today.AddYears(-7),
                    EndDate = DateTime.Today.AddYears(6),
                    School = context.Schools.FirstOrDefault(s => s.ID == 5),
                    Trainer = context.Trainers.FirstOrDefault(t => t.ID == 27)
                },
                new Course()
                {
                    Title = "Programming5",
                    StartDate = DateTime.Today.AddYears(-5),
                    EndDate = DateTime.Today.AddYears(11),
                    School = context.Schools.FirstOrDefault(s => s.ID == 5),
                    Trainer = context.Trainers.FirstOrDefault(t => t.ID == 28)
                },
                new Course()
                {
                    Title = "Ekthesi5",
                    StartDate = DateTime.Today.AddYears(-9),
                    EndDate = DateTime.Today.AddYears(9),
                    School = context.Schools.FirstOrDefault(s => s.ID == 5),
                    Trainer = context.Trainers.FirstOrDefault(t => t.ID == 29)
                },
                new Course()
                {
                    Title = "History5",
                    StartDate = DateTime.Today.AddYears(-10),
                    EndDate = DateTime.Today.AddYears(100),
                    School = context.Schools.FirstOrDefault(s => s.ID == 5),
                    Trainer = context.Trainers.FirstOrDefault(t => t.ID == 30)
                }
            };
            courses.ForEach(c => context.Courses.AddOrUpdate(c));
            context.SaveChanges();

            students = new List<Student>()
            {
                new Student()
                {
                    FirstName = "Alexandros5",
                    LastName = "Platanios5",
                    StartDate = DateTime.Today.AddMonths(-3),
                    School = context.Schools.FirstOrDefault(s => s.ID == 5),
                    Courses = context.Courses.Where(s => s.School.ID == 5).ToList()
                },
                new Student()
                {
                    FirstName = "Antonis5",
                    LastName = "Platanios5",
                    StartDate = DateTime.Today.AddMonths(-3),
                    School = context.Schools.FirstOrDefault(s => s.ID == 5),
                    Courses = context.Courses.Where(s => s.School.ID == 5).ToList()
                },
                new Student()
                {
                    FirstName = "Miracle5",
                    LastName = "Edwin5",
                    StartDate = DateTime.Today.AddMonths(-3),
                    School = context.Schools.FirstOrDefault(s => s.ID == 5),
                    Courses = context.Courses.Where(s => s.School.ID == 5).ToList()
                },
                new Student()
                {
                    FirstName = "Alina5",
                    LastName = "Matsa5",
                    StartDate = DateTime.Today.AddMonths(-3),
                    School = context.Schools.FirstOrDefault(s => s.ID == 5),
                    Courses = context.Courses.Where(s => s.School.ID == 5).ToList()
                },
                new Student()
                {
                    FirstName = "Devine5",
                    LastName = "Edwin5",
                    StartDate = DateTime.Today.AddMonths(-3),
                    School = context.Schools.FirstOrDefault(s => s.ID == 5),
                    Courses = context.Courses.Where(s => s.School.ID == 5).ToList()
                },
                new Student()
                {
                    FirstName = "Mike5",
                    LastName = "Matsas5",
                    StartDate = DateTime.Today.AddMonths(-3),
                    School = context.Schools.FirstOrDefault(s => s.ID == 5),
                    Courses = context.Courses.Where(s => s.School.ID == 5).ToList()
                }
            };
            students.ForEach(s => context.Students.AddOrUpdate(s));
            context.SaveChanges();

            assignments = new List<Assignment>()
            {
                new Assignment()
                {
                    Title = "Assignment 1 5",
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddMonths(7),
                    School = context.Schools.FirstOrDefault(s => s.ID == 5),
                    Students = context.Students.Where(s => s.School.ID == 5).ToList()
                },
                new Assignment()
                {
                    Title = "Assignment 2 5",
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddMonths(7),
                    School = context.Schools.FirstOrDefault(s => s.ID == 5),
                    Students = context.Students.Where(s => s.School.ID == 5).ToList()
                },
                new Assignment()
                {
                    Title = "Assignment 3 5",
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddMonths(7),
                    School = context.Schools.FirstOrDefault(s => s.ID == 5),
                    Students = context.Students.Where(s => s.School.ID == 5).ToList()
                },
                new Assignment()
                {
                    Title = "Assignment 4 5",
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddMonths(7),
                    School = context.Schools.FirstOrDefault(s => s.ID == 5),
                    Students = context.Students.Where(s => s.School.ID == 5).ToList()
                },
                new Assignment()
                {
                    Title = "Assignment 5 5",
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddMonths(7),
                    School = context.Schools.FirstOrDefault(s => s.ID == 5),
                    Students = context.Students.Where(s => s.School.ID == 5).ToList()
                },
                new Assignment()
                {
                    Title = "Assignment 6 5",
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddMonths(7),
                    School = context.Schools.FirstOrDefault(s => s.ID == 5),
                    Students = context.Students.Where(s => s.School.ID == 5).ToList()
                }
            };
            assignments.ForEach(a => context.Assignments.AddOrUpdate(a));
            context.SaveChanges();
        }
    }
}
