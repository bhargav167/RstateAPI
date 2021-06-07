namespace RstateAPI.Entities.CityData
{
    public class Locality
    {
        public int Id { get; set; }
        public string LocalityName { get; set; }
        public City cityId { get; set; }
    }
}