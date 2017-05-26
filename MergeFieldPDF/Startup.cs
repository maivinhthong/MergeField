using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MergeFieldPDF.Startup))]
namespace MergeFieldPDF
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
