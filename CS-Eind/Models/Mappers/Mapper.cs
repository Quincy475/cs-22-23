using CS_Eind.Models.DTO;

namespace CS_Eind.Models.Mappers
{
    public class Mapper : IMapper
    {
        public LocationDTO Map(Location location)
        {
            return new LocationDTO
            {

            };
        }
        public LocationDTOv2 MapV2(Location location)
        {
            return new LocationDTOv2
            {

            };
        }

        public LocationDetailsDTO MapDetails(Location location)
        {
            return new LocationDetailsDTO
            {

            };
        }

        public LandlordDTO MapDetailsLandlord(Landlord landlord)
        {
            return new LandlordDTO
            {

            };
        }
        public ImageDTO MapDetailsImages(Image image)
        {
            return new ImageDTO
            {

            };
        }

        public SearchDTO Search(Location location)
        {
            return new SearchDTO
            {

            };
        }

        public ResRequestDTO resRequest(Reservation reservation) 
        {
            return new ResRequestDTO
            {

            };
        }

        public ResResponseDTO resResponse(Reservation reservation)
        {
            return new ResResponseDTO
            {

            };

        }

        public UnavailableDatesDTO unavailableDates(Reservation reservation)
        {
            return new UnavailableDatesDTO
            {

            };
        }

       
    }
}
