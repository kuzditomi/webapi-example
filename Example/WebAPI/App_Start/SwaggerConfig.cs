using System.Web.Http;
using Swashbuckle.Application;
using Swashbuckle.Swagger;
using System.Web.Http.Description;

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
                    c.DescribeAllEnumsAsStrings();

                    c.OperationFilter<ComplexRequestObjectOperationFilter>();
                })
                .EnableSwaggerUi("swagger/docs/v1");
        }
    }

    /// <summary>
    /// Add operation filter for complex get types based on https://github.com/domaindrivendev/Swashbuckle/issues/70
    /// </summary>
    public class ComplexRequestObjectOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            var actionDescriptor = apiDescription.ActionDescriptor;
            var method = apiDescription.HttpMethod.Method.ToLower();
            var controllerName = actionDescriptor.ControllerDescriptor.ControllerName;
            var parameterBindings = actionDescriptor.ActionBinding.ParameterBindings;
            
            // we are only interested in modifying GET requests
            if (method == "get")
            {
                if (parameterBindings.Length > 0)
                {
                    var parameterDescriptor = actionDescriptor.ActionBinding.ParameterBindings[0].Descriptor;
                    var parameterName = parameterDescriptor.ParameterName;
                    // correct the name of the property to _not_ prepend it with the controller method's argument name
                    if (parameterDescriptor.ParameterType.BaseType == typeof(object))
                    {
                        foreach (var property in operation.parameters)
                            property.name = property.name.Replace(parameterName + ".", "");
                    }
                }
            }
        }
    }
}
