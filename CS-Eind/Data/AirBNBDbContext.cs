using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CS_Eind.Models;


namespace CS_Eind.Data
{
    public class AirBNBDbContext : DbContext
    {
        public AirBNBDbContext(DbContextOptions<AirBNBDbContext> options)
            : base(options)
        {
        }

        


        public DbSet<Location> Location { get; set; }
        public DbSet<Landlord> Landlord { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Reservation> Reservation { get; set; }


        public async Task AddLocationAsync(Location location)
        {
            await Location.AddAsync(location);
            await SaveChangesAsync();
        }

        public async Task AddImageAsync(Image image)
        {
            await Image.AddAsync(image);
            await SaveChangesAsync();
        }
        public async Task AddCustomerAsync(Customer customer)
        {
            await Customer.AddAsync(customer);
            await SaveChangesAsync();
        }
       
        public async Task AddLandlordAsync(Landlord landlord)
        {
            await Landlord.AddAsync(landlord);
            await SaveChangesAsync();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Landlord>().HasData(
             new Landlord {Id =1, FirstName = "John", LastName = "Doe", Age = 40 },
             new Landlord { Id = 2, FirstName = "Mister", LastName = "Bean", Age = 55 },
             new Landlord {Id =3,FirstName = "Johny", LastName = "Test", Age = 40 },
             new Landlord { Id =4,FirstName = "Mister", LastName = "Mister", Age = 35 }
             );

            modelBuilder.Entity<Customer>().HasData(
                new Customer {Id =1, FirstName = "Alice", LastName = "Smith", Email = "alice.smith@example.com" },
                new Customer { Id =2,FirstName = "Bob", LastName = "Jones", Email = "bob.jones@example.com" },
                new Customer { Id =3,FirstName = "Alice", LastName = "InWonderLand", Email = "alice.Wonder@example.com" },
                new Customer { Id =4,FirstName = "Tim", LastName = "Smiles", Email = "bob.Smiles@example.com" }

                );

            modelBuilder.Entity<Location>().HasData(
                new Location
                {
                    Id = 1,
                    Title = "De Boerenhoeve",
                    SubTitle = "Lekker veel ruimte",
                    Description = "De camping ligt verscholen achter de boerderij in de polder. Op fietsafstand (5 minuten) liggen het dorpje Nieuwvliet, de zee, het strand, het bos van Erasmus en het natuurgebied de Knokkert.",
                    Rooms = 5,
                    NumberOfGuests = 12,
                    PricePerDay = 300,
                    Type = CS_Eind.Models.Location.LocationType.Appartment,
                    LocationFeatures = CS_Eind.Models.Location.Features.Bath | CS_Eind.Models.Location.Features.Wifi| CS_Eind.Models.Location.Features.PetsAllowed,
                    LandlordId = 1
                },
                new Location
                {
                    Id = 2,
                    Title = "Frankie's Penthouse",
                    SubTitle = "Te gek uitzicht",
                    Description = "Nee, dit puike penthouse dat al jaren te koop stond en nu is verkocht, is niet de duurste woning van ons land. Bij lange na niet. Wel is de meer dan €30.000 per vierkante meter woonruimte een record in ons land.",
                    Rooms = 2,
                    NumberOfGuests = 4,
                    PricePerDay = 400,
                    Type = CS_Eind.Models.Location.LocationType.Chalet,
                    LocationFeatures = CS_Eind.Models.Location.Features.Wifi | CS_Eind.Models.Location.Features.TV| CS_Eind.Models.Location.Features.Bath,
                    LandlordId = 2
                },
                new Location
                {Id = 3,
                    Title = "Cozy Cottage",
                    SubTitle = "Near the lake",
                    Rooms = 2,
                    LandlordId = 4,
                    PricePerDay = 100,
                    Type = CS_Eind.Models.Location.LocationType.Cottage,
                    Description = "A cozy cottage near the lake with beautiful view",
                    NumberOfGuests = 4,
                    LocationFeatures = CS_Eind.Models.Location.Features.PetsAllowed | CS_Eind.Models.Location.Features.Wifi | CS_Eind.Models.Location.Features.Breakfast
                },
                new Location
                {Id = 4,
                    Title = "Luxury Apartment",
                    SubTitle = "Downtown",
                    Rooms = 3,
                    LandlordId = 3,
                    PricePerDay = 200,
                    Type = CS_Eind.Models.Location.LocationType.Appartment,
                    Description = "A luxury apartment in the heart of the city",
                    NumberOfGuests = 6,
                    LocationFeatures = CS_Eind.Models.Location.Features.TV | CS_Eind.Models.Location.Features.Wifi | CS_Eind.Models.Location.Features.Bath | CS_Eind.Models.Location.Features.Breakfast
                }

            );

            modelBuilder.Entity<Image>().HasData(
                new Image { Id = 1, Url = "http://cdn.home-designing.com/wp-content/uploads/2010/11/Gingerbread-cottage-house-beautiful-landscape.jpg", IsCover = true, LocationId = 1 },
                new Image {Id =2, Url = "http://weknowyourdreams.com/images/penthouse/penthouse-01.jpg", IsCover = true, LocationId = 2 },
                new Image { Id =3,Url = "http://www.cottageblog.ca/wp-content/uploads/LakeJoeCabin-1280.jpg", IsCover = true, LocationId = 3 },
                new Image { Id =4,Url = "https://tse3.mm.bing.net/th?id=OIP.5Nd1LqWRLikaFVlmbw2RfQHaLG&pid=Api", IsCover = true, LocationId = 4 },
                new Image {Id =5, Url = "https://tse1.explicit.bing.net/th?id=OIP.tOrydk5j46G7kWuS1elhsgHaE8&pid=Api", IsCover = false, LocationId = 2 },
                new Image { Id =6,Url = "https://tse1.mm.bing.net/th?id=OIP.KXS2egJ9qayUAMdLc9cCYQHaFX&pid=Api", IsCover = false, LocationId = 3 },
                new Image {Id =7, Url = "https://tse1.mm.bing.net/th?id=OIP.PLvxFihIhkAe7oBQ_Ma-TwHaE7&pid=Api", IsCover = false, LocationId = 4 },
                new Image {Id =8, Url = "https://tse4.mm.bing.net/th?id=OIP.iBoBzz9cJd-jmN7jBpM2HwHaKB&pid=Api", IsCover = true, LandlordId = 1 },
                new Image {Id =9, Url = "https://tse4.mm.bing.net/th?id=OIP.bkfMC2AL9D8fFqfyAmsTqAHaI7&pid=Api", IsCover = true, LandlordId = 2 },
                new Image {Id =10, Url = "https://tse4.mm.bing.net/th?id=OIP.q99MVI-6mP4jD1NpWGR8bAHaHa&pid=Api", IsCover = true, LandlordId = 3 },
                new Image {Id =11, Url = "https://tse4.mm.bing.net/th?id=OIP.MB0oW9c-Mr-w-wPv91_VsgHaLH&pid=Api", IsCover = true, LandlordId = 4 }
                );


            modelBuilder.Entity<Reservation>().HasData(
    new Reservation { Id = 1, LocationId = 1, CustomerId = 1, StartDate = new DateTime(2023, 5, 1), EndDate = new DateTime(2023, 5, 7) },
    new Reservation { Id = 2, LocationId = 2, CustomerId = 2, StartDate = new DateTime(2023, 5, 15), EndDate = new DateTime(2023, 5, 20) },
    new Reservation { Id = 3, LocationId = 3, CustomerId = 3, StartDate = new DateTime(2023, 6, 1), EndDate = new DateTime(2023, 6, 7) },
    new Reservation { Id = 4, LocationId = 4, CustomerId = 4, StartDate = new DateTime(2023, 7, 1), EndDate = new DateTime(2023, 7, 10) },
    new Reservation { Id = 5, LocationId = 4, CustomerId = 1, StartDate = new DateTime(2023, 8, 1), EndDate = new DateTime(2023, 8, 5) },
    new Reservation { Id = 6, LocationId = 1, CustomerId = 2, StartDate = new DateTime(2023, 9, 1), EndDate = new DateTime(2023, 9, 7) },
    new Reservation { Id = 7, LocationId = 2, CustomerId = 3, StartDate = new DateTime(2023, 9, 15), EndDate = new DateTime(2023, 9, 20) },
    new Reservation { Id = 8, LocationId = 3, CustomerId = 4, StartDate = new DateTime(2023, 10, 1), EndDate = new DateTime(2023, 10, 7) },
    new Reservation { Id = 9, LocationId = 4, CustomerId = 1, StartDate = new DateTime(2023, 11, 1), EndDate = new DateTime(2023, 11, 10) },
    new Reservation { Id = 10, LocationId = 1, CustomerId = 1, StartDate = new DateTime(2023, 12, 1), EndDate = new DateTime(2023, 12, 5) },
    new Reservation { Id = 11, LocationId = 1, CustomerId = 1, StartDate = new DateTime(2024, 1, 1), EndDate = new DateTime(2024, 1, 7) },
    new Reservation { Id = 12, LocationId = 2, CustomerId = 2, StartDate = new DateTime(2024, 2, 1), EndDate = new DateTime(2024, 2, 10) }
);
        }
    }
}
