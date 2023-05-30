using System.ComponentModel.DataAnnotations;

namespace LibraryMSv3.Models.DatabaseModels
{
    public class UserInfo
    {
        [Key]//primary Key
        public Guid Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        public User? User { get; set; }
        [Required, MaxLength(30)]
        public string? FirstName { get; set; }
        [Required, MaxLength(30)]
        public string? LastName { get; set; }
        [Required, MaxLength(30)]
        public long? PersonalCode { get; set; }
        [Required, MaxLength(12)]
        public string? PhoneNumber { get; set; }
        [Required, MaxLength(50)]
        public string? Email { get; set; }
    }
}
