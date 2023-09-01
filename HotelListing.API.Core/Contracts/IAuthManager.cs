using HotelListing.API.Core.Models.User;
using Microsoft.AspNetCore.Identity;

namespace HotelListing.API.Core.Contracts
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
