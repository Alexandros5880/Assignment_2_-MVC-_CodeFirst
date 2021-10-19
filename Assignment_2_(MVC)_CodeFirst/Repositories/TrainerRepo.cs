using Assignment_2__MVC__CodeFirst.Models;
using Assignment_2__MVC__CodeFirst.Models.Entities;
using Assignment_2__MVC__CodeFirst.Static;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

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
                .FirstOrDefault(t => t.ID == id);
        }

        public IEnumerable<Trainer> GetAll()
        {
            return this._context.Trainers;
        }

        public void Update(Trainer obj)
        {
            this._context.Entry(obj).State = EntityState.Modified;
        }

        public Trainer GetWithRelated(int? id)
        {
            return this._context.Trainers
                .Include(t => t.Courses)
                .FirstOrDefault(t => t.ID == id);
        }

        public ICollection<Trainer> GetAllWithRelated()
        {
            return this._context.Trainers
                .Include(t => t.Courses)
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