using MainService.DAL.Features.Words;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MainService.DAL.Configurations.PostgreSql;

public class WordConfiguration  : IEntityTypeConfiguration<Word>
{
    public void Configure(EntityTypeBuilder<Word> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder
            .HasOne(w => w.Language)
            .WithMany()
            .HasForeignKey(w => w.LanguageId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasIndex(w => w.Text)
            .IsUnique();
    }
}