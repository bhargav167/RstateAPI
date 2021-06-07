using Microsoft.AspNetCore.Http;

namespace RstateAPI.Entities.Dtos {
    public class PhotoDtos {
        public IFormFile File { get; set; }
        public string url { get; set; }
        public string tag { get; set; }
        public bool cover { get; set; }
        public string PublicId { get; set; }
        public string UserId { get; set; }
        public int uniqueID { get; set; }
    }
}