using System.ComponentModel.DataAnnotations;

namespace LibraryMSv3.Models.DTO
{
    public class AddInfoDto
    {
        [Required]
        [StringLength(30)]
        public string? FirstName { get; set; }
        [Required]
        [StringLength(30)]
        public string? LastName { get; set; }
        [Required]
        public int PersonalCode { get; set; }
        [Required]
        [StringLength(12)]
        public string? PhoneNumber { get; set; }
        [Required]
        [StringLength(50)]
        public string? Email { get; set; }
    }
}
