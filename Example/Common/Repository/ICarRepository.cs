using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Models;

namespace Common.Repository
{
    public interface ICarRepository
    {
        Task<IEnumerable<Car>> GetAllCars();
        Task<Car> GetCarById(int id);
        Task<Car> CreateCar(Car car);
    }
}
