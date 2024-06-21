using System.ComponentModel.DataAnnotations;

namespace Backend_TimTour.Models.RequestModels
{
    public class ExchangeRequest
    {
        [Required]
        public string CurrentCurrency { get; set; }
        [Required]
        public string WantedCurrency { get; set; }
        [Required]
        public float Amount { get; set; }

    }
}
