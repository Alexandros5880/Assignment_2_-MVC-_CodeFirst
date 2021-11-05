using Assignment_2__MVC__CodeFirst.Models;
using Assignment_2__MVC__CodeFirst.Models.Entities;
using Assignment_2__MVC__CodeFirst.Repositories;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using System.Web.Http;
using System.Web.Mvc;

namespace Assignment_2__MVC__CodeFirst.App_Start
{
    public class ContainerConfig
    {
        public static void RegisterContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<ApplicationDbContext>().InstancePerRequest();
            builder.RegisterType<SchoolRepo>().As<IRepository<School>>();
            builder.RegisterType<CourseRepo>().As<IRepository<Course>>();
            builder.RegisterType<AssignmentRepo>().As<IRepository<Assignment>>();
            builder.RegisterType<TrainerRepo>().As<IRepository<Trainer>>();
            builder.RegisterType<StudentRepo>().As<IRepository<Student>>();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
        public static void RegisterContainerApi()
        {
            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;
            builder.RegisterApiControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<ApplicationDbContext>().InstancePerRequest();
            builder.RegisterType<SchoolRepo>().As<IRepository<School>>();
            builder.RegisterType<CourseRepo>().As<IRepository<Course>>();
            builder.RegisterType<AssignmentRepo>().As<IRepository<Assignment>>();
            builder.RegisterType<TrainerRepo>().As<IRepository<Trainer>>();
            builder.RegisterType<StudentRepo>().As<IRepository<Student>>();

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
