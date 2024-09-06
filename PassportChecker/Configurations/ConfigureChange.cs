using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PassportChecker.Models.DbModels;

namespace PassportChecker.Configurations;

public class ConfigureChange:IEntityTypeConfiguration<ChangeModel>
{
    public void Configure(EntityTypeBuilder<ChangeModel> builder)
    {
        builder.ToTable("Changes");
        builder.HasKey(k => k.Id);
        builder.HasIndex(k => k.Id);


        /*builder
            .HasOne(p=>p.Passport)
            .WithMany(p=>p.Changes)
            .HasForeignKey(p=>p.Id)
            .OnDelete(DeleteBehavior.Cascade);*/
    }
}
