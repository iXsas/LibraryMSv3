using LibraryMSv3.Data;
using LibraryMSv3.Models.DatabaseModels;
using LibraryMSv3.Models.DTO;
using LibraryMSv3.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryMSv3.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly ApplicationDbContext _context;
        public ImageRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Image?> GetImage(Guid userId)
        {
            if (_context.Images != null)
            {
                return await _context.Images.FirstOrDefaultAsync(up => up.UserId == userId);
            }
            return null;
        }
        public async Task AddUserPhoto(Image userPav)
        {
            if (_context.Images != null)
            {
                await _context.Images.AddAsync(userPav);
                await _context.SaveChangesAsync();
            }
        }
        public async Task DeleteImage(Image katrinti)
        {
            if (_context.Images != null)
            {
                _context.Images.Remove(katrinti);
                await _context.SaveChangesAsync();
            }
        }
    }
}
