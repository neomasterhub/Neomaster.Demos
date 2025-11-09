using System.Linq.Expressions;
using System.Reflection;
using System.Security.Claims;
using Xunit;
using static Neomaster.Demos.Tests.LinqExpr.BrokenReducible;

namespace Neomaster.Demos.Tests.LinqExpr;

public class LinqExprUnitDemos(ITestOutputHelper output)
{
  private delegate void ActionRef<T>(ref T arg);

  [Fact]
  public void TreeStructure_View()
  {
    var tree = (Expression<Func<Claim, bool>>)(c => c.Value == "1");
    var body = (BinaryExpression)tree.Body;
    var root = body.NodeType;
    var left = body.Left.NodeType;
    var right = body.Right.NodeType;
    const string expected =
      """
            MemberAccess
           /
      Equal
           \
            Constant
      """;

    var actual =
      $"""
            {left}
           /
      {root}
           \
            {right}
      """;

    Assert.Equal(expected, actual);
  }

  [Fact]
  public void TreeStructure_Create_LeftViaMakeMemberAccess()
  {
    var leftPar = Expression.Parameter(typeof(Claim));
    var leftPi = typeof(Claim).GetProperty(nameof(Claim.Value));
    var leftOperand = Expression.MakeMemberAccess(leftPar, leftPi);

    var rightOperand = Expression.Constant("1");

    Func<Expression, Expression, BinaryExpression> binaryOperator = Expression.Equal;

    var body = binaryOperator(leftOperand, rightOperand);

    var root = body.NodeType;
    var left = body.Left.NodeType;
    var right = body.Right.NodeType;
    const string expected =
      """
            MemberAccess
           /
      Equal
           \
            Constant
      """;

    var actual =
      $"""
            {left}
           /
      {root}
           \
            {right}
      """;

    Assert.Equal(expected, actual);
  }

  [Fact]
  public void TreeStructure_Create_LeftViaProperty()
  {
    var leftPar = Expression.Parameter(typeof(Claim));
    var leftOperand = Expression.Property(leftPar, nameof(Claim.Value));

    var rightOperand = Expression.Constant("1");

    Func<Expression, Expression, BinaryExpression> binaryOperator = Expression.Equal;

    var body = binaryOperator(leftOperand, rightOperand);

    var root = body.NodeType;
    var left = body.Left.NodeType;
    var right = body.Right.NodeType;
    const string expected =
      """
            MemberAccess
           /
      Equal
           \
            Constant
      """;

    var actual =
      $"""
            {left}
           /
      {root}
           \
            {right}
      """;

    Assert.Equal(expected, actual);
  }

  [Fact]
  public void TreeStructure_Create_3Levels()
  {
    var leftPar = Expression.Parameter(typeof(Claim));
    var leftOperand = Expression.Property(leftPar, nameof(Claim.Value));
    var rightOperand = Expression.Constant("1");
    var body = Expression.Equal(
      Expression.Constant(true),
      Expression.Equal(leftOperand, rightOperand));
    const string expected =
      """
            Constant
           /
      Equal      MemberAccess
           \    /
            Equal__Constant
      """;

    var n1 = body.NodeType;
    var n1L = body.Left.NodeType;
    var n1R = body.Right.NodeType;
    var n2 = (BinaryExpression)body.Right;
    var n2L = n2.Left.NodeType;
    var n2R = n2.Right.NodeType;

    var actual =
      $"""
            {n1L}
           /
      {n1}      {n2L}
           \    /
            {n1R}__{n2R}
      """;

    Assert.Equal(expected, actual);
  }

  [Fact]
  public void Lambda_CreateFunc1()
  {
    var leftPar = Expression.Parameter(typeof(Claim));
    var leftOperand = Expression.Property(leftPar, nameof(Claim.Value));
    var rightOperand = Expression.Constant("1");
    var body = Expression.Equal(leftOperand, rightOperand);

    var lambda = Expression.Lambda<Func<Claim, bool>>(body, leftPar);
    var func = lambda.Compile();

    Assert.True(func(new Claim(ClaimTypes.Name, "1")));
    Assert.False(func(new Claim(ClaimTypes.Name, "2")));
  }

  [Fact]
  public void Lambda_CreateFunc2()
  {
    var claimType = typeof(Claim);
    var claimValuePropName = nameof(Claim.Value);
    var xPar = Expression.Parameter(claimType);
    var yPar = Expression.Parameter(claimType);
    var x = Expression.Property(xPar, claimValuePropName);
    var y = Expression.Property(yPar, claimValuePropName);
    var c = Expression.Constant("1");

    var body1 = Expression.Equal(x, c);
    var body2 = Expression.Equal(y, c);
    var body = Expression.AndAlso(body1, body2);

    var lambda = Expression.Lambda<Func<Claim, Claim, bool>>(body, xPar, yPar);
    var func = lambda.Compile();

    var r1 = func(new Claim(ClaimTypes.Name, "1"), new Claim(ClaimTypes.Name, "1"));
    var r2 = func(new Claim(ClaimTypes.Name, "1"), new Claim(ClaimTypes.Name, "2"));

    Assert.True(r1);
    Assert.False(r2);
  }

  [Fact]
  public void Lambda_Info()
  {
    var claimType = typeof(Claim);
    var claimValuePropName = nameof(Claim.Value);
    var xPar = Expression.Parameter(claimType);
    var yPar = Expression.Parameter(claimType);
    var x = Expression.Property(xPar, claimValuePropName);
    var y = Expression.Property(yPar, claimValuePropName);
    var c = Expression.Constant("1");
    var body1 = Expression.Equal(x, c);
    var body2 = Expression.Equal(y, c);
    var body = Expression.AndAlso(body1, body2);

    var lambda = Expression.Lambda<Func<Claim, Claim, bool>>(body, xPar, yPar);

    // Views
    Assert.Equal(
      "(Param_0, Param_1) => ((Param_0.Value == \"1\") AndAlso (Param_1.Value == \"1\"))",
      lambda.ToString());
    Assert.Equal(
      "((Param_0.Value == \"1\") AndAlso (Param_1.Value == \"1\"))",
      lambda.Body.ToString());

    // Parameters
    Assert.Equal(2, lambda.Parameters.Count);
    Assert.All(lambda.Parameters, (p, i) =>
    {
      Assert.Null(p.Name);
      Assert.Equal(nameof(Expression.Parameter), p.NodeType.ToString());
    });

    Assert.Equal(nameof(Expression.Lambda), lambda.NodeType.ToString());
    Assert.Equal(typeof(Func<Claim, Claim, bool>), lambda.Type);
    Assert.Equal(typeof(bool), lambda.ReturnType);
  }

  [Fact]
  public void Lambda_ViewWithNamedParameters()
  {
    var parNames = new[] { "x", "y" };
    var claimType = typeof(Claim);
    var claimValuePropName = nameof(Claim.Value);
    var xPar = Expression.Parameter(claimType, parNames[0]);
    var yPar = Expression.Parameter(claimType, parNames[1]);
    var x = Expression.Property(xPar, claimValuePropName);
    var y = Expression.Property(yPar, claimValuePropName);
    var c = Expression.Constant("1");
    var body1 = Expression.Equal(x, c);
    var body2 = Expression.Equal(y, c);
    var body = Expression.AndAlso(body1, body2);

    var lambda = Expression.Lambda<Func<Claim, Claim, bool>>(body, xPar, yPar);

    // View
    Assert.Equal(
      "(x, y) => ((x.Value == \"1\") AndAlso (y.Value == \"1\"))",
      lambda.ToString());

    // Parameters
    Assert.Equal(2, lambda.Parameters.Count);
    Assert.All(lambda.Parameters, (p, i) =>
    {
      Assert.Equal(p.Name, parNames[i]);
    });
  }

  [Fact]
  public void ExpressionType_ListOfAll()
  {
    var n = 1;
    foreach (var et in Enum.GetValues<ExpressionType>())
    {
      output.WriteLine($"{n++}. {et}");
    }
  }

  [Fact]
  public void DebugView()
  {
    var debugViewPi = typeof(Expression)
      .GetProperty(
        "DebugView",
        BindingFlags.Instance | BindingFlags.NonPublic);

    var parNames = new[] { "x", "y" };
    var claimType = typeof(Claim);
    var claimValuePropName = nameof(Claim.Value);

    var expressions = new Dictionary<string, Expression>();
    expressions.Add("xPar", Expression.Parameter(claimType, parNames[0]));
    expressions.Add("yPar", Expression.Parameter(claimType, parNames[1]));
    expressions.Add("x", Expression.Property(expressions["xPar"], claimValuePropName));
    expressions.Add("y", Expression.Property(expressions["yPar"], claimValuePropName));
    expressions.Add("c", Expression.Constant("1"));
    expressions.Add("body1", Expression.Equal(expressions["x"], expressions["c"]));
    expressions.Add("body2", Expression.Equal(expressions["y"], expressions["c"]));
    expressions.Add("body", Expression.AndAlso(expressions["body1"], expressions["body2"]));

    foreach (var expr in expressions)
    {
      var key = expr.Key.PadRight(6);
      var view = debugViewPi.GetValue(expr.Value);
      output.WriteLine($"{key}: {view}");
    }

    // xPar  : $x
    // yPar  : $y
    // x     : $x.Value
    // y     : $y.Value
    // c     : "1"
    // body1 : $x.Value == "1"
    // body2 : $y.Value == "1"
    // body  : $x.Value == "1" && $y.Value == "1"
  }

  [Fact]
  public void DebugView_Lambda()
  {
    var debugViewPi = typeof(Expression)
      .GetProperty(
        "DebugView",
        BindingFlags.Instance | BindingFlags.NonPublic);
    var parNames = new[] { "x", "y" };
    var claimType = typeof(Claim);
    var claimValuePropName = nameof(Claim.Value);
    var xPar = Expression.Parameter(claimType, parNames[0]);
    var yPar = Expression.Parameter(claimType, parNames[1]);
    var x = Expression.Property(xPar, claimValuePropName);
    var y = Expression.Property(yPar, claimValuePropName);
    var c = Expression.Constant("1");
    var body1 = Expression.Equal(x, c);
    var body2 = Expression.Equal(y, c);
    var body = Expression.AndAlso(body1, body2);
    var lambda = Expression.Lambda<Func<Claim, Claim, bool>>(body, xPar, yPar);

    output.WriteLine(debugViewPi.GetValue(lambda).ToString());

    // .Lambda #Lambda1<System.Func`3[System.Security.Claims.Claim,System.Security.Claims.Claim,System.Boolean]>(
    //     System.Security.Claims.Claim $x,
    //     System.Security.Claims.Claim $y) {
    //     $x.Value == "1" && $y.Value == "1"
    // }
  }

  [Fact]
  public void Reduce()
  {
    var x = Expression.Parameter(typeof(int), "x");
    var y = Expression.Parameter(typeof(int), "y");
    var c0 = Expression.Constant(0);
    var c1 = Expression.Constant(1);

    var expressions = new Dictionary<string, Expression>
    {
      ["x + y"] = new ReducibleIntAdd(x, y),
      ["x + c1"] = new ReducibleIntAdd(x, c1),
      ["c1 + x"] = new ReducibleIntAdd(c1, x),

      ["x + c0"] = new ReducibleIntAdd(x, c0),
      ["c0 + x"] = new ReducibleIntAdd(c0, x),
      ["c0 + c1"] = new ReducibleIntAdd(c0, c1),
    };

    output.WriteLine("Expr    | Can Reduce | Reduced");
    foreach (var kv in expressions)
    {
      var key = kv.Key.PadRight(7);
      var expr = kv.Value;
      var canReduce = expr.CanReduce.ToString().PadRight(10);
      var reduced = expr.Reduce();
      output.WriteLine($"{key} | {canReduce} | {reduced}");
    }

    // Expr    | Can Reduce | Reduced
    // x + y   | False      | (x + y)
    // x + c1  | False      | (x + 1)
    // c1 + x  | False      | (1 + x)
    // x + c0  | True       | x
    // c0 + x  | True       | x
    // c0 + c2 | True       | 1
  }

  [Fact]
  public void ReduceAndCheck_Reducible()
  {
    var x = Expression.Parameter(typeof(int), "x");
    var y = Expression.Parameter(typeof(int), "y");
    var c0 = Expression.Constant(0);
    var c1 = Expression.Constant(1);

    var expressions = new List<Expression>
    {
      new ReducibleIntAdd(x, c0),
      new ReducibleIntAdd(c0, x),
      new ReducibleIntAdd(c0, c1),
    };

    Assert.All(expressions, expr =>
    {
      Expression reduced = null;
      var ex = Record.Exception(() => reduced = expr.ReduceAndCheck());
      Assert.Null(ex);
      output.WriteLine(reduced.ToString());
    });

    // x
    // x
    // 1
  }

  [Fact]
  public void ReduceAndCheck_NotReducible()
  {
    var x = Expression.Parameter(typeof(int), "x");
    var y = Expression.Parameter(typeof(int), "y");
    var c = Expression.Constant(1);

    var expressions = new List<Expression>
    {
      new ReducibleIntAdd(x, y),
      new ReducibleIntAdd(x, c),
      new ReducibleIntAdd(c, x),
    };

    Assert.All(expressions, expr =>
    {
      Assert.Throws<ArgumentException>(() => expr.ReduceAndCheck());
    });
  }

  [Theory]
  [InlineData(ReducedResult.Null)]
  [InlineData(ReducedResult.This)]
  public void ReduceAndCheck_PreventReturn(ReducedResult reducedResult)
  {
    var expr = new BrokenReducible(reducedResult);
    Assert.Throws<ArgumentException>(expr.ReduceAndCheck);
  }

  [Fact]
  public void ReduceExtensions_BuiltinRoot()
  {
    var x = Expression.Parameter(typeof(int), "x");
    var c = Expression.Constant(0);
    var expr = Expression.Add(
      new ReducibleIntAdd(x, c),
      c);

    var reduced = expr.ReduceExtensions();

    Assert.Equal(expr, reduced);
  }

  [Fact]
  public void ReduceExtensions_CustomRoot_Reducible()
  {
    var x = Expression.Parameter(typeof(int), "x");
    var c = Expression.Constant(0);
    var expr = new ReducibleIntAdd(
      new ReducibleIntAdd(x, c),
      c);

    var reduced = expr.ReduceExtensions();

    Assert.Equal(x, reduced);
  }

  [Fact]
  public void ReduceExtensions_CustomRoot_NotReducible()
  {
    var x = Expression.Parameter(typeof(int), "x");
    var c = Expression.Constant(0);
    var expr = new ReducibleIntAdd(
      new ReducibleIntAdd(x, c),
      new ReducibleIntAdd(x, c));

    var ex = Assert.Throws<ArgumentException>(expr.ReduceExtensions);
    output.WriteLine(ex.Message); // must be reducible node
  }

  [Fact]
  public void IsByRef_StructParameter_WithRefModifier()
  {
    var x = 1;
    var par = Expression.Parameter(typeof(int).MakeByRefType(), "x");
    var body = Expression.PostIncrementAssign(par);
    var lambda = Expression.Lambda<ActionRef<int>>(body, par);
    var action = lambda.Compile();

    action(ref x);

    Assert.True(par.IsByRef);
    Assert.Equal(2, x);
  }

  [Fact]
  public void IsByRef_ReferenceParameter_WithoutRefModifier()
  {
    var s = "1";
    var par = Expression.Parameter(typeof(string), "s");
    var body = Expression.Assign(par, Expression.Constant("2"));
    var lambda = Expression.Lambda<Action<string>>(body, par);
    var action = lambda.Compile();

    action(s);

    Assert.False(par.IsByRef);
    Assert.Equal("1", s);
  }

  [Fact]
  public void IsByRef_ReferenceParameter_WithRefModifier()
  {
    var s = "1";
    var par = Expression.Parameter(typeof(string).MakeByRefType(), "s");
    var body = Expression.Assign(par, Expression.Constant("2"));
    var lambda = Expression.Lambda<ActionRef<string>>(body, par);
    var action = lambda.Compile();

    action(ref s);

    Assert.True(par.IsByRef);
    Assert.Equal("2", s);
  }

  [Fact]
  public void Visitor()
  {
    var x = Expression.Parameter(typeof(int), "x");
    var y = Expression.Parameter(typeof(int), "y");
    var c0 = Expression.Constant(0);
    var c1 = Expression.Constant(1);

    var expressions = new Dictionary<string, Expression>
    {
      ["x + y"] = Expression.Add(x, y),
      ["x + c1"] = Expression.Add(x, c1),
      ["c1 + x"] = Expression.Add(c1, x),

      ["x + c0"] = Expression.Add(x, c0),
      ["c0 + x"] = Expression.Add(c0, x),
      ["c0 + c1"] = Expression.Add(c0, c1),
    };

    var intAddVisitor = new IntAddVisitor();

    output.WriteLine("Expr    | Modified | Visited");
    foreach (var kv in expressions)
    {
      var key = kv.Key.PadRight(7);
      var expr = kv.Value;
      var visited = intAddVisitor.Visit(expr, out var modified);
      var modifiedString = modified.ToString().PadRight(8);
      output.WriteLine($"{key} | {modifiedString} | {visited}");
    }

    // Expr    | Modified | Visited
    // x + y   | False    | (x + y)
    // x + c1  | False    | (x + 1)
    // c1 + x  | False    | (1 + x)
    // x + c0  | True     | x
    // c0 + x  | True     | x
    // c0 + c1 | True     | 1
  }

  [Fact]
  public void Visitor_Immutable()
  {
    var intAddVisitor = new IntAddVisitor();
    var c = Expression.Constant(0);
    var x = Expression.Parameter(typeof(int), "x");
    var expr = Expression.Add(
      Expression.Add(
        x,
        Expression.Add(x, x)),
      Expression.Add(
        x,
        x));

    var visited = intAddVisitor.Visit(expr, out var modified);

    Assert.False(modified);
    Assert.Equal(expr, visited);
    output.WriteLine(visited.ToString()); // ((x + (x + x)) + (x + x))
  }
}
