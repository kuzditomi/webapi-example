using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Models;

namespace Repository
{
    public class CarRepository
    {
        public CarRepository()
        {
            
        }

        public Task<IEnumerable<Common.Models.Car>> GetAllCars()
        {
            var cars = new List<Car>
            {
                new Car
                {
                    Id = 1,
                    PlateNumber = "ABC-123",
                    Color = CarColor.Black
                }
            };

            return Task.FromResult<IEnumerable<Car>>(cars);
        }
    }
}
