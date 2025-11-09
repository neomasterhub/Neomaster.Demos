using System.Linq.Expressions;

namespace Neomaster.Demos.Tests.LinqExpr;

public class IntAddVisitor : ExpressionVisitor
{
  private static readonly Type _intType = typeof(int);

  public override Expression Visit(Expression node)
  {
    if (node.NodeType != ExpressionType.Add
      && node.Type != _intType)
    {
      return node;
    }

    var be = (BinaryExpression)node;
    var lc = be.Left as ConstantExpression;
    var rc = be.Right as ConstantExpression;

    if (lc != null && rc != null)
    {
      return Expression.Constant((int)lc.Value + (int)rc.Value);
    }

    if (lc != null && (int)lc.Value == 0)
    {
      return be.Right;
    }

    if (rc != null && (int)rc.Value == 0)
    {
      return be.Left;
    }

    return node;
  }
}
