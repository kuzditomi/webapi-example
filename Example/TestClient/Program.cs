using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Example;

namespace TestClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new Example.CarClient();

            var cars = await client.GetCarsAsync().ConfigureAwait(false);

            foreach (var car in cars)
            {
                Console.WriteLine(car.Id + "-" + car.PlateNumber);
            }

            try
            {
                await client.AddCarAsync(new CreateCar
                {
                    PlateNumber = "ABB-123",
                    Color = CreateCarColor.Red
                }).ConfigureAwait(false);
            }
            catch (SwaggerException e)
            {
            }

            Console.ReadKey();
        }
    }
}
