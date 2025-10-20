using MainService.DAL.Features.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MainService.DAL.Configurations.PostgreSql;

public class UserConfiguration :  IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder
            .HasIndex(u => u.Username).IsUnique();
        builder
            .HasIndex(u => u.Email).IsUnique();
        
        builder.HasData(
            new User
            {
                Id = Guid.Parse("a1b2c3d4-e5f6-4a7b-8c9d-1234567890ab"),
                Username = "testuser",
                Email = "testuser@example.com",
                PasswordHash = "AQAAAAIAAYagAAAAEFakeHashedPassword1234567890", // placeholder hash
                AvatarUrl = null,
                Status = true
            }
        );
    }
}