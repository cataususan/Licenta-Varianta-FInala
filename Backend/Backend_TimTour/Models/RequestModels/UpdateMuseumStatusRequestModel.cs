using Backend_TimTour.Models.LocationModels;
using System.ComponentModel.DataAnnotations;

namespace Backend_TimTour.Models.RequestModels
{
    public class UpdateMuseumStatusRequestModel
    {
        [Required]
        public string museumName { get; set; }
        [Required]
        public string museumStatus { get; set; }
    }
}
