using LibraryMSv3.Models.DatabaseModels;

namespace LibraryMSv3.Repositories.Interfaces
{
    public interface IUserInfoRepository
    {
        public Task<UserInfo?> GetUserDetailsByUserID(Guid userId);
        public Task<UserInfo?> AddUserDetails(UserInfo userinfo);
        public Task UpdateUserInfo(UserInfo userinfo);
        public UserInfo GetAllInfoByIDNew(Guid userId);
    }
}
