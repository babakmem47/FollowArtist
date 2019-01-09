using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FollowArtist.Startup))]
namespace FollowArtist
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
