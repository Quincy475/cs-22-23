using CS_Eind.Models;
using CS_Eind.Models.DTO;

namespace CS_Eind.Services;

public interface ISearchService
{
    Task<IEnumerable<LocationDTO>> GetAllLocationsAsync(CancellationToken cancellationToken);
    Task<LocationDetailsDTO> GetLocationById(int id, CancellationToken cancellationToken);
    Task<IEnumerable<LocationDTOv2>> SearchLocations(SearchDTO searchRequest, CancellationToken cancellationToken);
    Task <float> GetPriceForLocation(int locationId, CancellationToken cancellationToken);
    Task<object> GetMaxPrice(CancellationToken cancellationToken);
    Task<IEnumerable<LocationDTOv2>> GetAllLocationsV2Async(CancellationToken cancellationToken);
    Task<UnavailableDatesDTO> GetUnavailableDates(int locationId, CancellationToken cancellationToken);
    Task<IEnumerable<Location>> GetAllLocations(CancellationToken cancellationToken);

}
