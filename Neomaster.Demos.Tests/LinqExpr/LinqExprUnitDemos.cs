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

  [Fact]
  public void Tree_Create_LeftViaMakeMemberAccess()
  {
    var leftPar = Expression.Parameter(typeof(Claim));
    var leftPi = typeof(Claim).GetProperty(nameof(Claim.Value));
    var leftOperand = Expression.MakeMemberAccess(leftPar, leftPi);

    var rightOperand = Expression.Constant("1");

    Func<Expression, Expression, BinaryExpression> binaryOperator = Expression.Equal;

    var body = binaryOperator(leftOperand, rightOperand);

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

  [Fact]
  public void Tree_Create_LeftViaProperty()
  {
    var leftPar = Expression.Parameter(typeof(Claim));
    var leftProp = Expression.Property(leftPar, nameof(Claim.Value));

    var rightOperand = Expression.Constant("1");

    Func<Expression, Expression, BinaryExpression> binaryOperator = Expression.Equal;

    var body = binaryOperator(leftProp, rightOperand);

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
