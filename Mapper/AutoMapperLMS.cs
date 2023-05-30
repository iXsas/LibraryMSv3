using AutoMapper;
using LibraryMSv3.Models.DatabaseModels;
using LibraryMSv3.Models.DTO;

namespace LibraryMSv3.Mapper
{
    public class AutoMapperLMS : Profile
    {
        public AutoMapperLMS()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<Address, AddAddressDto>().ReverseMap();
            CreateMap<UserInfo, UserInfoDto>().ReverseMap();
            CreateMap<UserInfo, AddInfoDto>().ReverseMap();
            CreateMap<Image, ImageDto>().ReverseMap();
        }
    }
}
