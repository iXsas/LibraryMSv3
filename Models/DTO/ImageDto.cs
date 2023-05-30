namespace LibraryMSv3.Models.DTO
{
    public class ImageDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public byte[]? Image { get; set; }
    }
}
