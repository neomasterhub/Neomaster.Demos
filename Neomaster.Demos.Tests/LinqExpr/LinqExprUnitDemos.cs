using System.Linq.Expressions;
using System.Security.Claims;
using Xunit;

namespace Neomaster.Demos.Tests.LinqExpr;

public class LinqExprUnitDemos()
{
  [Fact]
  public void Tree_View()
  {
    var tree = (Expression<Func<Claim, bool>>)(c => c.Value == "1");
    var body = (BinaryExpression)tree.Body;
    var root = body.NodeType;
    var left = body.Left.NodeType;
    var right = body.Right.NodeType;
    const string expected =
      """
            MemberAccess
           /
      Equal
           \
            Constant
      """;

    var actual =
      $"""
            {left}
           /
      {root}
           \
            {right}
      """;

    Assert.Equal(expected, actual);
  }
}
