using Backend_TimTour.Interfaces.LocationInterfaces;
using Backend_TimTour.Models;
using Backend_TimTour.Models.LocationModels;
using Backend_TimTour.Models.ResultsModels;
using DnsClient;
using MongoDB.Driver;
using System.Xml.Linq;

namespace Backend_TimTour.Repositories
{
    public class ReservationRepository:IReservationRepository
    {
        private readonly IMongoCollection<Reservation> _reservations;

        public ReservationRepository(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("Reservation");
            _reservations = database.GetCollection<Reservation>("Reservation");
        }

        public async Task<(RepositoryResult, List<Reservation>)> FindAllReservationsByRestaurantNameAsync(string name)
        {
            var filter = Builders<Reservation>.Filter.Eq(reservation => reservation.LocationName, name);

            var reservationsFound = await _reservations.Find(filter).ToListAsync();

            if (reservationsFound != null)
            {
                return (RepositoryResult.RESERVATIONS_FOUND, reservationsFound);
            }
            else
            {
                return (RepositoryResult.RESERVATIONS_NOT_FOUND, reservationsFound);
            }
        }

        public async Task<(RepositoryResult, List<Reservation>)> FindAllReservationsByCustomerEmailAsync(string email)
        {
            var filter = Builders<Reservation>.Filter.Eq(reservation => reservation.CustomerEmail, email);

            var reservationsFound = await _reservations.Find(filter).ToListAsync();

            if (reservationsFound != null)
            {
                return (RepositoryResult.RESERVATIONS_FOUND, reservationsFound);
            }
            else
            {
                return (RepositoryResult.RESERVATIONS_NOT_FOUND, reservationsFound);
            }
        }

        public async Task<RepositoryResult> SendReservation(Reservation reservation)
        {
            try
            {
                await _reservations.InsertOneAsync(reservation);
                return RepositoryResult.RESERVATION_CREATED;
            }
            catch (MongoWriteException ex)
            {
                Console.WriteLine($"Insert failed: {ex.Message}");
                return RepositoryResult.RESERVATION_CANT_BE_CREATED;
            }
        }
        public async Task<RepositoryResult> UpdateReservationAsync(string LocatioName, string customerEmail, string newStatus)
        {
            var filter = Builders<Reservation>.Filter.And(
                Builders<Reservation>.Filter.Eq(reservation => reservation.LocationName, LocatioName),
                Builders<Reservation>.Filter.Eq(reservation => reservation.CustomerEmail, customerEmail)
            );

            var update = Builders<Reservation>.Update
                .Set("status", newStatus);
            try
            {
                await _reservations.UpdateOneAsync(filter, update);
                return RepositoryResult.RESERVATION_SUCCESFULLY_UPDATED;
            }
            catch (MongoWriteException ex)
            {
                Console.WriteLine($"Update failed: {ex.Message}");
                return RepositoryResult.RESERVATION_CAN_NOT_BE_UPDATED;
            }
        }
        

    }
}
