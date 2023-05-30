using Microsoft.AspNetCore.Http;
using LibraryMSv3.Attributes;

namespace LibraryMSv3.Services
{
    public class ImageUploadRequest
        {
            [AllowedExtensionsAtribute(new string[] { ".png", ".jpg",".jpeg" })]
            public IFormFile Image { get; set; }
        }
}
