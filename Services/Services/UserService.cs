using LibraryMSv3.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using LibraryMSv3.Models.DatabaseModels;
using LibraryMSv3.Models.DTO;
using LibraryMSv3.Repositories.Interfaces;
using AutoMapper;
using Image = LibraryMSv3.Models.DatabaseModels.Image;

namespace LibraryMSv3.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IAddressRepository _addressRepository;
        private readonly IUserInfoRepository _userInfoRepository;
        private readonly IConfiguration _configuration;
        private readonly IImageRepository _imageRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository repository, IAddressRepository addressRepository, IUserInfoRepository userInfoRepository,
            IConfiguration configuration, IImageRepository imageRepository, IMapper mapper)
        {
            _repository = repository;
            _addressRepository = addressRepository;
            _configuration = configuration;
            _userInfoRepository = userInfoRepository;
            _imageRepository = imageRepository;
            _mapper = mapper;
        }

        public ResponseDto Login(string username, string password)
        {
            User user = _repository.GetUser(username);
            if (user is null)
                return new ResponseDto(false, "Username or password does not match");

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return new ResponseDto(false, "Username or password does not match");

            string token = CreateToken(user);
            return new ResponseDto(true, token);
        }

        public ResponseDto Signup(string username, string password)
        {
            User user = _repository.GetUser(username);
            if (user is not null)
                return new ResponseDto(false, "User already exists");

            user = CreateUser(username, password);
            _repository.SaveUser(user);
            return new ResponseDto(true);
        }

        private User CreateUser(string username, string password)
        {
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            var user = new User
            {
                Id = Guid.NewGuid(),
                UserName = username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = "user",
                CreatedDate = DateTime.UtcNow
        };
            return user;
        }
        public async Task<UserInfoDto?> CreateUserDetails(Guid userId, AddInfoDto addInfoDto)
        {
            User? user = await _repository.Get(userId);
            UserInfo? userDetailsCheck = await _userInfoRepository.GetUserDetailsByUserID(userId);
            if (user != null && userDetailsCheck == null)
            {
                UserInfo userInfos = new UserInfo();
                _mapper.Map(addInfoDto, userInfos);
                userInfos.UserId = userId;
                var addedUser = await _userInfoRepository.AddUserDetails(userInfos);
                UserInfoDto userInfoDto = _mapper.Map<UserInfoDto>(addedUser);
                return userInfoDto;
            }
            return null;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }

        private string CreateToken(User user)
        {
            List<Claim> claimse = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
            };
            var secretToken = _configuration.GetSection("JWT:Token").Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretToken));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                claims: claimse,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
   
            return jwt;
        }

        public async Task<AddressDto?> CreateUserAddress(Guid userId, AddAddressDto userAddAddressDto)
        {
            User? user = await _repository.Get(userId);
            Address? userAddressCheck = await _addressRepository.GetUserAddressByUserID(userId);
            if (user != null && userAddressCheck == null)
            {
                Address newUserAddress = new Address();
                _mapper.Map(userAddAddressDto, newUserAddress);
                newUserAddress.UserId = userId;
                await _addressRepository.AddAddress(newUserAddress);
                AddressDto userAddressDto = _mapper.Map<AddressDto>(newUserAddress);
                return userAddressDto;
            }
            return null;
        }

        public async Task<UserDto?> GetUserByID(Guid userId)
        {
            User? user = await _repository.Get(userId);
            if (user != null)
            {
                UserDto userDto = _mapper.Map<UserDto>(user);
                return userDto;
            }
            return null;
        }
        public async Task<ImageDto?> UploadUserPhoto(Guid userId, byte[] profilio_foto)
        {
            User? user = await _repository.Get(userId);
            Image? tikrinamafoto = await _imageRepository.GetImage(userId);
            if (user != null && tikrinamafoto == null)
            {
                Image userioPhoto = new Image(userId, profilio_foto);
                await _imageRepository.AddUserPhoto(userioPhoto);
                ImageDto imageDto = _mapper.Map<ImageDto>(userioPhoto);
                return imageDto;
            }
            return null;
        }

        public async Task<UserInfoDto?> UpdateUserFirstName(Guid userId, string newFirstName)
        {
            UserInfo? userioInfo = await _userInfoRepository.GetUserDetailsByUserID(userId);
            if (userioInfo != null)
            {
                userioInfo.FirstName = newFirstName;
                await _userInfoRepository.UpdateUserInfo(userioInfo);
                UserInfoDto UserInfoDto = _mapper.Map<UserInfoDto>(userioInfo);
                return UserInfoDto;
            }
            return null;
        }

        public async Task<UserInfoDto?> UpdateUserLastName(Guid userId, string newLastName)
        { UserInfo? userioInfo = await _userInfoRepository.GetUserDetailsByUserID(userId);
            if (userioInfo != null)
            {
                userioInfo.LastName = newLastName;
                await _userInfoRepository.UpdateUserInfo(userioInfo);
        UserInfoDto UserInfoDto = _mapper.Map<UserInfoDto>(userioInfo);
                return UserInfoDto;
            }
            return null;
        }
        public async Task<UserInfoDto?> UpdatePersonalCode(Guid userId, long newPersonalCode)
        {
            UserInfo? userioInfo = await _userInfoRepository.GetUserDetailsByUserID(userId);
            if (userioInfo != null)
            {
                userioInfo.PersonalCode = newPersonalCode;
                await _userInfoRepository.UpdateUserInfo(userioInfo);
                UserInfoDto UserInfoDto = _mapper.Map<UserInfoDto>(userioInfo);
                return UserInfoDto;
            }
            return null;
        }
        public async Task<UserInfoDto?> UpdatePhoneNumber(Guid userId, string newPhoneNumber)
        {
            UserInfo? userioInfo = await _userInfoRepository.GetUserDetailsByUserID(userId);
            if (userioInfo != null)
            {
                userioInfo.PhoneNumber = newPhoneNumber;
                await _userInfoRepository.UpdateUserInfo(userioInfo);
                UserInfoDto UserInfoDto = _mapper.Map<UserInfoDto>(userioInfo);
                return UserInfoDto;
            }
            return null;
        }

        public async Task<UserInfoDto?> UpdateEmail(Guid userId, string newEmail)
        {
            UserInfo? userioInfo = await _userInfoRepository.GetUserDetailsByUserID(userId);
            if (userioInfo != null)
            {
                userioInfo.Email = newEmail;
                await _userInfoRepository.UpdateUserInfo(userioInfo);
                UserInfoDto UserInfoDto = _mapper.Map<UserInfoDto>(userioInfo);
                return UserInfoDto;
            }
            return null;
        }

        public async Task<AddressDto?> UpdateCity(Guid userId, string newCity)
        {
            Address? userioAddress = await _addressRepository.GetUserAddressByUserID(userId);
            if (userioAddress != null)
            {
                userioAddress.City = newCity;
                await _addressRepository.UpdateAddress(userioAddress);
                AddressDto AddressDto = _mapper.Map<AddressDto>(userioAddress);
                return AddressDto;
            }
            return null;
        }

        public async Task<AddressDto?> UpdateStreet(Guid userId, string newStreet)
        {
            Address? userioAddress = await _addressRepository.GetUserAddressByUserID(userId);
            if (userioAddress != null)
            {
                userioAddress.Street = newStreet;
                await _addressRepository.UpdateAddress(userioAddress);
                AddressDto AddressDto = _mapper.Map<AddressDto>(userioAddress);
                return AddressDto;
            }
            return null;
        }

        public async Task<AddressDto?> UpdateHouseNb(Guid userId, string newHouseNb)
          {
            Address? userioAddress = await _addressRepository.GetUserAddressByUserID(userId);
            if (userioAddress != null)
            {
                userioAddress.HouseNumber = newHouseNb;
                await _addressRepository.UpdateAddress(userioAddress);
        AddressDto AddressDto = _mapper.Map<AddressDto>(userioAddress);
                return AddressDto;
            }
            return null;
        }

        public async Task<AddressDto?> UpdateFlatNb(Guid userId, string newFlatNb)
           {
            Address? userioAddress = await _addressRepository.GetUserAddressByUserID(userId);
            if (userioAddress != null)
            {
                userioAddress.FlatNumber = newFlatNb;
                await _addressRepository.UpdateAddress(userioAddress);
                AddressDto addressDto = _mapper.Map<AddressDto>(userioAddress);
                return addressDto;
            }
            return null;
           }

        public async Task<ImageDto?> DeleteImageById(Guid userId)
        {
            Image? userView = await _imageRepository.GetImage(userId);
            if (userView != null)
            {
                await _imageRepository.DeleteImage(userView);
                ImageDto ImageDto = _mapper.Map<ImageDto>(userView);
                return ImageDto;
            }
            return null;
        }

        public async Task<UserDto?> GetUserByName(string username)
        {
            User? user = await _repository.Get(username);
            UserDto userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }
        public async Task<UserDto?> DeleteUser(Guid userId)
        {
            User? user = await _repository.Get(userId);
            if (user != null)
            {
                await _repository.DeleteUser(user);
                return _mapper.Map<UserDto>(user);
            }
            return null;
        }

    }
}
