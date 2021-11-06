using Assignment_2__MVC__CodeFirst.Models;
using Assignment_2__MVC__CodeFirst.Repositories;
using System;

namespace Assignment_2__MVC__CodeFirst.Static
{
    public class Repos : IRepos, IDisposable
    {
        private ApplicationDbContext _context;
        private bool disposedValue;
        public readonly SchoolRepo Schools;
        public readonly CourseRepo Courses;
        public readonly AssignmentRepo Assignments;
        public readonly TrainerRepo Trainers;
        public readonly StudentRepo Students;
        public readonly IdentityRolesRepo Roles;
        public readonly IdentityUsersRepo Users;

        public Repos(ApplicationDbContext context)
        {
            if (this._context == null)
                this._context = context;
            if (Schools == null)
                Schools = new SchoolRepo(this._context);
            if (Courses == null)
                Courses = new CourseRepo(this._context);
            if (Assignments == null)
                Assignments = new AssignmentRepo(this._context);
            if (Trainers == null)
                Trainers = new TrainerRepo(this._context);
            if (Students == null)
                Students = new StudentRepo(this._context);
            if (Roles == null)
                Roles = new IdentityRolesRepo(this._context);
            if (Users == null)
                Users = new IdentityUsersRepo(this._context);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this._context.Dispose();
                    this.Schools.Dispose();
                    this.Courses.Dispose();
                    this.Assignments.Dispose();
                    this.Trainers.Dispose();
                    this.Roles.Dispose();
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