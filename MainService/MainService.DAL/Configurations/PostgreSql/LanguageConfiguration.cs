using MainService.DAL.Features.Languages.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MainService.DAL.Configurations.PostgreSql;

public class LanguageConfiguration : IEntityTypeConfiguration<Language>
{
    public void Configure(EntityTypeBuilder<Language> builder)
    {
        builder.HasKey(t => t.Id);
        
        builder.HasIndex(l => l.Name);
        
        builder.HasData(
            new Language { Id = Guid.Parse("8dc05007-6ed9-406a-9eeb-fbbf748283e2"), Name = "English" },
            new Language { Id = Guid.Parse("8e5a2463-e8d1-427a-bd84-9386e073999f"), Name = "German" },
            new Language { Id = Guid.Parse("419ce969-51ab-41c9-9d2f-ae0f007d3b2d"), Name = "Russian" },
            new Language { Id = Guid.Parse("5715abc0-de4e-4a5c-bd9c-4edcbade3e09"), Name = "French" }
        );
    }
}