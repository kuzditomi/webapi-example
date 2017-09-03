using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Repository;
using WebAPI.ViewModels;

namespace WebAPI.Controllers
{
    [RoutePrefix("api/cars")]
    public class CarsController : ApiController
    {
        private CarRepository _repository;

        public CarsController()
        {
            this._repository = new CarRepository();
        }

        /// <summary>
        /// Get all cars
        /// </summary>
        /// <remarks>
        /// We can add some implementation notes here
        /// </remarks>
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(List<ViewModels.Car>))]
        public async Task<IHttpActionResult> GetCars()
        {
            var cars = await this._repository.GetAllCars().ConfigureAwait(false);

            var result = cars.Select(car => new Car
            {
                Id = car.Id,
                PlateNumber = car.PlateNumber,
                Color = (CarColor)car.Color
            });

            return Ok(result);
        }

        /// <summary>
        /// Gets car by it's id in the system
        /// </summary>
        /// <param name="id">Id of car in system</param>
        /// <response code="200">Returns the car</response>
        /// <response code="404">If car with given Id is not found</response>
        [HttpGet]
        [Route("{id}")]
        [ResponseType(typeof(Car)), ResponseType(typeof(string))]
        public async Task<IHttpActionResult> GetCarById(int id)
        {
            var cars = await this._repository.GetAllCars().ConfigureAwait(false);

            var car = cars.SingleOrDefault(c => c.Id == id);

            if (car == null)
            {
                return NotFound();
            }

            return Ok(new Car
            {
                Id = car.Id,
                PlateNumber = car.PlateNumber,
                Color = (CarColor) car.Color
            });
        }
    }
}