using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Assignment_2__MVC__CodeFirst.Startup))]
namespace Assignment_2__MVC__CodeFirst
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
