using Assignment_2__MVC__CodeFirst.Models;
using Assignment_2__MVC__CodeFirst.Repositories;

namespace Assignment_2__MVC__CodeFirst.Static
{
    public class Repos : IRepos
    {
        private ApplicationDbContext _context;
        public readonly SchoolRepo Schools;
        public readonly CourseRepo Courses;
        public readonly AssignmentRepo Assignments;
        public readonly TrainerRepo Trainers;
        public readonly StudentRepo Students;
        public readonly RolesRepo Roles;

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
                Roles = new RolesRepo(this._context);
        }
    }
}