using LibraryMSv3.Data;
using LibraryMSv3.Models.DatabaseModels;
using LibraryMSv3.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LibraryMSv3.Repositories
{
    public class UserInfoRepository : IUserInfoRepository
    {
        private ApplicationDbContext _context;
        public UserInfoRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }
        public async Task<UserInfo> GetUserDetailsByUserID(Guid userId)
        {
            return await _context.UserInfos.FirstOrDefaultAsync(ud => ud.UserId == userId);
        }
        public async Task<UserInfo?> AddUserDetails(UserInfo userinfo)
        {
            if (_context.UserInfos != null)
            {
                var entity = (await _context.UserInfos.AddAsync(userinfo)).Entity;
                await _context.SaveChangesAsync();
                return entity;
            }
            return null;
        }
        public async Task UpdateUserInfo(UserInfo userinfo)
        {
            _context.Update(userinfo);
            await _context.SaveChangesAsync();
        }

        public UserInfo GetAllInfoByIDNew(Guid userId)
        {
            UserInfo userInfo = _context.Set<UserInfo>().FirstOrDefault(u => u.UserId == userId);
            return userInfo;
        }
    }
}
