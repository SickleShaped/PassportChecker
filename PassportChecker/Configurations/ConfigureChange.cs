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
        builder.HasIndex(k => k.Date);
        builder.HasIndex(c => new { c.Series, c.Number });
    }
}
