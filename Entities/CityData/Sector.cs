namespace RstateAPI.Entities.CityData {
    public class Sector {
        public int Id { get; set; }
        public string SectorName { get; set; } 
        public AddressLocality locality { get; set; }
        public int localityId { get; set; }
    }
}