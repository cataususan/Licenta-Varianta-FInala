using System.ComponentModel.DataAnnotations;

namespace Backend_TimTour.Models.RequestModels
{
    public class FindReservationsByUserEmail
    {
        [Required]
        public string Email { get; set; }
    }
}
