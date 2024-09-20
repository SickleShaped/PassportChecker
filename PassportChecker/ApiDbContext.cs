using Microsoft.EntityFrameworkCore;
using PassportChecker.Models.DbModels;
using System.Net;
using System.Reflection;

namespace PassportChecker;

public class ApiDbContext:DbContext
{

    public ApiDbContext() => Database.EnsureCreated();

    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { Database.SetCommandTimeout(150000); }

    public DbSet<PassportModel> Passports { get; set; } = null!;

    public DbSet<ChangeModel> Changes { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
