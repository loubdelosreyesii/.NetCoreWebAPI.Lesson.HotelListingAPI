using HotelListingAPI.Models.User;
using Microsoft.AspNetCore.Identity;

namespace HotelListingAPI.Contracts
{
    public interface IAuthManager
    {
        Task<IEnumerable<IdentityError>> Reqister(ApiUserDto userDto);
        Task<IEnumerable<IdentityError>> ReqisterAdminOnly(ApiUserDto userAdminDto, LoginDto userLoginDto);
        Task<AuthResponseDto> Login(LoginDto userDto);

        Task<string> CreateRefreshToken();
        Task<AuthResponseDto> VerifyRefreshToken(AuthResponseDto request);
    }
}
