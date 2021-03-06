using Assignment_2__MVC__CodeFirst.Models;
using Assignment_2__MVC__CodeFirst.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

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
            return this._context.Schools
                .Include(s => s.Courses)
                .Include(s => s.Assignments)
                .Include(s => s.Trainers)
                .Include(s => s.Students)
                .FirstOrDefault(s => s.ID == id);
        }

        public School GetEmpty(int? id)
        {
            return this._context.Schools
                .FirstOrDefault(s => s.ID == id);
        }

        public IEnumerable<School> GetAll()
        {
            return this._context.Schools
                .Include(s => s.Courses)
                .Include(s => s.Assignments)
                .Include(s => s.Trainers)
                .Include(s => s.Students)
                .ToList();
        }

        public IEnumerable<School> GetAllByName(string name)
        {
            return this._context.Schools
                .Where(s => s.Name.Equals(name) || s.Name.Contains(name))
                .Include(s => s.Courses)
                .Include(s => s.Assignments)
                .Include(s => s.Trainers)
                .Include(s => s.Students)
                .ToList();
        }

        public IEnumerable<School> GetAllEmpty()
        {
            return this._context.Schools.ToList();
        }

        public void Update(School obj)
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