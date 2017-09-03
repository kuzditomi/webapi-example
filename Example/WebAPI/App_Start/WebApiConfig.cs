using System.Web.Http;

namespace WebAPI
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration httpConfig)
        {
            // we only need json formatter now
            httpConfig.Formatters.Remove(httpConfig.Formatters.XmlFormatter);

            // use explicit routes
            httpConfig.MapHttpAttributeRoutes();
        }
    }
}