using System.Diagnostics.CodeAnalysis;

namespace Neomaster.Demos.Tests.LinqExpr;

public class BallColorEqualityComparer : IEqualityComparer<Ball>
{
  public bool Equals(Ball x, Ball y)
  {
    return x.Color == y.Color;
  }

  public int GetHashCode([DisallowNull] Ball obj)
  {
    return (int)obj.Color;
  }
}
