using MainService.DAL.Features.Words.Models;
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
        
        var englishLangId = Guid.Parse("8dc05007-6ed9-406a-9eeb-fbbf748283e2");
        var germanLangId  = Guid.Parse("8e5a2463-e8d1-427a-bd84-9386e073999f");

        builder.HasData(
            new Word { Id = Guid.Parse("a1b2c3d4-e5f6-4789-abc1-123456789001"), Text = "Hello",      LanguageId = englishLangId },
            new Word { Id = Guid.Parse("a1b2c3d4-e5f6-4789-abc1-123456789002"), Text = "Goodbye",   LanguageId = englishLangId },
            new Word { Id = Guid.Parse("a1b2c3d4-e5f6-4789-abc1-123456789003"), Text = "Thank you", LanguageId = englishLangId },
            new Word { Id = Guid.Parse("a1b2c3d4-e5f6-4789-abc1-123456789004"), Text = "Please",    LanguageId = englishLangId },
            new Word { Id = Guid.Parse("a1b2c3d4-e5f6-4789-abc1-123456789005"), Text = "Water",     LanguageId = englishLangId },
            
            new Word { Id = Guid.Parse("a1b2c3d4-e5f6-4789-abc1-123456789006"), Text = "Hallo",           LanguageId = germanLangId },
            new Word { Id = Guid.Parse("a1b2c3d4-e5f6-4789-abc1-123456789007"), Text = "Auf Wiedersehen", LanguageId = germanLangId },
            new Word { Id = Guid.Parse("a1b2c3d4-e5f6-4789-abc1-123456789008"), Text = "Danke",           LanguageId = germanLangId },
            new Word { Id = Guid.Parse("a1b2c3d4-e5f6-4789-abc1-123456789009"), Text = "Bitte",           LanguageId = germanLangId },
            new Word { Id = Guid.Parse("a1b2c3d4-e5f6-4789-abc1-123456789010"), Text = "Wasser",          LanguageId = germanLangId }
        );
    }
}