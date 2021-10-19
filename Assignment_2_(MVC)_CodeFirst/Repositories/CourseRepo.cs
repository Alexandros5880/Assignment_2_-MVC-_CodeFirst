using Assignment_2__MVC__CodeFirst.Models;
using Assignment_2__MVC__CodeFirst.Models.Entities;
using Assignment_2__MVC__CodeFirst.Static;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Assignment_2__MVC__CodeFirst.Repositories
{
    public class CourseRepo : IRepository<Course>, IDisposable
    {
        private ApplicationDbContext _context;
        private bool disposedValue;

        public CourseRepo(ApplicationDbContext context)
        {
            this._context = context;
        }

        public void Add(Course obj)
        {
            this._context.Courses.Add(obj);
        }

        public void Delete(Course obj)
        {
            Course course = this._context.Courses
                                .Find(obj.ID);
            this._context.Courses.Remove(course);
        }

        public Course Get(int? id)
        {
            return this._context.Courses
                    .FirstOrDefault(c => c.ID == id);
        }

        public IEnumerable<Course> GetAll()
        {
            return this._context.Courses;
        }

        public void Update(Course obj)
        {
            this._context.Entry(obj).State = EntityState.Modified;
        }

        public ICollection<Student> GetStudents(int id)
        {
            return Globals.studentRepo.GetAll()
                .Where(s => s.Courses.Contains(this.Get(id)))
                .ToList();
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