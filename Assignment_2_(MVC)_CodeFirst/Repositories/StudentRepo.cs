using Assignment_2__MVC__CodeFirst.Models;
using Assignment_2__MVC__CodeFirst.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

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
                .Include(s => s.Courses)
                .Include(s => s.Assignments)
                .Include(c => c.School)
                .FirstOrDefault(s => s.ID == id);
        }

        public Student GetEmpty(int? id)
        {
            return this._context.Students
                .FirstOrDefault(s => s.ID == id);
        }

        public IEnumerable<Student> GetAll()
        {
            return this._context.Students
                .Include(s => s.Courses)
                .Include(s => s.Assignments)
                .Include(c => c.School)
                .ToList();
        }

        public IEnumerable<Student> GetAllByName(string search)
        {
            return this._context.Students
                .Where(s => s.FirstName.Equals(search) || s.FirstName.Contains(search) ||
                        s.LastName.Equals(search) || s.LastName.Contains(search))
                .Include(s => s.Courses)
                .Include(s => s.Assignments)
                .Include(c => c.School)
                .ToList();
        }

        public IEnumerable<Student> GetAllEmpty()
        {
            return this._context.Students
                .ToList();
        }

        public IEnumerable<Student> GetAllBySchool(int schoolId)
        {
            return this._context.Students
                .Include(s => s.Courses)
                .Include(s => s.Assignments)
                .Where(s => s.School.ID == schoolId)
                .Include(c => c.School)
                .ToList();
        }

        public IEnumerable<Student> GetAllByIds(ICollection<int?> ids)
        {
            return this._context.Students
                .Where(s => ids.Contains(s.ID))
                .Include(s => s.Courses)
                .Include(s => s.Assignments)
                .Include(c => c.School)
                .ToList();
        }

        public IEnumerable<Student> GetAllByIdsEmpty(ICollection<int?> ids)
        {
            return this._context.Students
                .Where(s => ids.Contains(s.ID))
                .ToList();
        }

        public void Update(Student obj)
        {
            this._context.Entry(obj).State = EntityState.Modified;
        }
        public bool Save()
        {
            return this._context.SaveChanges() > 0;
        }

        public async Task<int> SaveAsync()
        {
            return await this._context.SaveChangesAsync();
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