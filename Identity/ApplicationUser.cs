using System;
using Microsoft.AspNetCore.Identity;

namespace RstateAPI {
    public class ApplicationUser : IdentityUser {
        public string FullName { get; set; }
        public string Password { get; set; }
        
        public string userId { get; set; }
        public string imagUrl { get; set; }
        public string RoleId { get; set; }
        public string tokenid { get; set; }
        public bool Islogin { get; set; } 
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
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }

        public ApplicationUser()
        {
            CreateDate=DateTime.Now;
            IsActive=true;
        }
    }
}