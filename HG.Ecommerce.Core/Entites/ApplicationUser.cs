using Microsoft.AspNetCore.Identity;
namespace HG.Ecommerce.Core.Entites
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime? LastLoginTime { get; set; }

        public List<RefreshToken> RefreshTokens { get; set; } = new();
    }
}
