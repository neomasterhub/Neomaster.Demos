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
}
