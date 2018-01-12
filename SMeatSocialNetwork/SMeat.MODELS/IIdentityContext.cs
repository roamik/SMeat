using Microsoft.EntityFrameworkCore;
using SMeat.MODELS.DTO;
using SMeat.MODELS.Entities;

namespace SMeat.MODELS
{
    public interface IIdentityContext
    {
        DbSet<User> Users { get; set; }
        DbSet<UserToken> UserTokens { get; set; }
        DbSet<UserLogin> UserLogins { get; set; }
        DbSet<UserRoleClaim> RoleClaims { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<UserRole> UserRoles { get; set; }
    }
}