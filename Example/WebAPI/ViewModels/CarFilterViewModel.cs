namespace WebAPI.ViewModels
{
    /// <summary>
    /// Filter type for car filering
    /// </summary>
    public class CarFilterViewModel
    {
        /// <summary>
        /// String to be included in car's plate number
        /// </summary>
        public string PlateNumber { get; set; }
        
        /// <summary>
        /// Only color to filter to
        /// </summary>
        public CarColor? Color { get; set; }
    }
}