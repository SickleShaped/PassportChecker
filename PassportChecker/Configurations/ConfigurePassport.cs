using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PassportChecker.Models.DbModels;

namespace PassportChecker.Configurations;

public class ConfigurePassport:IEntityTypeConfiguration<PassportModel>
{
    public void Configure(EntityTypeBuilder<PassportModel> builder)
    {
        builder.ToTable("Passports");
        builder.HasKey(c => new { c.Series, c.Number });
        builder.HasIndex(c => new { c.Series, c.Number });
    }
}
