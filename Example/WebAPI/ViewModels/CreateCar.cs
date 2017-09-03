using System.ComponentModel.DataAnnotations;

namespace WebAPI.ViewModels
{
    /// <summary>
    /// Car
    /// </summary>
    public class CreateCar
    {
        /// <summary>
        /// Plate number of car, format: ABC-123
        /// </summary>
        [Required]
        [RegularExpression("^[A-Z]{3}-[0-9]{3}")]
        public string PlateNumber { get; set; }

        /// <summary>
        /// Registered color of the car
        /// </summary>
        [Required]
        public CarColor Color { get; set; }
    }
}