using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SMeat.MODELS.DTO;
using SMeat.MODELS.Entities;

namespace SMeat.MODELS
{
    public abstract class ApplicatonBaseContext : IdentityDbContext<User, Role, string, UserClaim, UserRole, UserLogin, UserRoleClaim, UserToken>
    {
        
    }
}
