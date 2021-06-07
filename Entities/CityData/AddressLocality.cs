namespace RstateAPI.Entities.CityData {
    public class AddressLocality {
        public int Id { get; set; }
        public string LocalityName { get; set; }
        public City city { get; set; }
        public int cityId { get; set; }
        public string CityName { get; set; }
    }
}