using Assignment_2__MVC__CodeFirst.Models;
using System;

namespace Assignment_2__MVC__CodeFirst.Repositories
{
    public class RepositoryHundler : IRepositoryHundler, IDisposable
    {
        private ApplicationDbContext _context;
        private bool disposedValue;

        public RepositoryHundler()
        {
            this._context = new ApplicationDbContext();
    }

        public SchoolRepo GetSchoolRepo()
        {
            return new SchoolRepo(this._context);
        }

        public CourseRepo GetCourseRepo()
        {
            return new CourseRepo(this._context);
        }

        public AssignmentRepo GetAssignmentRepo()
        {
            return new AssignmentRepo(this._context);
        }

        public TrainerRepo GetTrainerRepo()
        {
            return new TrainerRepo(this._context);
        }

        public StudentRepo GetStudentRepo()
        {
            return new StudentRepo(this._context);
        }

        public bool Save()
        {
            return this._context.SaveChanges() > 0;
        }

        public async void SaveAsync()
        {
            await this._context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {

                }
                disposedValue = true;
            }
        }
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}