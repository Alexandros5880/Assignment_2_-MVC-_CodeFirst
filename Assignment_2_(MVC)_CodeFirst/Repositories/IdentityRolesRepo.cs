using Assignment_2__MVC__CodeFirst.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment_2__MVC__CodeFirst.Repositories
{
    public class IdentityRolesRepo : IDisposable
    {
        private ApplicationDbContext _context;
        private bool disposedValue;

        public IdentityRolesRepo(ApplicationDbContext context)
        {
            this._context = context;
        }

        public void Add(IdentityRole obj)
        {
            this._context.Roles.Add(obj);
        }

        public void Delete(IdentityRole obj)
        {
            this._context.Roles.Remove(obj);
        }

        public IdentityRole Get(string id)
        {
            return this._context.Roles.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<IdentityRole> GetAll()
        {
            return this._context.Roles.ToList();
        }

        public bool Save()
        {
            return this._context.SaveChanges()>0? true:false;
        }

        public async Task<int> SaveAsync()
        {
            return await this._context.SaveChangesAsync();
        }

        public void Update(IdentityRole obj)
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