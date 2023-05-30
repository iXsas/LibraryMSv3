using System.ComponentModel.DataAnnotations;

namespace LibraryMSv3.Models.DatabaseModels
{
    public class Image
    {
        public Guid Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public byte[] ImageBytes { get; set; }
        public Image(Guid userId, byte[] image)
        {
            Id = new Guid();
            UserId = userId;
            ImageBytes = image;
        }

        public Image()
        {
        }
    }
}
