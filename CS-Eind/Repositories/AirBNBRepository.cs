using AutoMapper;
using Castle.Core.Resource;
using CS_Eind.Data;
using CS_Eind.Models;
using CS_Eind.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace CS_Eind.Repositories
{
    public class AirBnBRepository : IAirBnBRepository
    {

        private readonly AirBNBDbContext _context;
        public AirBnBRepository(AirBNBDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Location>> GetAllLocationsAsync(CancellationToken cancellationToken)
        {
            // Returneer alle locaties in de lijst
            var locations = await _context.Location.ToListAsync(cancellationToken);
            return locations;
        }

        public async Task<Location> GetLocationById(int id, CancellationToken cancellationToken)
        {
            var location = _context.Location.FirstOrDefault(l => l.Id == id);

            return location;
        }

        public async Task<string> GetImageForLocationAsync(int locationId, CancellationToken cancellationToken)
        {
            var image = await _context.Image.FirstOrDefaultAsync(i => i.LocationId == locationId, cancellationToken);
            return image?.Url;
        }

        public async Task<string> GetAvatarForLandlordAsync(int locationId, CancellationToken cancellationToken)
        {
            var location = await _context.Location.FirstOrDefaultAsync(l => l.Id == locationId, cancellationToken);
            if (location == null) return null;

            var landlordAvatar = await _context.Image
                .FirstOrDefaultAsync(i => i.LandlordId == location.LandlordId, cancellationToken);

            return landlordAvatar?.Url;
        }



        public async Task<float> GetPriceForLocation(int locationId, CancellationToken cancellationToken)
        {
            var location = await _context.Location.FirstOrDefaultAsync(l => l.Id == locationId, cancellationToken);
            return location.PricePerDay;
        }
        public async Task<IEnumerable<ImageDTO>> GetImagesForLocation(int locationId, CancellationToken cancellationToken)
        {
            var images = _context.Image
                .Where(i => i.LocationId == locationId)
                .Select(i => new ImageDTO { URL = i.Url, IsCover = i.IsCover });
            return images;
        }

        public async Task<Customer> GetCustomerByEmailAsync(string email, CancellationToken cancellationToken)
        {
            var customer = await _context.Customer.FirstOrDefaultAsync(c => c.Email == email, cancellationToken);
            return customer;
        }

        public async Task<string> GetNameForLandlord(int locationId, CancellationToken cancellationToken)
        {
            var location = await _context.Location.FirstOrDefaultAsync(lo => lo.Id == locationId, cancellationToken);
            if (location == null) return null;

            var landlord = await _context.Landlord.FirstOrDefaultAsync(la => la.Id == location.LandlordId, cancellationToken);
            return landlord?.FirstName;
        }

        public async Task<Reservation> CreateReservation(ResRequestDTO request, CancellationToken cancellationToken)
        {
            var location = await _context.Location.FindAsync(request.LocationId);
            if (location == null)
            {
                throw new ArgumentException($"Location with ID {request.LocationId} does not exist.");
            }
            var customer = await _context.Customer.FirstOrDefaultAsync(c => c.Email == request.Email, cancellationToken);


            // Create reservation
            var reservation = new Reservation
            {
                LocationId = location.Id,
                CustomerId = customer.Id,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Discount = request.Discount ?? 0
            };
            _context.Reservation.Add(reservation);

            await _context.SaveChangesAsync(cancellationToken);

            return reservation;
        }

        public async Task<Customer> CreateCustomerFromRequestAsync(ResRequestDTO request, CancellationToken cancellationToken)
        {
            
            var newCustomer = new Customer
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            // Add the new customer to the database
            _context.Customer.Add(newCustomer);
            await _context.SaveChangesAsync(cancellationToken);

            // Return the newly created customer
            return newCustomer;
        }

        public async Task<UnavailableDatesDTO> GetUnavailableDates(int locationId, CancellationToken cancellationToken)
        {
            var reservations = await _context.Reservation.Where(r => r.LocationId == locationId).ToListAsync(cancellationToken);

            if (reservations == null || !reservations.Any())
            {
                return new UnavailableDatesDTO { UnAvailableDates = Enumerable.Empty<DateTime>() };
            }

            var futureDates = new List<DateTime>();

            foreach (var reservation in reservations)
            {
                var reservationDates = Enumerable.Range(0, (reservation.EndDate - reservation.StartDate).Days + 1)
                    .Select(offset => reservation.StartDate.AddDays(offset))
                    .Where(date => date >= DateTime.Today) // Filter out dates in the past
                    .ToList();

                futureDates.AddRange(reservationDates);
            }

            return new UnavailableDatesDTO { UnAvailableDates = futureDates };
        }


        public async Task<IEnumerable<Location>> SearchLocations(SearchDTO searchRequest, CancellationToken cancellationToken)
        {
            var locations = _context.Location.AsQueryable();


            if (searchRequest.Features.HasValue)
            {
                locations = locations.Where(l => (int)l.LocationFeatures == searchRequest.Features.Value);
            }

            if (searchRequest.Type.HasValue)
            {
                locations = locations.Where(l => (int)l.Type == searchRequest.Type.Value);
            }

            if (searchRequest.Room.HasValue)
            {
                locations = locations.Where(l => l.Rooms >= searchRequest.Room.Value);
            }

            if (searchRequest.MinPrice.HasValue)
            {
                locations = locations.Where(l => l.PricePerDay >= searchRequest.MinPrice.Value);
            }

            if (searchRequest.MaxPrice.HasValue)
            {
                locations = locations.Where(l => l.PricePerDay <= searchRequest.MaxPrice.Value);
            }


            // If search criteria were provided, apply filters and return matching locations
            return await locations.ToListAsync(cancellationToken);
        }
    }
}
