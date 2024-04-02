using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlazorWebAppCustomAuth.Authentication;

public class UserDbContext : IdentityDbContext<AppUser>
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {

    }


}
