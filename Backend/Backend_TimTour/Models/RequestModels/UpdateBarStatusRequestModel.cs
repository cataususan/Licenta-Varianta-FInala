using Backend_TimTour.Models.LocationModels;
using System.ComponentModel.DataAnnotations;

namespace Backend_TimTour.Models.RequestModels
{
    public class UpdateBarStatusRequestModel
    {
        [Required]
        public string barName { get; set; }
        [Required]
        public string barStatus { get; set; }
    }
}
