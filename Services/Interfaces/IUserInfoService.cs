using LibraryMSv3.Models.DatabaseModels;

namespace LibraryMSv3.Services.Interfaces
{
    public interface IUserInfoService
    {
        public UserInfo _GetAllInfoByID(Guid userId);
        public Address GetAddressInfo(Guid userId);
    }
}
