using Microsoft.AspNetCore.Http;

namespace RstateAPI.Entities.Dtos.Profile {
    public class UploadPhoto {
        public string Id { get; set; }
        public IFormFile File { get; set; }
        public string url { get; set; }
    }
}