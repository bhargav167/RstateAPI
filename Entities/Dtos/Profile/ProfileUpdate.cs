using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RstateAPI.Entities.Profile {
    public class ProfileUpdate {
        public ProfileUpdate () {
            Claims = new List<string> ();
            Roles = new List<string> ();
        }
        public string Id { get; set; }

        [Required]
        public string UserName { get; set; }
        public string MobileNo { get; set; }

        
        public string Email { get; set; }
        public bool ConfirmEmail { get; set; }
        public bool confirmMobile { get; set; }

        // Broker 
        public string BrokerFirm { get; set; }
        public string FirmAddress { get; set; }
        public string FirmAddress1 { get; set; }
        public string GstNo { get; set; }
        public string reraNo { get; set; }
        public string landlineNo { get; set; }
        public string mobNo1 { get; set; }
        public string mobNo2 { get; set; }
        public string mobNo3 { get; set; }
        public string mobNo4 { get; set; }
        public string locations { get; set; }
        public List<string> Claims { get; set; }
        public IList<string> Roles { get; set; }
    }
}