using Microsoft.EntityFrameworkCore;
using Sample.API.Models;

namespace Sample.API.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    // DbSets
    public DbSet<CertificateCategory> CertificateCategories { get; set; } = default!;
}