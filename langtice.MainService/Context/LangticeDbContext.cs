using Microsoft.EntityFrameworkCore;

namespace langtice_api.Context;

public class LangticeDbContext : DbContext
{
    public LangticeDbContext(DbContextOptions <LangticeDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
}