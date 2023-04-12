using CS_Eind.Models;
using CS_Eind.Models.DTO;
using AutoMapper;
using CS_Eind.Repositories;
using CS_Eind.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Net;

public class SearchService : ISearchService
{
    private readonly IAirBnBRepository _airBnBRepository;
    private readonly IMapper _mapper;

    public SearchService(IAirBnBRepository airBnBRepository, IMapper mapper)
    {
        _mapper = mapper;
        _airBnBRepository = airBnBRepository;
    }

    public async Task<IEnumerable<Location>> GetAllLocations(CancellationToken cancellationToken)
    {
        var locations = await _airBnBRepository.GetAllLocationsAsync(cancellationToken);


        return locations;
    }
    public async Task<IEnumerable<LocationDTO>> GetAllLocationsAsync(CancellationToken cancellationToken)
    {
        var locations = await _airBnBRepository.GetAllLocationsAsync(cancellationToken);
        var locationDtos = _mapper.Map<IEnumerable<LocationDTO>>(locations);

        foreach (var locationDto in locationDtos)
        {
            // Set ImageURL property based on Image.URL where LocationID matches the current location ID
            var imageTask = GetImageForLocationAsync(locationDto.Id, cancellationToken);
            var avatarTask = GetAvatarForLandlordAsync(locationDto.Id, cancellationToken);

            if (imageTask.Result != null)
            {
                locationDto.imageURL = imageTask.Result;
            }

            if (avatarTask.Result != null)
            {
                locationDto.landlordAvatarURL = avatarTask.Result;
            }
        }

        return locationDtos;
    }
    public async Task<UnavailableDatesDTO> GetUnavailableDates(int locationId, CancellationToken cancellationToken)
    {
       

        var dates = await _airBnBRepository.GetUnavailableDates(locationId, cancellationToken);

        

        return dates;
    }
    public async Task<LocationDetailsDTO> GetLocationById(int id, CancellationToken cancellationToken)
    {

        var location = await _airBnBRepository.GetLocationById(id, cancellationToken);
        var locationDetailsDto = _mapper.Map<LocationDetailsDTO>(location);
        var avatar = await GetAvatarForLandlordAsync(location.Id, cancellationToken);
        var name = await GetNameForLandlord(location.Id, cancellationToken);
        var images = await GetImagesForLocation(id, cancellationToken);


        locationDetailsDto.Features = (int)location.LocationFeatures;
       
        locationDetailsDto.Landlord = new LandlordDTO();
        var imagesDto = new List<ImageDTO>();

                locationDetailsDto.Landlord.Avatar = avatar;
                locationDetailsDto.Landlord.Name = name;
            


            foreach (var image in images)
            {
                var imageDto = _mapper.Map<ImageDTO>(image);
                imagesDto.Add(imageDto);
            }
            locationDetailsDto.Images = imagesDto;
        


        return locationDetailsDto;
    }

    public async Task<string> GetImageForLocationAsync(int locationId, CancellationToken cancellationToken)
    {

        await Task.Delay(1000, cancellationToken);
        var image = await _airBnBRepository.GetImageForLocationAsync(locationId, cancellationToken);
        return image;
    }

    public async Task<string> GetAvatarForLandlordAsync(int locationId, CancellationToken cancellationToken)
    {
        await Task.Delay(2000, cancellationToken);
        var avatar = await _airBnBRepository.GetAvatarForLandlordAsync(locationId, cancellationToken);
        return avatar;
    }

    public async Task<object> GetMaxPrice(CancellationToken cancellationToken)
    {
        var price = await _airBnBRepository.GetAllLocationsAsync(cancellationToken);
        var maxPrice = price.Max(l => l.PricePerDay);


        return maxPrice;
    }


    public async Task<float> GetPriceForLocation(int locationId, CancellationToken cancellationToken)
    {
        Task.Delay(2000, cancellationToken);

        return await _airBnBRepository.GetPriceForLocation(locationId, cancellationToken);
    }

    public async Task<IEnumerable<ImageDTO>> GetImagesForLocation(int locationId, CancellationToken cancellationToken)
    {

        await Task.Delay(3000, cancellationToken);
        var images = await _airBnBRepository.GetImagesForLocation(locationId, cancellationToken);

        return images;


    }
    

    public async Task<string> GetNameForLandlord(int locationId, CancellationToken cancellationToken)
    {
        Task.Delay(1500, cancellationToken);

        var name = await _airBnBRepository.GetNameForLandlord(locationId, cancellationToken);
        return name;
    }

    
    public async Task<IEnumerable<LocationDTOv2>> GetAllLocationsV2Async(CancellationToken cancellationToken)
    {
        var locations = await _airBnBRepository.GetAllLocationsAsync(cancellationToken);
        var locationDtos = _mapper.Map<IEnumerable<LocationDTOv2>>(locations);

        foreach (var locationDto in locationDtos)
        {

            // Set ImageURL property based on Image.URL where LocationID matches the current location ID
            var imageTask = GetImageForLocationAsync(locationDto.id, cancellationToken);
            var avatarTask = GetAvatarForLandlordAsync(locationDto.id, cancellationToken);

            locationDto.price = locations.FirstOrDefault(l => l.Id == locationDto.id)?.PricePerDay ?? 0;
            locationDto.type = (int)locations.FirstOrDefault(l => l.Id == locationDto.id)?.Type;

            if (imageTask.Result != null)
            {
                locationDto.imageURL = imageTask.Result;
            }

            if (avatarTask.Result != null)
            {
                locationDto.landlordAvatarURL = avatarTask.Result;
            }
        }

        return locationDtos;
    }
    public async Task<IEnumerable<LocationDTOv2>> SearchLocations(SearchDTO searchRequest, CancellationToken cancellationToken)
    {

        var locations = await _airBnBRepository.GetAllLocationsAsync(cancellationToken);

        var searchedLocations = await _airBnBRepository.SearchLocations(searchRequest, cancellationToken);

        var locationDtos = _mapper.Map<IEnumerable<LocationDTOv2>>(searchedLocations);

            foreach (var locationDto in locationDtos)
            {

                // Set ImageURL property based on Image.URL where LocationID matches the current location ID
                var imageTask = GetImageForLocationAsync(locationDto.id, cancellationToken);
                var avatarTask = GetAvatarForLandlordAsync(locationDto.id, cancellationToken);

                locationDto.price = locations.FirstOrDefault(l => l.Id == locationDto.id)?.PricePerDay ?? 0;
                locationDto.type = (int)locations.FirstOrDefault(l => l.Id == locationDto.id)?.Type;

                if (imageTask.Result != null)
                {
                    locationDto.imageURL = imageTask.Result;
                }

                if (avatarTask.Result != null)
                {
                    locationDto.landlordAvatarURL = avatarTask.Result;
                }

        }
        return locationDtos;

    }

    

}
