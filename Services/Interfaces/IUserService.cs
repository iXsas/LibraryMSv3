using LibraryMSv3.Models.DTO;

namespace LibraryMSv3.Services.Interfaces
{
    public interface IUserService
    {
        ResponseDto Signup(string username, string password);
        ResponseDto Login(string username, string password);
        public Task<UserInfoDto?> CreateUserDetails(Guid userId, AddInfoDto addInfoDto);
        public Task<AddressDto?> CreateUserAddress(Guid userId, AddAddressDto userAddAddressDto);
        public Task<UserDto?> GetUserByID(Guid userId);
        public Task<ImageDto?> UploadUserPhoto(Guid userId, byte[] profilePic);
        public Task<UserInfoDto?> UpdateUserFirstName(Guid userId, string newFirstName);
        public Task<UserInfoDto?> UpdateUserLastName(Guid userId, string newLastName);
        public Task<UserInfoDto?> UpdatePersonalCode(Guid userId, long newPersonalCode);
        public Task<UserInfoDto?> UpdatePhoneNumber(Guid userId, string newPhoneNumber);
        public Task<UserInfoDto?> UpdateEmail(Guid userId, string newEmail);
        public Task<AddressDto?> UpdateCity(Guid userId, string newCity);
        public Task<AddressDto?> UpdateStreet(Guid userId, string newStreet);
        public Task<AddressDto?> UpdateHouseNb(Guid userId, string newHouseNb);
        public Task<AddressDto?> UpdateFlatNb(Guid userId, string newFlatNb);
        public Task<ImageDto?> DeleteImageById(Guid userId);
        public Task<UserDto?> GetUserByName(string username);
        public Task<UserDto?> DeleteUser(Guid userId);
    }
}
