using Backend_TimTour.Models.LocationModels;
using Backend_TimTour.Models.ResultsModels;

namespace Backend_TimTour.Interfaces.LocationInterfaces
{
    public interface IReservationRepository
    {
        Task<(RepositoryResult, List<Reservation>)> FindAllReservationsByRestaurantNameAsync(string name);
        Task<(RepositoryResult, List<Reservation>)> FindAllReservationsByCustomerEmailAsync(string email);
        Task<RepositoryResult> SendReservation(Reservation reservation);
        Task<RepositoryResult> UpdateReservationAsync(string LocatioName, string customerEmail, string newStatus);

    }
}
