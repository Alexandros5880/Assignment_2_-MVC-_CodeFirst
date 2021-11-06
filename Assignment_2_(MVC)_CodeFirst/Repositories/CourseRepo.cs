using Assignment_2__MVC__CodeFirst.Models;
using Assignment_2__MVC__CodeFirst.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

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
                .Include(c => c.Students)
                .Include(c => c.School)
                .FirstOrDefault(c => c.ID == id);
        }
        public Course GetEmpty(int? id)
        {
            return this._context.Courses
                .FirstOrDefault(c => c.ID == id);
        }

        public IEnumerable<Course> GetAll()
        {
            return this._context.Courses
                .Include(c => c.Students)
                .Include(c => c.School)
                .ToList();
        }

        public IEnumerable<Course> GetAllByName(string search)
        {
            return this._context.Courses
                .Where(c => c.Title.Equals(search) || c.Title.Contains(search))
                .Include(c => c.Students)
                .Include(c => c.School)
                .ToList();
        }

        public IEnumerable<Course> GetAllEmpty()
        {
            return this._context.Courses.ToList();
        }

        public IEnumerable<Course> GetAllBySchool(int schoolId)
        {
            return this._context.Courses
                .Include(c => c.Students)
                .Where(c => c.School.ID == schoolId)
                .Include(c => c.School)
                .ToList();
        }

        public IEnumerable<Course> GetAllByIds(ICollection<int?> ids)
        {
            return this._context.Courses
                .Where(c => ids.Contains(c.ID))
                .Include(c => c.Students)
                .Include(c => c.School)
                .ToList();
        }

        public IEnumerable<Course> GetAllByIdsEmpty(ICollection<int?> ids)
        {
            return this._context.Courses
                .Where(c => ids.Contains(c.ID))
                .ToList();
        }
        public bool Save()
        {
            return this._context.SaveChanges() > 0;
        }

        public async Task<int> SaveAsync()
        {
            return await this._context.SaveChangesAsync();
        }

        public void Update(Course obj)
        {
            this._context.Entry(obj).State = EntityState.Modified;
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