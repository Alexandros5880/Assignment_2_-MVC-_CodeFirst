using Assignment_2__MVC__CodeFirst.Models;
using Assignment_2__MVC__CodeFirst.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System;
using Assignment_2__MVC__CodeFirst.Static;

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
            return this._context.Schools.FirstOrDefault(s => s.ID == id);
        }

        public IEnumerable<School> GetAll()
        {
            return this._context.Schools;
        }

        public void Update(School obj)
        {
            this._context.Entry(obj).State = EntityState.Modified;
        }

        public ICollection<Course> GetCourses(int id)
        {
            return Globals.courseRepo.GetAll()
                .Where(c => c.School.ID == id)
                .ToList();
        }

        public ICollection<Assignment> GetAssignments(int id)
        {
            return Globals.assignmentRepo.GetAll()
                .Where(a => a.School.ID == id)
                .ToList();
        }

        public ICollection<Trainer> GetTrainers(int id)
        {
            return Globals.trainerRepo.GetAll()
                .Where(t => t.School.ID == id)
                .ToList();
        }

        public ICollection<Student> GetStudents(int id)
        {
            return Globals.studentRepo.GetAll()
                .Where(s => s.School.ID == id)
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