﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Common.Repository;
using Swashbuckle.Swagger.Annotations;
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
        /// <param name="filter">filter containing options to filter cars</param>
        /// <returns>All cars in the system</returns>
        /// <response code="200"></response>
        [HttpGet]
        [Route("")]
        [SwaggerResponse(200, Type = typeof(List<ViewModels.Car>))]
        public async Task<IHttpActionResult> GetCars([FromUri]CarFilterViewModel filter)
        {
            var cars = await this._repository.GetAllCars().ConfigureAwait(false);

            var result = cars.Select(car => new Car
            {
                Id = car.Id,
                PlateNumber = car.PlateNumber,
                Color = (CarColor)car.Color
            });

            if (filter == null) return Ok(result);

            if (!string.IsNullOrEmpty(filter.PlateNumber))
            {
                result = result.Where(c => c.PlateNumber.Contains(filter.PlateNumber));
            }

            if (filter.Color != null)
            {
                result = result.Where(c => c.Color == filter.Color);
            }

            return Ok(result);
        }

        /// <summary>
        /// Get car by it's id in the system
        /// </summary>
        /// <returns>The car with specified Id</returns>
        /// <param name="id">Id of car in system</param>
        /// <response code="200"></response>
        /// <response code="404">If car with given Id is not found</response>
        [HttpGet]
        [Route("{id}")]
        [SwaggerResponse(200, Type = typeof(Car))]
        [SwaggerResponse(404)]
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
                Color = (CarColor)car.Color
            });
        }

        /// <summary>
        /// Add car to the system
        /// </summary>
        /// <param name="car">Car to create</param>
        /// <returns>The created car</returns>
        /// <response code="201"></response>
        /// <response code="400"></response>
        [HttpPost]
        [Route("")]
        [SwaggerResponse(201, "Returns the created car", typeof(Car))]
        [SwaggerResponse(400, "If the request model is invalid", Type = typeof(ModelStateDictionary))]
        public async Task<IHttpActionResult> AddCar(CreateCar car)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var carToAdd = new Common.Models.Car
            {
                Color = (Common.Models.CarColor)car.Color,
                PlateNumber = car.PlateNumber
            };

            var createdCar = await _repository.CreateCar(carToAdd);
            var result = new Car
            {
                Id = createdCar.Id,
                PlateNumber = createdCar.PlateNumber,
                Color = (CarColor)createdCar.Color
            };

            return Created<Car>(new Uri(Request.RequestUri.ToString() + createdCar.Id), result);
        }
    }
}