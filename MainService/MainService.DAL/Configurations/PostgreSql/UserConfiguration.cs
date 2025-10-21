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
    }
}