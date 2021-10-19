using Assignment_2__MVC__CodeFirst.Models;
using Assignment_2__MVC__CodeFirst.Models.Entities;
using Assignment_2__MVC__CodeFirst.Static;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Assignment_2__MVC__CodeFirst.Repositories
{
    public class StudentRepo : IRepository<Student>, IDisposable
    {
        private ApplicationDbContext _context;
        private bool disposedValue;

        public StudentRepo(ApplicationDbContext context)
        {
            this._context = context;
        }

        public void Add(Student obj)
        {
            this._context.Students.Add(obj);
        }

        public void Delete(Student obj)
        {
            Student student = this._context.Students
                                  .Find(obj.ID);
            this._context.Students.Remove(student);
        }

        public Student Get(int? id)
        {
            return this._context.Students
                .FirstOrDefault(s => s.ID == id);
        }

        public IEnumerable<Student> GetAll()
        {
            return this._context.Students;
        }

        public void Update(Student obj)
        {
            this._context.Entry(obj).State = EntityState.Modified;
        }

        public ICollection<Course> GetCourses(int id)
        {
            return Globals.courseRepo.GetAll()
                .Where(c => c.Students.Contains(this.Get(id)))
                .ToList();
        }

        public ICollection<Assignment> GetAssignments(int id)
        {
            return Globals.assignmentRepo.GetAll()
                .Where(a => a.Students.Contains(this.Get(id)))
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