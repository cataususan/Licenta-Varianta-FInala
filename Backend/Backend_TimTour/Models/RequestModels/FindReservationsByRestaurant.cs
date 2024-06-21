using System.ComponentModel.DataAnnotations;

namespace Backend_TimTour.Models.RequestModels
{
    public class FindReservationsByRestaurant
    {
        [Required]
        public string Name { get; set; }
    }
}
