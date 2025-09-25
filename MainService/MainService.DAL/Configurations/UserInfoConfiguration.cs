using MainService.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MainService.DAL.Configurations;

public class UserInfoConfiguration :  IEntityTypeConfiguration<UserInfo>
{
    public void Configure(EntityTypeBuilder<UserInfo> builder)
    {
        // builder.HasKey(ui => ui.Id);
        //
        // builder.Property(ui => ui.AddedAt)
        //     .IsRequired();
        //
        // builder.Property(ui => ui.Period);
        //
        // builder.Property(ui => ui.WordsLearned)
        //     .HasDefaultValue(0);
        //
        // builder.Property(ui => ui.TestsFinished)
        //     .HasDefaultValue(0);
        //
        // builder.Property(ui => ui.MistakesMade)
        //     .HasDefaultValue(0);
        //
        // builder.Property(ui => ui.StreakLength)
        //     .HasDefaultValue(0);
        //
        // builder.HasOne(ui => ui.User)
        //     .WithOne() 
        //     .HasForeignKey<UserInfo>(ui => ui.UserId)
        //     .OnDelete(DeleteBehavior.Cascade);
    }
}