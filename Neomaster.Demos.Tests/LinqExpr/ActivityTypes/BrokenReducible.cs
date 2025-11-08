using System.Linq.Expressions;

namespace Neomaster.Demos.Tests.LinqExpr;

public class BrokenReducible(BrokenReducible.ReducedResult reducedResult)
  : Expression
{
  private readonly ReducedResult _reducedResult = reducedResult;

  public enum ReducedResult
  {
    Null,
    This,
  }

  public override bool CanReduce => true;
  public override Expression Reduce()
  {
    return _reducedResult switch
    {
      ReducedResult.Null => null,
      ReducedResult.This => this,
    };
  }
}
