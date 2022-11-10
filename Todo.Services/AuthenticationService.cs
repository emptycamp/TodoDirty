using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Todo.Core.Models;
using Todo.Services.Exceptions;
using Todo.Services.Interfaces;
using Todo.Shared.Requests;
using Todo.Shared.Responses;

namespace Todo.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IJwtService _jwtService;

        public AuthenticationService(IMapper mapper, UserManager<User> userManager, IJwtService jwtService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _jwtService = jwtService;
        }

        public async Task<IdentityResult> CreateUserAsync(CreateUserRequest userDto)
        {
            var user = _mapper.Map<User>(userDto);
            var result = await _userManager.CreateAsync(user, userDto.Password);

            return result;
        }

        public async Task<JwtTokenResponse> AuthenticateUserAsync(AuthenticateUserRequest authenticateUserDto)
        {
            var user = await _userManager.FindByEmailAsync(authenticateUserDto.Email);
            var userIsValid = user != null && await _userManager.CheckPasswordAsync(user, authenticateUserDto.Password);

            if (userIsValid)
            {
                return new JwtTokenResponse
                {
                    Token = await _jwtService.CreateTokenAsync(user!)
                };
            }

            throw new AuthenticationException();
        }
    }
}
