namespace Neomaster.Demos.Tests.LinqExpr;

public class LinqExprDemoDbContextFixture : IDisposable
{
  public LinqExprDemoDbContextFixture()
  {
    DbContext = new LinqExprDemoDbContext();
    DbContext.Database.EnsureDeleted();
    DbContext.Database.EnsureCreated();
    DbContext.Departments.Add(new Department { Name = "D1" });
    DbContext.SaveChanges();
  }

  public LinqExprDemoDbContext DbContext { get; }

  public void Dispose()
  {
    DbContext.Dispose();
  }
}
