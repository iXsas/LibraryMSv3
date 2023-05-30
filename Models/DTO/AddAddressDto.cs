using System.ComponentModel.DataAnnotations;

namespace LibraryMSv3.Models.DTO
{
    public class AddAddressDto
    {
        [Required, MaxLength(50)]
        public string? City { get; set; }
        [Required, MaxLength(50)]
        public string? Street { get; set; }
        [Required, MaxLength(5)]
        public string? HouseNumber { get; set; }
        [Required, MaxLength(5)]
        public string? FlatNumber { get; set; }
    }
}
