using System.Linq.Expressions;

namespace Neomaster.Demos.Tests.LinqExpr;

public class ReducibleIntAdd : Expression
{
  private static readonly Type _intType = typeof(int);

  private readonly Expression _left;
  private readonly Expression _right;

  private Expression _reducedResult;

  public ReducibleIntAdd(Expression left, Expression right)
  {
    if (left.Type != _intType)
    {
      throw new InvalidOperationException("Left expression must be of type int.");
    }

    if (right.Type != _intType)
    {
      throw new InvalidOperationException("Right expression must be of type int.");
    }

    _left = left;
    _right = right;
  }

  public Expression Left => _left;
  public Expression Right => _right;

  public override Type Type => _intType;
  public override ExpressionType NodeType => ExpressionType.Extension;
  public override bool CanReduce
  {
    get
    {
      var lc = _left as ConstantExpression;
      var rc = _right as ConstantExpression;

      if (lc != null && rc != null)
      {
        _reducedResult = Constant((int)lc.Value + (int)rc.Value);
        return true;
      }

      if (lc != null && (int)lc.Value == 0)
      {
        _reducedResult = _right;
        return true;
      }

      if (rc != null && (int)rc.Value == 0)
      {
        _reducedResult = _left;
        return true;
      }

      return false;
    }
  }

  public override Expression Reduce()
  {
    return CanReduce
      ? _reducedResult
      : Add(Left, Right);
  }
}
