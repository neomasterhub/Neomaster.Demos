namespace Neomaster.Demos.Tests.LinqExpr;

public class Ball : IComparable<Ball>
{
  public ConsoleColor Color { get; set; }

  public int CompareTo(Ball other)
  {
    if (Color > other.Color)
    {
      return 1;
    }

    if (Color < other.Color)
    {
      return -1;
    }

    return 0;
  }
}
