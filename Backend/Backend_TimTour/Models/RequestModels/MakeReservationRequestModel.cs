using System.ComponentModel.DataAnnotations;

namespace Backend_TimTour.Models.RequestModels
{
    public class MakeReservationRequestModel
    {
        [Required]
        public string LocationName { get; set; }
        [Required]
        public string LocationType { get; set; }
        [Required]
        public string ReservationDateAndTime { get; set; }
    }
}
