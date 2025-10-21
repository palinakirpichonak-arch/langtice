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
    }
}