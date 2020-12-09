using Microsoft.AspNetCore.Http;

namespace API.DTOs
{
    public class PhotoUpdateDto
    {
        public string Description { get; set; } 
        public IFormFile File { get; set;}
    }
}