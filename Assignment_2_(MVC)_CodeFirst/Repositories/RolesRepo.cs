using Assignment_2__MVC__CodeFirst.Models;

namespace Assignment_2__MVC__CodeFirst.Repositories
{
    public class RolesRepo// : IRepository<Role>, IDisposable
    {
        private ApplicationDbContext _context;
        //private bool disposedValue;

        public RolesRepo(ApplicationDbContext context)
        {
            this._context = context;
        }

        //public void Add(Role obj)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Delete(Role obj)
        //{
        //    throw new NotImplementedException();
        //}

        //public Role Get(int? id)
        //{
        //    throw new NotImplementedException();
        //}

        //public IEnumerable<Role> GetAll()
        //{
        //    throw new NotImplementedException();
        //}

        //public IEnumerable<Role> GetAllEmpty()
        //{
        //    throw new NotImplementedException();
        //}

        //public Role GetEmpty(int? id)
        //{
        //    throw new NotImplementedException();
        //}

        //public bool Save()
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> SaveAsync()
        //{
        //    throw new NotImplementedException();
        //}

        //public void Update(Role obj)
        //{
        //    throw new NotImplementedException();
        //}
        //protected virtual void Dispose(bool disposing)
        //{
        //    if (!disposedValue)
        //    {
        //        if (disposing)
        //        {
        //            ((IDisposable)_context).Dispose();
        //        }
        //        disposedValue = true;
        //    }
        //}

        //public void Dispose()
        //{
        //    Dispose(disposing: true);
        //    GC.SuppressFinalize(this);
        //}
    }
}