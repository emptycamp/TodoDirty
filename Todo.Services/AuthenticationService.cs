using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Todo.Core.Exceptions;
using Todo.Core.Models;
using Todo.Services.Exceptions;
using Todo.Services.Interfaces;
using Todo.Shared.Requests;
using Todo.Shared.Responses;
using Todo.Shared.Responses.Errors;
using FieldError = Todo.Core.Exceptions.FieldError;
using ValidationException = Todo.Core.Exceptions.ValidationException;

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

            if (!result.Succeeded)
            {
                throw new ValidationException(result.Errors.Select(x => new FieldError
                {
                    Field = x.Code,
                    Messages = new List<string> { x.Description }
                }));
            }

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
