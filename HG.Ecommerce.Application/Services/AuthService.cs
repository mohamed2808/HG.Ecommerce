using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using HG.Ecommerce.Application.Abstraction.Models.Dtos.UserDtos;
using HG.Ecommerce.Core.Contracts;
using HG.Ecommerce.Core.Entites;
using HG.Ecommerce.Infrastruction.Presistance.Data.DbContextFile;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly EcommerceDbContext _db;
    private readonly IJwtService _jwtService;

    public AuthService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        EcommerceDbContext db,
        IJwtService jwtService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _db = db;
        _jwtService = jwtService;
    }

    public async Task<IdentityResult> RegisterAsync(RegisterDto dto)
    {
        var user = new ApplicationUser
        {
            UserName = dto.Email,
            Email = dto.Email
        };

        var result = await _userManager.CreateAsync(user, dto.Password);
        return result;
    }

    public async Task<(string accessToken, string refreshToken)> LoginAsync(string usernameOrEmail, string password)
    {
        var user = await _userManager.Users
            .Include(u => u.RefreshTokens)
            .FirstOrDefaultAsync(u => u.UserName == usernameOrEmail || u.Email == usernameOrEmail);

        if (user == null)
            throw new UnauthorizedAccessException("Invalid credentials");

        var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
        if (!result.Succeeded)
            throw new UnauthorizedAccessException("Invalid credentials");

        user.LastLoginTime = DateTime.UtcNow;
        await _userManager.UpdateAsync(user);

        var accessToken = _jwtService.GenerateAccessToken(user);
        var refreshToken = _jwtService.GenerateRefreshToken();

        user.RefreshTokens.Add(refreshToken);
        await _db.SaveChangesAsync();

        return (accessToken, refreshToken.Token);
    }

    public async Task<(string accessToken, string refreshToken)> RefreshTokenAsync(string token)
    {
        var refreshToken = await _db.RefreshTokens 
            .Include(rt => rt.User) 
            .FirstOrDefaultAsync(rt => rt.Token == token && !rt.IsRevoked);

        if (refreshToken == null || refreshToken.Expires < DateTime.UtcNow)
            throw new UnauthorizedAccessException("Invalid or expired refresh token");

        var user = refreshToken.User!;
        var newAccessToken = _jwtService.GenerateAccessToken(user);
        var newRefreshToken = _jwtService.GenerateRefreshToken();

        refreshToken.IsRevoked = true;
        user.RefreshTokens.Add(newRefreshToken);

        await _db.SaveChangesAsync();

        return (newAccessToken, newRefreshToken.Token);
    }
}
