using static System.Net.Mime.MediaTypeNames;

namespace CS_Eind.Models
{
    public class Location
    {
        public int Id { get; set; }
        public int Rooms { get; set; }
        public string Title { get; set; }
        public Landlord Landlord { get; set; }
        public int LandlordId { get; set; }
        public string SubTitle { get; set; }
        public float PricePerDay { get; set; }
        public string Description { get; set; }
        public LocationType Type { get; set; }
        public Features LocationFeatures { get; set; }
        public int NumberOfGuests { get; set; }

        public ICollection<Reservation> Reservations { get; set; }

        public ICollection<Image> Images { get; set; }

        [Flags]
        public enum Features
        {
            Smoking = 1,
            PetsAllowed = 2,
            Wifi = 4,
            TV = 8,
            Bath = 16,
            Breakfast = 32
        }

        public enum LocationType
        {
            Appartment = 0,
            Cottage = 1,
            Chalet = 2,
            Room = 3,
            Hotel = 4,
            House = 5
        }


    }
}
