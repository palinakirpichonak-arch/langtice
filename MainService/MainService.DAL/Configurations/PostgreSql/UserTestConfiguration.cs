using MainService.DAL.Features.UserTest;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MainService.DAL.Configurations.PostgreSql;

public class UserTestConfiguration :  IEntityTypeConfiguration<UserTest>
{
    public void Configure(EntityTypeBuilder<UserTest> modelBuilder)
    {
        modelBuilder.HasKey(ut => ut.Id);

        modelBuilder.Property(ut => ut.UserId)
            .IsRequired();

    }
}