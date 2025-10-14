using MainService.DAL.Features.Courses.Models;
using MainService.DAL.Features.Lessons;
using MainService.DAL.Features.Users.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MainService.DAL.Configurations;

public class UserTestConfiguration :  IEntityTypeConfiguration<UserTest>
{
    public void Configure(EntityTypeBuilder<UserTest> modelBuilder)
    {
        modelBuilder.HasKey(ut => ut.Id);

        modelBuilder.HasOne(ut => ut.User)
            .WithMany(u => u.UserTests) // make sure User entity has ICollection<UserTest> UserTests
            .HasForeignKey(ut => ut.UserId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}