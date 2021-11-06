using Assignment_2__MVC__CodeFirst.Models;
using Assignment_2__MVC__CodeFirst.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment_2__MVC__CodeFirst.Repositories
{
    public class AssignmentRepo : IRepository<Assignment>, IDisposable
    {
        private ApplicationDbContext _context;
        private bool disposedValue;

        public AssignmentRepo(ApplicationDbContext context)
        {
            this._context = context;
        }

        public void Add(Assignment obj)
        {
            this._context.Assignments.Add(obj);
        }

        public void Delete(Assignment obj)
        {
            Assignment assignment = this._context.Assignments
                                    .Find(obj.ID);
            this._context.Assignments.Remove(assignment);
        }

        public Assignment Get(int? id)
        {
            return this._context.Assignments
                .Include(a => a.Students)
                .FirstOrDefault(a => a.ID == id);
        }

        public Assignment GetEmpty(int? id)
        {
            return this._context.Assignments
                .FirstOrDefault(a => a.ID == id);
        }

        public IEnumerable<Assignment> GetAll()
        {
            return this._context.Assignments
                .Include(a => a.Students)
                .ToList();
        }

        public IEnumerable<Assignment> GetAllByName(string search)
        {
            return this._context.Assignments
                .Where(s => s.Title.Equals(search) || s.Title.Contains(search))
                .Include(s => s.Students)
                .ToList();
        }

        public IEnumerable<Assignment> GetAllEmpty()
        {
            return this._context.Assignments.ToList();
        }

        public IEnumerable<Assignment> GetAllBySchool(int schoolId)
        {
            return this._context.Assignments
                .Include(s => s.Students)
                .Where(s => s.School.ID == schoolId)
                .ToList();
        }

        public IEnumerable<Assignment> GetAllByIds(ICollection<int?> ids)
        {
            return this._context.Assignments
                .Where(a => ids.Contains(a.ID))
                .Include(a => a.Students)
                .ToList();
        }

        public IEnumerable<Assignment> GetAllByIdsEmpty(ICollection<int?> ids)
        {
            return this._context.Assignments
                .Where(a => ids.Contains(a.ID))
                .ToList();
        }

        public void Update(Assignment obj)
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