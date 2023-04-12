using CS_Eind.Models.DTO;

namespace CS_Eind.Models.Mappers
{
    public interface IMapper
    {
        public LocationDTO Map(Location location);
        public LocationDTOv2 MapV2(Location location);
        public LocationDetailsDTO MapDetails(Location location);
        public ImageDTO MapDetailsImages(Image image);
        public LandlordDTO MapDetailsLandlord(Landlord landlord);
        public SearchDTO Search(Location location);
        public UnavailableDatesDTO unavailableDates(Reservation reservation);
        public ResResponseDTO resResponse(Reservation reservation);
        public ResRequestDTO resRequest(Reservation reservation);




    }
}
