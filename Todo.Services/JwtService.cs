using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Todo.Core.Interfaces;
using Todo.Core.Models;
using Todo.Server.Exceptions;
using Todo.Services.Interfaces;
using Todo.Shared.Store;
using Todo.Shared.Requests.Auth;

namespace Todo.Services
{
    public class JwtService : IJwtService
    {
        private readonly UserManager<User> _userManager;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly ITokenRepository _tokenRepository;
        private readonly JwtConfig _jwtConfig;

        public JwtService(UserManager<User> userManager, 
            IOptions<JwtConfig> jwtOptions, 
            TokenValidationParameters tokenValidationParameters, 
            ITokenRepository tokenRepository)
        {
            _userManager = userManager;
            _jwtConfig = jwtOptions.Value;
            _tokenValidationParameters = tokenValidationParameters;
            _tokenRepository = tokenRepository;
        }

        public async Task<string> GenerateAccessTokenAsync(User user)
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims(user);
            var token = GenerateToken(signingCredentials, claims);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<string> GenerateRefreshToken(User user)
        {
            var refreshToken = new RefreshToken
            {
                UserId = user.Id,
                ExpiryDate = DateTime.Now.AddMinutes(_jwtConfig.RefreshExpiresIn),
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                User = user
            };

            await _tokenRepository.AddRefreshToken(refreshToken);

            return refreshToken.Token;
        }

        public async Task<string> RefreshAccessTokenAsync(RefreshTokenRequest refreshTokenDto)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            try
            {
                jwtTokenHandler.ValidateToken(refreshTokenDto.Token, _tokenValidationParameters, out var validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = Guid.Parse(jwtToken.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);

                var refreshToken = await _tokenRepository.FindByRefreshToken(refreshTokenDto.RefreshToken);
                if (refreshToken.UserId != userId)
                {
                    throw new UnauthorizedException();
                }

                return await GenerateAccessTokenAsync(refreshToken.User);
            }
            catch (Exception)
            {
                throw new UnauthorizedException();
            }
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_jwtConfig.Secret);
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims(User user)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            return claims;
        }

        private JwtSecurityToken GenerateToken(SigningCredentials signingCredentials, IEnumerable<Claim> claims)
        {
            var token = new JwtSecurityToken
            (
                issuer: _jwtConfig.ValidIssuer,
                audience: _jwtConfig.ValidAudience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtConfig.ExpiresIn),
                signingCredentials: signingCredentials
            );
            return token;
        }
    }
}
