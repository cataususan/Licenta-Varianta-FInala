using System.ComponentModel.DataAnnotations;

namespace Backend_TimTour.Models.RequestModels
{
    public class RateModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Rating { get; set; }
        [Required]
        public string Type { get; set; }
    }
}
