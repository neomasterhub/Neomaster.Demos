using System.Linq.Expressions;
using System.Reflection;

namespace Neomaster.Demos.Tests.LinqExpr;

public static class ExpressionExtensions
{
  private static readonly PropertyInfo _debugViewPi = typeof(Expression)
    .GetProperty(
      "DebugView",
      BindingFlags.Instance | BindingFlags.NonPublic);

  public static string DebugView(this Expression expr)
  {
    return _debugViewPi.GetValue(expr).ToString();
  }
}
