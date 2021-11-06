using Assignment_2__MVC__CodeFirst.Models;
using Assignment_2__MVC__CodeFirst.Static;
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
            builder.RegisterType<Repos>().As<IRepos>();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
        public static void RegisterContainerApi()
        {
            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;
            builder.RegisterApiControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<ApplicationDbContext>().InstancePerRequest();
            builder.RegisterType<Repos>().As<IRepos>();
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
