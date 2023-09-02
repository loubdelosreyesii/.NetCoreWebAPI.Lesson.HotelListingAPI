﻿using HotelListing.API.Core.Contracts;
using HotelListing.API.Core.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;

namespace HotelListing.API.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthManager _authManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IAuthManager authManager, ILogger<AccountController> logger)
        {
            this._authManager = authManager;
            this._logger = logger;
        }

        // location wille be : POST: api/Account/register 
        [HttpPost]
        [Route("register")]//naming the route
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //FromBody : param should be expected in the body parameters only.
        public async Task<ActionResult> Register([FromBody] ApiUserDto apiUserDto) 
        {
            _logger.LogInformation($"Registration Attempt for {apiUserDto.Email}");

            var errors = await _authManager.Reqister(apiUserDto);

            if (errors.Any())
            { 
                foreach (var error in errors)
                {
                    ModelState.AddModelError(error.Code,error.Description);
                }

                return BadRequest(ModelState);
            }
            return Ok();
        }

        // location wille be : POST: api/Account/login 
        [HttpPost]
        [Route("login")]//naming the route
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //FromBody : param should be expected in the body parameters only.
        public async Task<ActionResult> Login([FromBody] LoginDto loginDto)
        {
            _logger.LogInformation($"Login Attempt for {loginDto.Email}");

            var authResponse = await _authManager.Login(loginDto);


            if (authResponse == null)
            { 
                return Unauthorized(); //Status Code 401
            }

            return Ok(authResponse);
        }

        // location wille be : POST: api/Account/refresh 
        [HttpPost]
        [Route("refreshtoken")]//naming the route
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //FromBody : param should be expected in the body parameters only.
        public async Task<ActionResult> RefreshToken([FromBody] AuthResponseDto request)
        {
            var authResponse = await _authManager.VerifyRefreshToken(request);

            if (authResponse == null)
            {
                return Unauthorized(); //Status Code 401
            }

            return Ok(authResponse);
        }

    }
}
