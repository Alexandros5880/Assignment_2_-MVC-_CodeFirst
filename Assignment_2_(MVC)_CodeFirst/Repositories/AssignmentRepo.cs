﻿using Assignment_2__MVC__CodeFirst.Models;
using Assignment_2__MVC__CodeFirst.Models.Entities;
using Assignment_2__MVC__CodeFirst.Static;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

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
            this._context.Assignents.Add(obj);
        }

        public void Delete(Assignment obj)
        {
            Assignment assignment = this._context.Assignents
                                    .Find(obj.ID);
            this._context.Assignents.Remove(assignment);
        }

        public Assignment Get(int? id)
        {
            return this._context.Assignents
                .Include(a => a.Students)
                .FirstOrDefault(a => a.ID == id);
        }

        public IEnumerable<Assignment> GetAll()
        {
            return this._context.Assignents
                .Include(a => a.Students);
        }

        public void Update(Assignment obj)
        {
            this._context.Entry(obj).State = EntityState.Modified;
        }
        
        //public Assignment GetWithRelated(int? id)
        //{
        //    return this._context.Assignents
        //        .Include(a => a.Students)
        //        .FirstOrDefault(a => a.ID == id);
        //}

        //public ICollection<Assignment> GetAllWithRelated()
        //{
        //    return this._context.Assignents
        //        .Include(a => a.Students)
        //        .ToList();
        //}
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