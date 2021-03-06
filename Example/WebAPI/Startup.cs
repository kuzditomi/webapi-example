﻿using System.Web.Http;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(WebAPI.Startup))]

namespace WebAPI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Serve Swagger UI
            app.UseStaticFiles();

            var httpConfig = new HttpConfiguration();

            // setup webapi
            WebApiConfig.Register(httpConfig);

            // setup swagger
            SwaggerConfig.Register(httpConfig);

            // setup autofac
            AutofacConfig.Register(httpConfig);

            // register to owin
            app.UseWebApi(httpConfig);
        }
    }
}
