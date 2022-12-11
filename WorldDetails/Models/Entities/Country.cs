using System.Net.NetworkInformation;

namespace WorldDetails.Models.Entities
{
    public class Country
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string IOS3 { get; set; }
        public int Number { get; set; }
        public string Continent_code { get; set; }
        public int Display_order { get; set; }
    }
}
