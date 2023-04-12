using System.Text.Json.Serialization;

namespace CS_Eind.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public bool IsCover { get; set; }
        public int? LocationId { get; set; }
        public int? LandlordId { get; set; }

        [JsonIgnore]
        public Location? Location { get; set; }

        public Landlord? Landlord { get; set; }
    }


}
