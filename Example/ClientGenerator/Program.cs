using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using NSwag;
using NSwag.CodeGeneration.CSharp;

namespace ClientGenerator
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var document = await SwaggerDocument.FromUrlAsync("http://localhost:49530/swagger/docs/v1");

            var settings = new SwaggerToCSharpClientGeneratorSettings
            {
                ClassName = "CarClient",
                CSharpGeneratorSettings = { Namespace = "Example" }
            };

            var generator = new SwaggerToCSharpClientGenerator(document, settings);

            var code = generator.GenerateFile();
            var clientFilePath = Path.Combine(Assembly.GetExecutingAssembly().Location, "../../../CarClient.cs");
                
            if (!File.Exists(clientFilePath))
            {
                File.Create(clientFilePath);
            }

            using (var tw = new StreamWriter(clientFilePath, false))
            {
                await tw.WriteAsync(code).ConfigureAwait(false);
            }
        }
    }
}
