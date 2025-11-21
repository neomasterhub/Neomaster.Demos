using Microsoft.EntityFrameworkCore;

namespace Neomaster.Demos.Tests;

public abstract class DemoDbContext : DbContext
{
  protected override void OnConfiguring(DbContextOptionsBuilder options)
  {
    options.UseNpgsql("Host=localhost;Port=5432;Database=demo;Username=postgres;Password=postgres");
  }
}
