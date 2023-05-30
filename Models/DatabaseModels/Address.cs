using System.ComponentModel.DataAnnotations;

namespace LibraryMSv3.Models.DatabaseModels
{
    public class Address
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        public User? User { get; set; }
        [Required, MaxLength(50)]
        public string City { get; set; }
        [Required, MaxLength(50)]
        public string Street { get; set; }
        [Required, MaxLength(5)]
        public string HouseNumber { get; set; }
        [Required, MaxLength(5)]
        public string FlatNumber { get; set; }

        public Address()
        {
        }
    }
}
