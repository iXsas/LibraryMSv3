using AutoMapper;
using LibraryMSv3.Models.DatabaseModels;
using LibraryMSv3.Models.DTO;
using LibraryMSv3.Repositories.Interfaces;
using LibraryMSv3.Services.Interfaces;

namespace LibraryMSv3.Services.Services
{
    public class UserInfoService : IUserInfoService
    {
        private readonly IImageRepository _imageRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IUserInfoRepository _userInfoRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserInfoService(IImageRepository imageRepository, IAddressRepository addressRepository, 
            IUserInfoRepository userInfoRepository, IUserRepository userRepository, IMapper mapper)
        {
            _imageRepository = imageRepository;
            _addressRepository = addressRepository;
            _userInfoRepository = userInfoRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public UserInfo _GetAllInfoByID(Guid userId)
        {
            UserInfo userInfo = _userInfoRepository.GetAllInfoByIDNew(userId);
            return userInfo;
        }
        public Address GetAddressInfo(Guid userId)
        {
            Address userioAddress = _addressRepository._GetUserAddressByUserID(userId);
                return userioAddress;
        }
    }
}
