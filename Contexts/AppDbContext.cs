using Microsoft.EntityFrameworkCore;

namespace CodeSimits.Contexts;

public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
    {
        
    }
}
