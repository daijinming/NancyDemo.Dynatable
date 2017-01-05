using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NancyDemo.Dynatable.Startup))]
namespace NancyDemo.Dynatable
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
                        
            app.UseNancy();
        }
    }
}
