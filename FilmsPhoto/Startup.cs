using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FilmsPhoto.Startup))]
namespace FilmsPhoto
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
