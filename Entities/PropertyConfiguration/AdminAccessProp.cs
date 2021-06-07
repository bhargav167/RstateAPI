namespace RstateAPI.Entities.PropertyConfiguration {
    public class AdminAccessProp {
        public int Id { get; set; }
        public bool IsManageAddress { get; set; }
        public bool IsManageFeild { get; set; }
        public bool IsSetPropImg { get; set; }
        public bool IsManageCity { get; set; }
        public bool IsManageLocality { get; set; }
        public bool IsManageSector { get; set; }
        public bool IsManagePocket { get; set; }
        public string UserId { get; set; }
    }
}