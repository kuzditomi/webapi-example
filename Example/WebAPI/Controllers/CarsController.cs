using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.ModelBinding;
using Common.Repository;
using Repository;
using WebAPI.ViewModels;

namespace WebAPI.Controllers
{
    [RoutePrefix("api/cars")]
    public class CarsController : ApiController
    {
        private ICarRepository _repository;

        public CarsController(ICarRepository repository)
        {
            this._repository = repository;
        }

        /// <summary>
        /// Get all cars
        /// </summary>
        /// <remarks>
        /// We can add some implementation notes here
        /// </remarks>
        /// <returns>All cars in the system</returns>
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
        /// Get car by it's id in the system
        /// </summary>
        /// <returns>The car with specified Id</returns>
        /// <param name="id">Id of car in system</param>
        /// <response code="200">Returns the car</response>
        /// <response code="404">If car with given Id is not found</response>
        [HttpGet]
        [Route("{id}")]
        [ResponseType(typeof(Car)), ResponseType(typeof(string))]
        public async Task<IHttpActionResult> GetCarById(int id)
        {
            var car = await this._repository.GetCarById(id).ConfigureAwait(false);

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

        /// <summary>
        /// Add car to the system
        /// </summary>
        /// <param name="car">Car to create</param>
        /// <returns>The created car</returns>
        /// <response code="201">Returns the created car</response>
        /// <response code="400">If the request model is invalid</response>
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(Car)), ResponseType(typeof(ModelStateDictionary))]
        public async Task<IHttpActionResult> AddCar(CreateCar car)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var carToAdd = new Common.Models.Car
            {
                Color = (Common.Models.CarColor) car.Color,
                PlateNumber = car.PlateNumber
            };

            var createdCar = await _repository.CreateCar(carToAdd);
            var result = new Car
            {
                Id = createdCar.Id,
                PlateNumber = createdCar.PlateNumber,
                Color = (CarColor) createdCar.Color
            };

            return Created<Car>(new Uri(Request.RequestUri.ToString() + createdCar.Id), result);
        }
    }
}