﻿using AutoMapper;
using HotelListingAPI.Contracts;
using HotelListingAPI.Data;
using HotelListingAPI.Models.User;
using Microsoft.AspNetCore.Identity;

namespace HotelListingAPI.Repositories
{
    public class AuthManager : IAuthManager
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApiUser> _userManager;

        public AuthManager(IMapper mapper, UserManager<ApiUser> userManager)
        {
            this._mapper = mapper;
            this._userManager = userManager;
        }
        public async Task<IEnumerable<IdentityError>> Reqister(ApiUserDto userDto)
        {
            var user  = _mapper.Map<ApiUser>(userDto);
            user.UserName = userDto.Email;

            var result = await _userManager.CreateAsync(user,userDto.Password);

            if(result.Succeeded) 
            {
                await _userManager.AddToRoleAsync(user, "User");
            }

            return result.Errors;


        }
    }
}
