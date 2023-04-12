using CS_Eind.Models;
using CS_Eind.Models.DTO;

namespace CS_Eind.Services
{
    public interface IReservationService
    {
        Task<ResResponseDTO> MakeReservation(ResRequestDTO resRequest, CancellationToken cancellationToken);
    }
}
