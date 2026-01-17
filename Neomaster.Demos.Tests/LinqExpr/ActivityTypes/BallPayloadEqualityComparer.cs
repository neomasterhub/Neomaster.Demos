using System.Diagnostics.CodeAnalysis;

namespace Neomaster.Demos.Tests.LinqExpr;

public class BallPayloadEqualityComparer : IEqualityComparer<Ball>
{
  public bool Equals(Ball x, Ball y)
  {
    return x.Color == y.Color;
  }

  public int GetHashCode([DisallowNull] Ball obj)
  {
    return 1;
  }
}
