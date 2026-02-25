using MainService.DAL.Models.UserStreakModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MainService.DAL.Configurations.PostgreSql;

public class UserStreakConfiguration : IEntityTypeConfiguration<UserStreak>
{
    public void Configure(EntityTypeBuilder<UserStreak> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.CurrentStreakDays)
            .IsRequired();

        builder.Property(x => x.LastActiveDate);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .IsRequired();

        builder.Property(x => x.LastMorningNotificationAt);
        builder.Property(x => x.LastEveningNotificationAt);

        builder.HasIndex(x => x.UserId)
            .IsUnique();
    }
}