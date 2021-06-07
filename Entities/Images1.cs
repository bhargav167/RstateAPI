using System;
using Microsoft.AspNetCore.Http;

namespace RstateAPI.Entities {
    public class Images1 {
        public int Id { get; set; } 
        public string Url { get; set; }
        public string tag { get; set; }
        public bool cover { get; set; }
        public string PublicId { get; set; }
        public string UserId { get; set; }
        public int uniqueID { get; set; } 
    }
}