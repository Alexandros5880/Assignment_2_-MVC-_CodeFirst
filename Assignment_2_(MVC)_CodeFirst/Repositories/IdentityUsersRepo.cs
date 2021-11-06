using Assignment_2__MVC__CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment_2__MVC__CodeFirst.Repositories
{
    public class IdentityUsersRepo : IRepositoryIdentity<ApplicationUser>
    {
        private ApplicationDbContext _context;
        private bool disposedValue;

        public IdentityUsersRepo(ApplicationDbContext context)
        {
            this._context = context;
        }
        public void Add(ApplicationUser obj)
        {
            this._context.Users.Add(obj);
        }

        public void Delete(ApplicationUser obj)
        {
            this._context.Users.Remove(obj);
        }

        public ApplicationUser Get(string id)
        {
            return this._context.Users.FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            return this._context.Users.ToList();
        }

        public bool Save()
        {
            return this._context.SaveChanges() > 0 ? true : false;
        }

        public async Task<int> SaveAsync()
        {
            return await this._context.SaveChangesAsync();
        }

        public void Update(ApplicationUser obj)
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