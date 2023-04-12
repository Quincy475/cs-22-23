using AutoMapper;
using CS_Eind.Models;
using CS_Eind.Models.DTO;

namespace CS_Eind
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Location, LocationDTO>();
            CreateMap<Location, LocationDTOv2>();
            CreateMap<Location, LocationDetailsDTO>();
            CreateMap<Landlord, LandlordDTO>();
            CreateMap<Image, ImageDTO>();
            CreateMap<Location, SearchDTO>();
            CreateMap<Reservation, ResRequestDTO>();
            CreateMap<Reservation, ResResponseDTO>();
            CreateMap<Reservation, UnavailableDatesDTO>();

        }
    }
}
