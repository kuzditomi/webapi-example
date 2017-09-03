using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Models;
using Common.Repository;

namespace Repository
{
    public class CarRepository : ICarRepository
    {
        private int _idCounter = 1;
        private List<Car> _cars { get; }

        public CarRepository()
        {
            this._cars = new List<Car>();

            AddCar(new Car
            {
                PlateNumber = "ABC-123",
                Color = CarColor.Red
            });

            AddCar(new Car
            {
                PlateNumber = "CDE-321",
                Color = CarColor.Green
            });
        }

        private Car AddCar(Car car)
        {
            var carToCreate = new Car
            {
                Id = _idCounter++,
                PlateNumber = car.PlateNumber,
                Color = car.Color
            };

            _cars.Add(carToCreate);

            return carToCreate;
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

        public Task<Car> CreateCar(Car car)
        {
            var created = AddCar(car);

            return Task.FromResult(created);
        }
    }
}
