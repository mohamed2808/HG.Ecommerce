using HG.Ecommerce.Application.Abstraction.Models.Dtos.UserDtos;
using Microsoft.AspNetCore.Identity;

namespace HG.Ecommerce.Core.Contracts
{
    public interface IAuthService
    {
        Task<(string accessToken, string refreshToken)> LoginAsync(string usernameOrEmail, string password);
        Task<IdentityResult> RegisterAsync(RegisterDto dto);
        Task<(string accessToken, string refreshToken)> RefreshTokenAsync(string token);
    }
}
