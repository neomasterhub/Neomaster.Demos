using Microsoft.EntityFrameworkCore;

namespace Neomaster.Demos.Tests.LinqExpr;

public class LinqExprDemoDbContext : DemoDbContext
{
  public DbSet<Department> Departments { get; set; }
}
