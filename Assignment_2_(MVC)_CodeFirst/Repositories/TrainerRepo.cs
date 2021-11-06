using Assignment_2__MVC__CodeFirst.Models;
using Assignment_2__MVC__CodeFirst.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment_2__MVC__CodeFirst.Repositories
{
    public class TrainerRepo : IRepository<Trainer>, IDisposable
    {
        private ApplicationDbContext _context;
        private bool disposedValue;

        public TrainerRepo(ApplicationDbContext context)
        {
            this._context = context;
        }

        public void Add(Trainer obj)
        {
            this._context.Trainers.Add(obj);
        }

        public void Delete(Trainer obj)
        {
            Trainer trainer = this._context.Trainers
                                  .Find(obj.ID);
            this._context.Trainers.Remove(trainer);
        }

        public Trainer Get(int? id)
        {
            return this._context.Trainers
                .Include(t => t.Courses)
                .FirstOrDefault(t => t.ID == id);
        }

        public Trainer GetEmpty(int? id)
        {
            return this._context.Trainers
                .FirstOrDefault(t => t.ID == id);
        }

        public IEnumerable<Trainer> GetAll()
        {
            return this._context.Trainers
                .Include(t => t.Courses)
                .ToList();
        }

        public IEnumerable<Trainer> GetAllByName(string search)
        {
            return this._context.Trainers
                .Where(t => t.FirstName.Equals(search) || t.FirstName.Contains(search) ||
                        t.LastName.Equals(search) || t.LastName.Contains(search))
                .Include(t => t.Courses)
                .ToList();
        }

        public IEnumerable<Trainer> GetAllEmpty()
        {
            return this._context.Trainers
                .ToList();
        }

        public IEnumerable<Trainer> GetAllBySchool(int schoolId)
        {
            return this._context.Trainers
                .Include(t => t.Courses)
                .Where(t => t.School.ID == schoolId)
                .ToList();
        }

        public IEnumerable<Trainer> GetAllByIds(ICollection<int?> ids)
        {
            return this._context.Trainers
                .Where(t => ids.Contains(t.ID))
                .Include(t => t.Courses)
                .ToList();
        }

        public IEnumerable<Trainer> GetAllByIdsEmpty(ICollection<int?> ids)
        {
            return this._context.Trainers
                .Where(t => ids.Contains(t.ID))
                .ToList();
        }

        public void Update(Trainer obj)
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