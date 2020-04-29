using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MemberCardManagementV1.Startup))]
namespace MemberCardManagementV1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
