namespace RstateAPI.Entities.ContactDetails {
    public class UserContact {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int UniqueID { get; set; }
        public string OwnerId { get; set; }
    }
}