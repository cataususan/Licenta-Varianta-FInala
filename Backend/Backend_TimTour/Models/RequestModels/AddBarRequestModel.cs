using Backend_TimTour.Models.LocationModels;
using Backend_TimTour.Models.PrefferenceModels;
using System.ComponentModel.DataAnnotations;

namespace Backend_TimTour.Models.RequestModels
{
    public class AddBarRequestModel
    {
        [Required]
        public RequestBar bar { get; set; }
    }
}
