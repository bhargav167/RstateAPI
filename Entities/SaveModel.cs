namespace RstateAPI.Entities {
    public class SaveModel {
        public int Id { get; set; }
        public BasicDetails basicDetailId { get; set; }
        public Locations locationId { get; set; } 
        public string UserId { get; set; }
        public int uniqueID { get; set; }
        public bool isConfirmed { get; set; }
        public bool isDecline { get; set; }
        public bool IsLead { get; set; }
         public string price { get; set; }
        public SaveModel () { }
    }
}