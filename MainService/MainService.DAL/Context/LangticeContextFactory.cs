using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MainService.DAL.Context;

    public class LangticeContextFactory : IDesignTimeDbContextFactory<PostgreLangticeContext>
    {
        public PostgreLangticeContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PostgreLangticeContext>();
            var host = Environment.GetEnvironmentVariable("DB_HOST");
            var port = Environment.GetEnvironmentVariable("DB_PORT");
            var db = Environment.GetEnvironmentVariable("DB_NAME");
            var username = Environment.GetEnvironmentVariable("DB_USER");
            var password = Environment.GetEnvironmentVariable("DB_PASS");

            var connectionString = $"Host={host};Port={port};Database={db};Username={username};Password={password}";
            optionsBuilder.UseNpgsql(connectionString);
            
            return new PostgreLangticeContext(optionsBuilder.Options);
        }
    }

