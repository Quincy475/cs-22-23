using System.Collections.Generic;
using CS_Eind.Models;
using CS_Eind.Models.DTO;
using CS_Eind.Repositories;

namespace CS_Eind.Repositories
{
    public interface IAirBnBRepository
    {
        Task<IEnumerable<Location>> GetAllLocationsAsync(CancellationToken cancellationToken);
        Task<string> GetImageForLocationAsync(int locationId, CancellationToken cancellationToken);
        Task<string> GetAvatarForLandlordAsync(int locationId, CancellationToken cancellationToken);
        Task<Location> GetLocationById(int id, CancellationToken cancellationToken);
        Task<IEnumerable<ImageDTO>> GetImagesForLocation(int locationId, CancellationToken cancellationToken);
        Task<string> GetNameForLandlord(int locationId, CancellationToken cancellationToken);
        Task<IEnumerable<Location>> SearchLocations(SearchDTO searchRequest, CancellationToken cancellationToken);
        Task<float> GetPriceForLocation(int locationId, CancellationToken cancellationToken);
        Task<Customer> GetCustomerByEmailAsync(string email, CancellationToken cancellationToken);
        Task<Customer> CreateCustomerFromRequestAsync(ResRequestDTO request, CancellationToken cancellationToken);
        Task<UnavailableDatesDTO> GetUnavailableDates(int locationId, CancellationToken cancellationToken);
        Task<Reservation> CreateReservation(ResRequestDTO request, CancellationToken cancellationToken);
    }
}