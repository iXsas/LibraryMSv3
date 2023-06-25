using System.ComponentModel.DataAnnotations;

namespace LibraryMSv3.Models.DatabaseModels
{
    public class User
    {
        [Key]//primary Key
        public Guid Id { get; set; }
        [Required, MaxLength(30)]
        public string UserName { get; set; } 
        [Required]
        public byte[] PasswordHash { get; set; }
        [Required]
        public byte[] PasswordSalt { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public UserInfo? UserInfo { get; set; }
        public Image? Image { get; set; }
        public Address? Address { get; set; }
        public User() 
        { 
        }
    }
}
