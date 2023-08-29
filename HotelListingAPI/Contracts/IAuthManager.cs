using HotelListingAPI.Models.User;
using Microsoft.AspNetCore.Identity;

namespace HotelListingAPI.Contracts
{
    public interface IAuthManager
    {
        Task<IEnumerable<IdentityError>> Reqister(ApiUserDto userDto);

        Task<bool> Login(LoginDto userDto);
    }
}
