using HG.Ecommerce.Core.Entites;

namespace HG.Ecommerce.Core.Contracts
{
    public interface IJwtService
    {
        string GenerateAccessToken(ApplicationUser user);
        RefreshToken GenerateRefreshToken();
    }
}
