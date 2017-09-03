using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Models;
using Common.Repository;

namespace Repository
{
    public class CarRepository : ICarRepository
    {
        private List<Car> _cars { get;}

        public CarRepository()
        {
            this._cars = new List<Car>
            {
                new Car
                {
                    Id = 1,
                    PlateNumber = "ABC-123",
                    Color = CarColor.Red
                },
                new Car
                {
                    Id = 2,
                    PlateNumber = "CDE-321",
                    Color = CarColor.Green
                },
            };
        }

        public Task<IEnumerable<Car>> GetAllCars()
        {
            var cars = _cars.ToArray();

            return Task.FromResult<IEnumerable<Car>>(cars);
        }

        public Task<Car> GetCarById(int id)
        {
            var car = _cars.SingleOrDefault(c => c.Id == id);

            return Task.FromResult<Car>(car);
        }
    }
}
