using System.ComponentModel.DataAnnotations;

namespace Backend_TimTour.Models.RequestModels
{
    public class UpdateReservationRequestModel
    {
        [Required]
        public string LocationName { get; set; }
        [Required]
        public string LocationType { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string CustomerEmail { get; set; }
        [Required]
        public string Status { get; set; }
    }
}
