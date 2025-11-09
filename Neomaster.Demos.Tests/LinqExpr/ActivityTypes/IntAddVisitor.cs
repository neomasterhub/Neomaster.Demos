using System.Linq.Expressions;

namespace Neomaster.Demos.Tests.LinqExpr;

public class IntAddVisitor : ExpressionVisitor
{
  private static readonly Type _intType = typeof(int);

  private bool _modified;

  public override Expression Visit(Expression node)
  {
    if (node.NodeType != ExpressionType.Add
      || node.Type != _intType)
    {
      return node;
    }

    var be = (BinaryExpression)node;
    var lc = be.Left as ConstantExpression;
    var rc = be.Right as ConstantExpression;

    if (lc != null && rc != null)
    {
      _modified = true;
      return Expression.Constant((int)lc.Value + (int)rc.Value);
    }

    if (lc != null && (int)lc.Value == 0)
    {
      _modified = true;
      return be.Right;
    }

    if (rc != null && (int)rc.Value == 0)
    {
      _modified = true;
      return be.Left;
    }

    return node;
  }

  public Expression Visit(Expression node, out bool modified)
  {
    var result = Visit(node);
    modified = _modified;

    return result;
  }
}
