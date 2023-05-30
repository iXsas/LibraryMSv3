﻿
namespace LibraryMSv3.Models.DTO
{
    public class AddressDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? HouseNumber { get; set; }
        public string? FlatNumber { get; set; }
    }
}
