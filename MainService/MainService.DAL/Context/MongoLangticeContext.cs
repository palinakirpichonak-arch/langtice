using MainService.DAL.Features.Courses.Models;
using MainService.DAL.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;

namespace MainService.DAL.Context;

public class MongoLangticeContext(DbContextOptions<MongoLangticeContext> options) : DbContext(options)
{
    public DbSet<Test>  Tests { get; set; }
    public DbSet<Question> Questions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Test>().ToCollection("tests");
    }
}