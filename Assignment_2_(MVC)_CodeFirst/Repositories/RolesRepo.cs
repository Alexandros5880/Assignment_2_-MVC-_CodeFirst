using Assignment_2__MVC__CodeFirst.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assignment_2__MVC__CodeFirst.Repositories
{
    public class RolesRepo : IDisposable
    {
        private ApplicationDbContext _context;
        private bool disposedValue;

        public RolesRepo(ApplicationDbContext context)
        {
            this._context = context;
        }

        public IdentityRole Get(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new Exception();
            return this._context.Roles.FirstOrDefault(r => r.Id.Equals(id));
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            throw new NotImplementedException();
        }

        public ApplicationUser GetEmpty(int? id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ApplicationUser> GetAllEmpty()
        {
            throw new NotImplementedException();
        }

        public void Add(ApplicationUser obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(ApplicationUser obj)
        {
            throw new NotImplementedException();
        }

        public void Update(ApplicationUser obj)
        {
            throw new NotImplementedException();
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