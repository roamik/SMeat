using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SMeat.MODELS.Models;

namespace SMeat.MODELS
{
    public abstract class ApplicatonBaseContext : IdentityDbContext<User, Role, string, UserClaim, UserRole, UserLogin, UserRoleClaim, UserToken>
    {
        
    }
}
