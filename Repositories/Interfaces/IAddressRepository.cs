using LibraryMSv3.Models.DatabaseModels;

namespace LibraryMSv3.Repositories.Interfaces
{
    public interface IAddressRepository
    {
        public Task<Address?> GetUserAddressByUserID(Guid userId);
        public Task AddAddress(Address userAddress);
        public Task UpdateAddress(Address newAdress);
        public Address _GetUserAddressByUserID(Guid userId);

    }
}
