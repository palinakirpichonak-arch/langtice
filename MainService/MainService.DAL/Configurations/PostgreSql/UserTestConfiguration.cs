using MainService.DAL.Features.UserTest;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MainService.DAL.Configurations.PostgreSql;

public class UserTestConfiguration :  IEntityTypeConfiguration<UserTest>
{
    public void Configure(EntityTypeBuilder<UserTest> modelBuilder)
    {
        modelBuilder.HasKey(ut => ut.Id);

        modelBuilder.HasOne(ut => ut.User)
            .WithMany(u => u.UserTests) 
            .HasForeignKey(ut => ut.UserId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}