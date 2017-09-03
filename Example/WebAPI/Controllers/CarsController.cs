﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Repository;

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

        [HttpGet]
        [Route("")]
        [ResponseType(typeof(IEnumerable<ViewModels.Car>))]
        public async Task<IHttpActionResult> GetCars()
        {
            var cars = await this._repository.GetAllCars().ConfigureAwait(false);

            var result = cars.Select(car => new ViewModels.Car
            {
                Id = car.Id,
                PlateNumber = car.PlateNumber,
                Color = (ViewModels.CarColor)car.Color
            });

            return Ok(result);
        }
    }
}