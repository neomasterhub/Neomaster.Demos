namespace Neomaster.Demos.Tests.LinqExpr;

public class BallStringComparer : IComparer<Ball>
{
  public int Compare(Ball x, Ball y)
  {
    return x.ToString().CompareTo(y.ToString());
  }
}
