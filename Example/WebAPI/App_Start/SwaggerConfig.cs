using System;
using System.Web.Http;
using WebActivatorEx;
using WebAPI;
using Swashbuckle.Application;

namespace WebAPI
{
    public class SwaggerConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "WebAPI");
                    c.IncludeXmlComments(string.Format(@"{0}\bin\WebAPI.XML",
                        System.AppDomain.CurrentDomain.BaseDirectory));

                    c.PrettyPrint();
                    c.DescribeAllEnumsAsStrings();
                })
                .EnableSwaggerUi("swagger/docs/v1");
        }
    }
}
