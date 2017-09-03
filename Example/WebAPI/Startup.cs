using System.Web.Http;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(WebAPI.Startup))]

namespace WebAPI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var httpConfig = new HttpConfiguration();

            // we only need json formatter now
            httpConfig.Formatters.Remove(httpConfig.Formatters.XmlFormatter);

            // use explicit routes
            httpConfig.MapHttpAttributeRoutes();

            // register webapi to owin
            app.UseWebApi(httpConfig);
        }
    }
}
