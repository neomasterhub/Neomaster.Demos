namespace Neomaster.Demos.Tests.LinqExpr;

public class User
{
  public User()
  {
  }

  public User(string email)
  {
    Id = Guid.NewGuid().ToString();
    Email = email;
  }

  public string Id { get; set; }
  public string Email { get; set; }
  public Department Department { get; set; }
  public Department DepartmentNull { get; set; }
  public Department DepartmentDefault { get; set; } = new();
}
