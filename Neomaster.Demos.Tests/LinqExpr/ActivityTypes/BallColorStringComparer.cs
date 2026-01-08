namespace Neomaster.Demos.Tests.LinqExpr;

public class BallColorStringComparer : IComparer<Ball>
{
  public int Compare(Ball x, Ball y)
  {
    return x.Color.ToString().CompareTo(y.Color.ToString());
  }
}
