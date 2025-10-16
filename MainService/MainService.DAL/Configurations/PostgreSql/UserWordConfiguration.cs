using MainService.DAL.Features.UserWord;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MainService.DAL.Configurations.PostgreSql;

public class UserWordConfiguration : IEntityTypeConfiguration<UserWord>
{
    public void Configure(EntityTypeBuilder<UserWord> builder)
    {
        builder.HasKey(uw => new { uw.UserId, uw.WordId });

        builder.HasOne(uw => uw.User)
            .WithMany(u => u.UserWords)
            .HasForeignKey(uw => uw.UserId);

        builder.HasOne(uw => uw.Word)
            .WithMany(w => w.UserWords)
            .HasForeignKey(uw => uw.WordId);

        var userId1 = Guid.Parse("a1b2c3d4-e5f6-4a7b-8c9d-1234567890ab");
        builder.HasData(
            new UserWord
            {
                UserId = userId1,
                WordId = Guid.Parse("a1b2c3d4-e5f6-4789-abc1-123456789001"),
                AddedAt = new DateTime(2024, 01, 10, 0, 0, 0, DateTimeKind.Utc)
            },
            new UserWord
            {
                UserId = userId1,
                WordId = Guid.Parse("a1b2c3d4-e5f6-4789-abc1-123456789002"),
                AddedAt = new DateTime(2024, 01, 11, 0, 0, 0, DateTimeKind.Utc)
            },
            new UserWord
            {
                UserId = userId1,
                WordId = Guid.Parse("a1b2c3d4-e5f6-4789-abc1-123456789003"),
                AddedAt = new DateTime(2024, 01, 12, 0, 0, 0, DateTimeKind.Utc)
            },
            new UserWord
            {
                UserId = userId1,
                WordId = Guid.Parse("a1b2c3d4-e5f6-4789-abc1-123456789006"),
                AddedAt = new DateTime(2024, 01, 13, 0, 0, 0, DateTimeKind.Utc)
            },
            new UserWord
            {
                UserId = userId1,
                WordId = Guid.Parse("a1b2c3d4-e5f6-4789-abc1-123456789007"),
                AddedAt = new DateTime(2024, 01, 14, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}