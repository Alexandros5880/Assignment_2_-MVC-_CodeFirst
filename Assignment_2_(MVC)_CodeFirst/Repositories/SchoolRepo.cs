using Assignment_2__MVC__CodeFirst.Models;
using Assignment_2__MVC__CodeFirst.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System;

namespace Assignment_2__MVC__CodeFirst.Repositories
{
    public class SchoolRepo : IRepository<School>, IDisposable
    {
        private ApplicationDbContext _context;
        private bool disposedValue;

        public SchoolRepo(ApplicationDbContext context)
        {
            this._context = context;
        }

        public void Add(School obj)
        {
            this._context.Schools.Add(obj);
        }

        public void Delete(School obj)
        {
            School school = this._context.Schools
                                .Find(obj.ID);
            this._context.Schools.Remove(school);
        }

        public School Get(int? id)
        {
            return this._context.Schools.Include(s => s.Courses.Select(c => c.Students))
                                        .Include(s => s.Trainers.Select(t => t.Courses))
                                        .Include(s => s.Students.Select(st => st.Courses))
                                        .FirstOrDefault(s => s.ID == id);
        }

        public IEnumerable<School> GetAll()
        {
            return this._context.Schools.Include(s => s.Courses.Select(c => c.Students))
                                        .Include(s => s.Trainers.Select(t => t.Courses))
                                        .Include(s => s.Students.Select(st => st.Courses));
        }

        public void Update(School obj)
        {
            this._context.Entry(obj).State = EntityState.Modified;
        }

        public void AddCourse(int id, Course course)
        {
            School school = this._context.Schools
                                .Include(s => s.Courses)
                                .FirstOrDefault(s => s.ID == id);
            school.Courses.Add(course);
        }

        public void AddTrainer(int id, Trainer trainer)
        {
            School school = this._context.Schools
                                .Include(s => s.Trainers)
                                .FirstOrDefault(s => s.ID == id);
            school.Trainers.Add(trainer);
        }

        public void AddStudent(int id, Student student)
        {
            School school = this._context.Schools
                                .Include(s => s.Students)
                                .FirstOrDefault(s => s.ID == id);
            school.Students.Add(student);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    ((IDisposable)_context).Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}