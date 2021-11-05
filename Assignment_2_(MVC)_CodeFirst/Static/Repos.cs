using Assignment_2__MVC__CodeFirst.Repositories;

namespace Assignment_2__MVC__CodeFirst.Static
{
    public static class Repos
    {
        public static RepositoryHundler DbHundler = new RepositoryHundler();
        public static SchoolRepo schoolRepo;
        public static CourseRepo courseRepo;
        public static AssignmentRepo assignmentRepo;
        public static TrainerRepo trainerRepo;
        public static StudentRepo studentRepo;
        public static RolesRepo rolesRepo;

        static Repos()
        {
            if (DbHundler == null)
                DbHundler = new RepositoryHundler();
            if (schoolRepo == null)
                schoolRepo = DbHundler.GetSchoolRepo();
            if (courseRepo == null)
                courseRepo = DbHundler.GetCourseRepo();
            if (assignmentRepo == null)
                assignmentRepo = DbHundler.GetAssignmentRepo();
            if (trainerRepo == null)
                trainerRepo = DbHundler.GetTrainerRepo();
            if (studentRepo == null)
                studentRepo = DbHundler.GetStudentRepo();
            if (rolesRepo == null)
                rolesRepo = DbHundler.GetRolesRepo();
        }
    }
}