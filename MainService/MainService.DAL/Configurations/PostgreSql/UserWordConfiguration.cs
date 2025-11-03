using MainService.DAL.Features.UserWord;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MainService.DAL.Configurations.PostgreSql;

public class UserWordConfiguration : IEntityTypeConfiguration<UserWord>
{
    public void Configure(EntityTypeBuilder<UserWord> builder)
    {
        builder.HasKey(uw => new { uw.UserId, uw.WordId });

        builder
            .Property(uw => uw.UserId)
            .IsRequired();

        builder.HasOne(uw => uw.Word)
            .WithMany(w => w.UserWords)
            .HasForeignKey(uw => uw.WordId);
    }
}