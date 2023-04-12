using static System.Net.Mime.MediaTypeNames;

namespace CS_Eind.Models
{
    public class Landlord
    {
        public int Id { get; set; }
        public int Age { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Image> Avatars { get; set; }
        public ICollection<Location> Locations { get; set; }


    }
}
