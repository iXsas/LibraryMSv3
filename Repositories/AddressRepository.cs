using LibraryMSv3.Data;
using LibraryMSv3.Models.DatabaseModels;
using LibraryMSv3.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryMSv3.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly ApplicationDbContext _context;
        public AddressRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Address?> GetUserAddressByUserID(Guid userId)
        {
            if (_context.Addresses != null)
            {
                return await _context.Addresses.FirstOrDefaultAsync(ua => ua.UserId == userId);
            }
            return null;
        }
        public async Task AddAddress(Address userAddress)
        {
            if (_context.Addresses != null)
            {
                await _context.Addresses.AddAsync(userAddress);
                await _context.SaveChangesAsync();
            }
        }
        public async Task UpdateAddress(Address newAdress)
        {
            if (_context.Addresses != null)
            {
                _context.Update(newAdress);
                await _context.SaveChangesAsync();
            }
        }
        public Address _GetUserAddressByUserID(Guid userId)
        {
            if (_context.Addresses != null)
            {
                return _context.Addresses.FirstOrDefault(ua => ua.UserId == userId);
            }
            return null;
        }
    }
}
