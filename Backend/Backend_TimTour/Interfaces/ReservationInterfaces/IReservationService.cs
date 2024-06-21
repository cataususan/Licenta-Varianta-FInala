using Backend_TimTour.Models.ResultsModels;

namespace Backend_TimTour.Interfaces.ReservationInterfaces
{
    public interface IReservationService
    {
        Task<ServiceResult> CheckIfLocationIsOpen(System.DayOfWeek dayName, int hour, int minutes, string locationType, string locationName);
    }
}
