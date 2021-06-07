namespace RstateAPI.Entities.CityData {
    public class Pocket {
        public int Id { get; set; }
        public string PocketAddress { get; set; }
        public Sector sector { get; set; }
        public int sectorId { get; set; }
    }
}