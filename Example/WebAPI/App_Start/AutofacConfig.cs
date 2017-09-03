using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Common.Repository;
using Repository;

namespace WebAPI
{
    public class AutofacConfig
    {
        public static void Register(HttpConfiguration httpConfig)
        {
            var container = RegisterServices(new ContainerBuilder());
            httpConfig.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            // Register your Web API controllers
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<CarRepository>()
                .As<ICarRepository>()
                .SingleInstance();

            // Set the dependency resolver to be Autofac
            var container = builder.Build();

            return container;
        }
    }
}