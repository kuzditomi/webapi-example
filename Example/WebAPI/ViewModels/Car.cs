namespace WebAPI.ViewModels
{
    /// <summary>
    /// Car
    /// </summary>
    public class Car
    {
        /// <summary>
        /// Numeric id of the car
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Plate number of car, format: ABC-123
        /// </summary>
        public string PlateNumber { get; set; }

        /// <summary>
        /// Registered color of the car
        /// </summary>
        public CarColor Color { get; set; }
    }
}