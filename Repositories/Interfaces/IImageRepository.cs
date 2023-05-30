using LibraryMSv3.Models.DatabaseModels;
using LibraryMSv3.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace LibraryMSv3.Repositories.Interfaces
{
    public interface IImageRepository
    {
        public Task<Image?> GetImage(Guid userId);
        public Task AddUserPhoto(Image userPav);
        public Task DeleteImage(Image katrinti);
    }
}
