using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neomaster.Demos.Tests.LinqExpr;

public class User
{
  public string Id { get; set; }
  public string Email { get; set; }

  public User()
  {
  }

  public User(string email)
  {
    Id = Guid.NewGuid().ToString();
    Email = email;
  }
}
