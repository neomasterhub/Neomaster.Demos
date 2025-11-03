using System.Linq.Expressions;
using System.Security.Claims;
using Xunit;

namespace Neomaster.Demos.Tests.LinqExpr;

public class LinqExprUnitDemos()
{
  [Fact]
  public void TreeStructure_View()
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
  public void TreeStructure_Create_LeftViaMakeMemberAccess()
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
  public void TreeStructure_Create_LeftViaProperty()
  {
    var leftPar = Expression.Parameter(typeof(Claim));
    var leftOperand = Expression.Property(leftPar, nameof(Claim.Value));

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
  public void TreeStructure_Create_3Levels()
  {
    var leftPar = Expression.Parameter(typeof(Claim));
    var leftOperand = Expression.Property(leftPar, nameof(Claim.Value));
    var rightOperand = Expression.Constant("1");
    var body = Expression.Equal(
      Expression.Constant(true),
      Expression.Equal(leftOperand, rightOperand));
    const string expected =
      """
            Constant
           /
      Equal      MemberAccess
           \    /
            Equal__Constant
      """;

    var n1 = body.NodeType;
    var n1L = body.Left.NodeType;
    var n1R = body.Right.NodeType;
    var n2 = (BinaryExpression)body.Right;
    var n2L = n2.Left.NodeType;
    var n2R = n2.Right.NodeType;

    var actual =
      $"""
            {n1L}
           /
      {n1}      {n2L}
           \    /
            {n1R}__{n2R}
      """;

    Assert.Equal(expected, actual);
  }
}
