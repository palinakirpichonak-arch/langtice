using Microsoft.EntityFrameworkCore;

public class LangticeDbContext : DbContext
{
    public LangticeDbContext(DbContextOptions <LangticeDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
}