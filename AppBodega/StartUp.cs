using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AppBodega.App_Start.StartUp))]
namespace AppBodega.App_Start
{
    public partial class StartUp
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}