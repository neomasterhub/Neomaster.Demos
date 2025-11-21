using System.Linq.Expressions;
using System.Reflection;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Xunit;
using static Neomaster.Demos.Tests.LinqExpr.BrokenReducible;

namespace Neomaster.Demos.Tests.LinqExpr;

public class LinqExprUnitDemos(ITestOutputHelper output)
{
  private delegate void ActionRef<T>(ref T arg);
  private delegate void ActionRef<T1, T2>(ref T1 arg1, ref T2 arg2);

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
  public void Lambda_ParameterOrder()
  {
    var type = typeof(int);
    var x = Expression.Parameter(type, "x");
    var y = Expression.Parameter(type, "y");
    var body = Expression.Subtract(x, y);

    var funcXY = Expression.Lambda<Func<int, int, int>>(body, x, y).Compile();
    var funcYX = Expression.Lambda<Func<int, int, int>>(body, y, x).Compile();

    Assert.Equal(9, funcXY(10, 1));
    Assert.Equal(9, funcYX(1, 10));
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
  public void Lambda_TypedVsUntyped()
  {
    var x = Expression.Parameter(typeof(int), "x");
    var body = Expression.Add(x, Expression.Constant(10));

    var typed = Expression.Lambda<Func<int, int>>(body, x); // Expression<Func<int, int>>
    var func = typed.Compile(); // Func<int, int>
    var r1 = func(1);
    var r2 = func.Invoke(2);

    var untyped = Expression.Lambda(body, x); // LambdaExpression
    var dlg = untyped.Compile(); // Delegate
    var r3 = dlg.DynamicInvoke(3);
  }

  [Fact]
  public void Lambda_DynamicInvoke_DynamicFunc()
  {
    var x = Expression.Parameter(typeof(int), "x");
    var y = Expression.Parameter(typeof(int), "y");
    var body1 = Expression.Add(x, y);

    var arr = Expression.Parameter(typeof(byte[]), "arr");
    var body2 = Expression.ArrayLength(arr);

    static object DynamicFunc(Expression body, ParameterExpression[] pars, params object[] args)
    {
      return Expression.Lambda(body, pars).Compile().DynamicInvoke(args);
    }

    Assert.Equal(3, DynamicFunc(body1, [x, y], 1, 2));
    Assert.Equal(3, DynamicFunc(body2, [arr], new byte[] { 1, 2, 3 }));
  }

  [Fact]
  public void Lambda_DynamicInvoke_DynamicAdd()
  {
    static object DynamicAdd<T>(T x, T y)
    {
      var type = typeof(T);
      var xPar = Expression.Parameter(type, nameof(x));
      var yPar = Expression.Parameter(type, nameof(y));
      var add = Expression.Add(xPar, yPar);

      // Optimization...

      return Expression.Lambda(add, xPar, yPar).Compile().DynamicInvoke(x, y);
    }

    Assert.Equal(3, DynamicAdd(1, 2));
    Assert.Equal(3.3m, DynamicAdd(1.1m, 2.2m));
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
  public void Visitor_Root()
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
  public void Visitor_Tree_Immutable()
  {
    var intAddVisitor = new IntAddVisitor();
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

  [Fact]
  public void Visitor_Tree_WithMutableChildNode()
  {
    var intAddVisitor = new IntAddVisitor();
    var c = Expression.Constant(0);
    var x = Expression.Parameter(typeof(int), "x");
    var expr = Expression.Add(
      Expression.Add(
        x,
        Expression.Add(x, c)),
      Expression.Add(
        x,
        x));

    var visited = intAddVisitor.Visit(expr, out var modified);

    Assert.True(modified);
    Assert.NotEqual(expr, visited);
    output.WriteLine(visited.ToString()); // ((x + x) + (x + x))
  }

  [Fact]
  public void Invoke()
  {
    var x = Expression.Parameter(typeof(int), "x");
    var c1 = Expression.Constant(1);
    var c2 = Expression.Constant(2);
    var body1 = Expression.Add(x, c1);
    var lambda1 = Expression.Lambda<Func<int, int>>(body1, x);

    var ex = Assert.Throws<InvalidOperationException>(() => Expression.Add(lambda1, c2));
    output.WriteLine(ex.Message);
    // The binary operator Add is not defined for the types
    // 'System.Func`2[System.Int32,System.Int32]' and 'System.Int32'.

    // Analogy
    // ((int x) => x + 1) + 2
    // Error: Operator '+' cannot be applied to operands of type 'lambda expression' and 'int'

    var body2 = Expression.Add(Expression.Invoke(lambda1, x), c2);
    var lambda2 = Expression.Lambda<Func<int, int>>(body2, x);
    var func2 = lambda2.Compile();
    var result = func2(10);
    Assert.Equal(13, result);
  }

  [Fact]
  public void Call_InstanceMethod()
  {
    var par = Expression.Parameter(typeof(string), "s");
    var mi = output.GetType().GetMethod(nameof(output.WriteLine), [typeof(string)]);
    var body = Expression.Call(Expression.Constant(output), mi, par);
    var lambda = Expression.Lambda<Action<string>>(body, par);
    var action = lambda.Compile();

    action("1"); // 1
  }

  [Fact]
  public void Call_StaticMethod()
  {
    var type = typeof(string);
    var par1 = Expression.Parameter(type, "s1");
    var par2 = Expression.Parameter(type, "s2");
    var mi = type.GetMethod(nameof(string.Concat), [type, type]);
    var body = Expression.Call(mi, par1, par2);
    var lambda = Expression.Lambda<Func<string, string, string>>(body, par1, par2);
    var func = lambda.Compile();

    var result = func("1", "2");

    Assert.Equal("12", result);
  }

  [Fact]
  public void New()
  {
    var type = typeof(User);
    var argType = typeof(string);
    var email = Expression.Parameter(argType, "email");
    var body1 = Expression.New(type);
    var body2 = Expression.New(type.GetConstructor([]));
    var body3 = Expression.New(type.GetConstructor([argType]), email);
    var func1 = Expression.Lambda<Func<User>>(body1).Compile();
    var func2 = Expression.Lambda<Func<User>>(body2).Compile();
    var func3 = Expression.Lambda<Func<string, User>>(body3, email).Compile();

    var user1 = func1();
    Assert.NotNull(user1);
    Assert.Null(user1.Id);
    Assert.Null(user1.Email);

    var user2 = func2();
    Assert.NotNull(user2);
    Assert.Null(user2.Id);
    Assert.Null(user2.Email);

    var user3 = func3("1");
    Assert.NotNull(user3);
    Assert.True(Guid.TryParse(user3.Id, out _));
    Assert.Equal("1", user3.Email);
  }

  [Fact]
  public void MemberInit_Bind()
  {
    var parType = typeof(string);
    var id = Expression.Parameter(parType, "id");
    var email = Expression.Parameter(parType, "email");
    var type = typeof(User);
    var body = Expression.MemberInit(
      Expression.New(type),
      Expression.Bind(type.GetProperty(nameof(User.Id)), id),
      Expression.Bind(type.GetProperty(nameof(User.Email)), email));
    var lambda = Expression.Lambda<Func<string, string, User>>(body, id, email);
    var func = lambda.Compile();

    var user = func("1", "2");

    Assert.NotNull(user);
    Assert.Equal("1", user.Id);
    Assert.Equal("2", user.Email);
  }

  [Fact]
  public void MemberBind()
  {
    var type = typeof(User);
    var childType = typeof(Department);
    var parType = typeof(int);
    var id1 = Expression.Parameter(parType, "id1");
    var id2 = Expression.Parameter(parType, "id2");
    var body = Expression.MemberInit(
      Expression.New(type),
      Expression.Bind(
        type.GetProperty(nameof(User.DepartmentNull)),
        Expression.New(childType)),
      Expression.MemberBind(
        type.GetProperty(nameof(User.DepartmentNull)),
        Expression.Bind(
          childType.GetProperty(nameof(Department.Id)),
          id1)),
      Expression.MemberBind(
        type.GetProperty(nameof(User.DepartmentDefault)),
        Expression.Bind(
          childType.GetProperty(nameof(Department.Id)),
          id2)));
    var ctor = Expression.Lambda<Func<int, int, User>>(body, id1, id2).Compile();

    var user = ctor(1, 2);

    Assert.Equal(1, user.DepartmentNull.Id);
    Assert.Equal(2, user.DepartmentDefault.Id);
  }

  [Fact]
  public void Quote_LambdaReturnsLambda()
  {
    const string view = "x => y => (x + y)";
    var x = Expression.Parameter(typeof(int), "x");
    var y = Expression.Parameter(typeof(int), "y");

    var lambda1 = Expression.Lambda(
      Expression.Lambda(
        Expression.Add(x, y),
        y),
      x);
    var dlg1 = lambda1.Compile();

    Assert.Equal(view, lambda1.ToString());
    Assert.Equal(ExpressionType.Lambda, lambda1.Body.NodeType);
    Assert.IsType<Func<int, Func<int, int>>>(dlg1);
    var func1 = (Func<int, Func<int, int>>)dlg1;
    Assert.Equal(3, func1(1)(2));

    var lambda2 = Expression.Lambda(
      Expression.Quote(
        Expression.Lambda(
          Expression.Add(x, y),
          y)),
      x);
    var dlg2 = lambda2.Compile();

    Assert.Equal(view, lambda2.ToString());
    Assert.Equal(ExpressionType.Quote, lambda2.Body.NodeType);
    Assert.IsType<Func<int, Expression<Func<int, int>>>>(dlg2);
    var func2 = (Func<int, Expression<Func<int, int>>>)dlg2;
    Assert.Equal(3, func2(1).Compile()(2));
  }

  [Fact]
  public void Quote_InvokeReturnsLambda_InDbProvider()
  {
    var par = Expression.Parameter(typeof(Department), "dep");
    var prop = Expression.Property(par, nameof(Department.Name));

    var lambda1 = Expression.Lambda<Func<Department, bool>>(
      Expression.Invoke(
        Expression.Lambda<Func<Department, bool>>(
          Expression.Equal(prop, Expression.Constant("D1")),
          par),
        par),
      par);

    var lambda2 = Expression.Lambda<Func<Department, bool>>(
      Expression.Invoke(
        Expression.Quote(
          Expression.Lambda<Func<Department, bool>>(
            Expression.Equal(prop, Expression.Constant("D1")),
            par)),
        par),
      par);

    using var db = new LinqExprDemoDbContext();
    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();
    db.Database.Migrate();
    db.Departments.Add(new() { Name = "D1" });
    db.Departments.Add(new() { Name = "D2" });
    db.SaveChanges();

    var d1 = db.Departments.First(lambda1);
    Assert.Equal("D1", d1.Name);

    var d2 = db.Departments.First(lambda2.Compile());
    Assert.Equal("D1", d2.Name);
  }

  [Fact]
  public void Assign()
  {
    var pUser = Expression.Parameter(typeof(User), "user");
    var pDep = Expression.Parameter(typeof(Department), "dep");
    var body = Expression.Assign(
      Expression.Property(pUser, nameof(User.Department)),
      pDep);
    var assignUserDepLambda = Expression.Lambda<Action<User, Department>>(body, pUser, pDep);
    var assignUserDepFunc = assignUserDepLambda.Compile();
    var user = new User();

    assignUserDepFunc(user, new Department { Id = 1, Name = "A" });

    Assert.Equal("(user, dep) => (user.Department = dep)", assignUserDepLambda.ToString());
    Assert.NotNull(user.Department);
    Assert.Equal(1, user.Department.Id);
    Assert.Equal("A", user.Department.Name);
  }

  [Fact]
  public void Block_Variable_Swap()
  {
    var type = typeof(int);
    var t = Expression.Variable(type, "t");
    var x = Expression.Parameter(type.MakeByRefType(), "x");
    var y = Expression.Parameter(type.MakeByRefType(), "y");
    var body = Expression.Block(
      variables: [t],
      Expression.Assign(t, x),
      Expression.Assign(x, y),
      Expression.Assign(y, t));
    var lambda = Expression.Lambda<ActionRef<int, int>>(body, x, y);
    var action = lambda.Compile();
    var a = 1;
    var b = 2;

    action(ref a, ref b);

    Assert.Equal(2, a);
    Assert.Equal(1, b);
  }

  [Fact]
  public void Block_ReturnsLastExpressionResult()
  {
    var type = typeof(int);
    var x = Expression.Parameter(type, "x");
    var y = Expression.Parameter(type, "y");
    var body = Expression.Block(
      Expression.Add(x, y));

    var sum = Expression.Lambda<Func<int, int, int>>(body, x, y).Compile();

    Assert.Equal(3, sum(1, 2));
  }

  [Fact]
  public void ConditionalOperators()
  {
    var x = Expression.Parameter(typeof(int), "x");
    var v = Expression.Variable(typeof(bool), "v");
    var c = Expression.Constant(0);
    var blocks = new List<BlockExpression>
    {
      Expression.Block(
        [v],
        Expression.Assign(v, Expression.Constant(false)),
        Expression.IfThen(
          Expression.GreaterThan(x, c),
          Expression.Assign(v, Expression.Constant(true))),
        v),

      Expression.Block(
        [v],
        Expression.IfThenElse(
          Expression.GreaterThan(x, c),
          Expression.Assign(v, Expression.Constant(true)),
          Expression.Assign(v, Expression.Constant(false))),
        v),

      Expression.Block(
        Expression.Condition(
          Expression.GreaterThan(x, c),
          Expression.Constant(true),
          Expression.Constant(false))),

      Expression.Block(
        [v],
        Expression.Switch(
          x,
          Expression.Throw(
            Expression.New(typeof(IndexOutOfRangeException)),
            typeof(bool)),
          Expression.SwitchCase(
            Expression.Assign(v, Expression.Constant(true)),
            Expression.Constant(1)),
          Expression.SwitchCase(
            Expression.Assign(v, Expression.Constant(false)),
            Expression.Constant(-1))),
        v),
    };

    foreach (var block in blocks)
    {
      var check = Expression.Lambda<Func<int, bool>>(block, x).Compile();
      Assert.True(check(1));
      Assert.False(check(-1));
    }
  }

  [Fact]
  public void Throw()
  {
    var x = Expression.Parameter(typeof(int), "x");
    var func1 = Expression.Lambda<Func<int, bool>>(
      Expression.Condition(
        Expression.GreaterThan(x, Expression.Constant(0)),
        Expression.Constant(true),
        Expression.Throw(
          Expression.New(typeof(InvalidOperationException)),
          typeof(bool))),
      x).Compile();
    var func2 = Expression.Lambda<Func<int, string>>(
      Expression.Condition(
        Expression.GreaterThan(x, Expression.Constant(0)),
        Expression.Constant("1"),
        Expression.Throw(
          Expression.New(typeof(InvalidOperationException)),
          typeof(string))),
      x).Compile();
    var func3 = Expression.Lambda<Func<int, Department>>(
      Expression.Condition(
        Expression.GreaterThan(x, Expression.Constant(0)),
        Expression.New(typeof(Department)),
        Expression.Throw(
          Expression.New(typeof(InvalidOperationException)),
          typeof(Department))),
      x).Compile();

    Assert.Throws<InvalidOperationException>(() => func1.Invoke(-1));
    Assert.Throws<InvalidOperationException>(() => func2.Invoke(-1));
    Assert.Throws<InvalidOperationException>(() => func3.Invoke(-1));
  }

  [Fact]
  public void Goto_Label_Empty()
  {
    var label = Expression.Label(); // declaration
    var v = Expression.Variable(typeof(int), "v");
    var x = Expression.Parameter(typeof(int), "x");
    var add100 = Expression.Parameter(typeof(bool), "add100");
    var body = Expression.Block(
      [v],
      Expression.Assign(v, x),
      Expression.IfThen(
        Expression.IsFalse(add100),
        Expression.Goto(label)),
      Expression.AddAssign(v, Expression.Constant(100)),
      Expression.Label(label), // instruction
      v);

    var add100Func = Expression.Lambda<Func<int, bool, int>>(body, x, add100).Compile();

    Assert.Equal(1, add100Func(1, false));
    Assert.Equal(101, add100Func(1, true));
  }

  [Fact]
  public void Goto_Label_Instruction()
  {
    var label = Expression.Label(); // declaration
    var v = Expression.Variable(typeof(int), "v");
    var x = Expression.Parameter(typeof(int), "x");
    var add100 = Expression.Parameter(typeof(bool), "add100");
    var body = Expression.Block(
      [v],
      Expression.Assign(v, x),
      Expression.IfThen(
        Expression.IsFalse(add100),
        Expression.Goto(label)),
      Expression.Label(label, Expression.AddAssign(v, Expression.Constant(100))), // instruction
      v);

    var add100Func = Expression.Lambda<Func<int, bool, int>>(body, x, add100).Compile();

    Assert.Equal(1, add100Func(1, false));
    Assert.Equal(101, add100Func(1, true));
  }

  [Fact]
  public void Goto_Label_ReturnValue()
  {
    var label = Expression.Label(typeof(int));
    var x = Expression.Parameter(typeof(bool), "x");
    var body = Expression.Block(
      Expression.IfThen(
        Expression.IsTrue(x),
        Expression.Goto(label, Expression.Constant(1))),
      Expression.Label(label, Expression.Constant(0)));

    var func = Expression.Lambda<Func<bool, int>>(body, x).Compile();

    Assert.Equal(0, func(false));
    Assert.Equal(1, func(true));
  }

  [Fact]
  public void Return_LikeGoto()
  {
    var type = typeof(int);
    var label1 = Expression.Label(type);
    var label2 = Expression.Label(type);
    var body = Expression.Block(
      Expression.Return(label1, Expression.Constant(1)),
      Expression.Label(label1, Expression.Constant(2)),
      Expression.Label(label2, Expression.Constant(3)));

    var func = Expression.Lambda<Func<int>>(body).Compile();

    Assert.Equal(3, func());
  }

  [Fact]
  public void ReturnVsGoto_SemanticDifference()
  {
    var c = Expression.Constant(1);
    var label = Expression.Label(typeof(int));
    var gt = Expression.Goto(label, c);
    var rt = Expression.Return(label, c);

    Assert.IsType<GotoExpression>(gt);
    Assert.IsType<GotoExpression>(rt);

    Assert.Equal(GotoExpressionKind.Goto, gt.Kind);
    Assert.Equal(GotoExpressionKind.Return, rt.Kind);
  }

  [Fact]
  public void ReturnVsGoto_CallViaKind()
  {
    var tInt = typeof(int);
    var tExpression = typeof(Expression);
    var label1 = Expression.Label(tInt);
    var label2 = Expression.Label(tInt);

    MethodInfo GetMethodInfo(GotoExpressionKind kind) => tExpression.GetMethod(
      kind.ToString(),
      [typeof(LabelTarget), tExpression]);

    Func<int> GetFunc(GotoExpressionKind kind) =>
     Expression.Lambda<Func<int>>(
       Expression.Block(
        Expression.Call(
          GetMethodInfo(kind),
          Expression.Constant(label1),
          Expression.Constant(Expression.Constant(1))),
        Expression.Label(label1, Expression.Constant(2)),
        Expression.Label(label2, Expression.Constant(3))))
     .Compile();

    Assert.Equal(3, GetFunc(GotoExpressionKind.Goto)());
    Assert.Equal(3, GetFunc(GotoExpressionKind.Return)());
  }

  [Fact]
  public void Loop_Break_PowerTo10()
  {
    var tInt = typeof(int);
    var i = Expression.Variable(tInt);
    var input = Expression.Parameter(tInt);
    var iterations = Expression.Parameter(tInt);
    var breakLabel = Expression.Label();
    var body = Expression.Block(
      [i],
      Expression.Assign(i, Expression.Constant(0)),
      Expression.Loop(
        Expression.Block(
          Expression.IfThenElse(
            Expression.LessThan(i, iterations),
            Expression.Block(
              Expression.MultiplyAssign(input, Expression.Constant(10)),
              Expression.PostIncrementAssign(i)),
            Expression.Break(breakLabel)))),
      Expression.Label(breakLabel),
      input);

    var powerTo10 = Expression.Lambda<Func<int, int, int>>(body, input, iterations).Compile();

    Assert.Equal(1, powerTo10(1, 0));
    Assert.Equal(20, powerTo10(2, 1));
    Assert.Equal(300, powerTo10(3, 2));
  }

  [Fact]
  public void Loop_Continue_SelectEven()
  {
    var tList = typeof(List<int>);
    var input = Expression.Parameter(tList);
    var result = Expression.Variable(tList);
    var breakLabel = Expression.Label();
    var continueLabel = Expression.Label();
    var i = Expression.Variable(typeof(int));
    var body = Expression.Block(
      [i, result],
      Expression.Assign(i, Expression.Constant(0)),
      Expression.Assign(result, Expression.New(tList)),
      Expression.Loop(
        Expression.Block(
          Expression.IfThen( // if (i >= Count) break
            Expression.GreaterThanOrEqual(i, Expression.Property(input, "Count")),
            Expression.Break(breakLabel)),
          Expression.IfThen( // if (input[i] % 2 != 0) continue
            Expression.NotEqual(
              Expression.Modulo(
                Expression.Property(input, "Item", i),
                Expression.Constant(2)),
              Expression.Constant(0)),
            Expression.Continue(continueLabel)),
          Expression.Call(
            result,
            tList.GetMethod("Add", [typeof(int)]),
            Expression.Property(input, "Item", i)),
          Expression.Label(continueLabel),
          Expression.PostIncrementAssign(i))),
      Expression.Label(breakLabel),
      result);

    var selectEven = Expression.Lambda<Func<List<int>, List<int>>>(body, input).Compile();

    Assert.Equal([2, 4], selectEven([1, 2, 3, 4]));
  }

  [Fact]
  public void GotoExpressionKind_ListOfAll()
  {
    var label = Expression.Label();
    var expectedKinds = Enum.GetValues<GotoExpressionKind>();

    var exprs = new GotoExpression[]
    {
      Expression.Goto(label),
      Expression.Return(label),
      Expression.Break(label),
      Expression.Continue(label),
    };

    Assert.Equal(expectedKinds.Length, exprs.Length);
    Assert.All(expectedKinds, (expectedKind, i) =>
    {
      Assert.Equal(expectedKind, exprs[i].Kind);
    });
  }

  [Fact]
  public void TryCatchFinally()
  {
    var v = Expression.Variable(typeof(int));
    var body = Expression.Block(
      [v],
      Expression.TryCatchFinally(
        Expression.Throw(
          Expression.New(typeof(InvalidOperationException)),
          typeof(int)),
        Expression.AddAssign(v, Expression.Constant(10)),
        Expression.Catch(
          typeof(InvalidOperationException),
          Expression.Assign(v, Expression.Constant(1))),
        Expression.Catch(
          typeof(IndexOutOfRangeException),
          Expression.Assign(v, Expression.Constant(2)))),
      v);

    var func = Expression.Lambda<Func<int>>(body).Compile();

    Assert.Equal(11, func());
  }

  [Fact]
  public void ReversePolishNotation()
  {
    // (1 + 2) * 3
    var tokens = new[] { "1", "2", "+", "3", "*" };
    var stack = new Stack<Expression>();

    foreach (var token in tokens)
    {
      if (int.TryParse(token, out var operand))
      {
        stack.Push(Expression.Constant(operand));
        continue;
      }

      stack.Push(token switch
      {
        "+" => Expression.Add(stack.Pop(), stack.Pop()),
        "*" => Expression.Multiply(stack.Pop(), stack.Pop()),
        _ => throw new InvalidOperationException($"Unknown token \"{token}\""),
      });
    }

    var body = stack.Pop();
    var calc = Expression.Lambda<Func<int>>(body).Compile();

    Assert.Equal("(3 * (2 + 1))", body.ToString());
    Assert.Equal(9, calc());
  }

  [Fact]
  public void AutoMapper()
  {
    Func<TSrc, TDst> CreateMapper<TSrc, TDst>()
    {
      var tSrc = typeof(TSrc);
      var tDst = typeof(TDst);
      var src = Expression.Parameter(tSrc);
      var body = Expression.MemberInit(
        Expression.New(tDst),
        tDst
          .GetProperties()
          .Select(p2 =>
          {
            var p1 = tSrc.GetProperty(p2.Name);
            return Expression.Bind(p2, Expression.Property(src, p1));
          }));

      return Expression.Lambda<Func<TSrc, TDst>>(body, src).Compile();
    }

    var src = new User
    {
      Id = "1",
      Email = "2",
      Department = new Department
      {
        Id = 3,
        Name = "4",
      },
    };

    var mapper = CreateMapper<User, UserDto>();
    var dst = mapper(src);

    Assert.Equal(src.Id, dst.Id);
    Assert.Equal(src.Email, dst.Email);
    Assert.Equal(src.Department.Id, dst.Department.Id);
    Assert.Equal(src.Department.Name, dst.Department.Name);
  }

  [Fact]
  public void SqlGeneration()
  {
    string MapOperator(ExpressionType type) => type switch
    {
      ExpressionType.Equal => "=",
      ExpressionType.GreaterThan => ">",
      ExpressionType.AndAlso => "AND",
      _ => throw new ArgumentOutOfRangeException($"Unknown operator \"{type}\"")
    };

    string Parse(Expression expr)
    {
      return expr switch
      {
        BinaryExpression b => $"{Parse(b.Left)} {MapOperator(b.NodeType)} {Parse(b.Right)}",
        MemberExpression m => m.Member.Name,
        ConstantExpression c =>
          c.Value is string s
          ? $"'{s}'"
          : (c.Value?.ToString() ?? "NULL"),
        _ => throw new NotSupportedException($"Unsupported expression \"{expr.NodeType}\""),
      };
    }

    string ToSql<T>(Expression<Func<T, bool>> predicate)
    {
      return
        $"""
        SELECT *
        FROM {typeof(T).Name}
        WHERE
        {Parse(predicate.Body)}
        """;
    }

    var sql = ToSql<Department>(d => d.Id > 1 && d.Name == "2");

    Assert.Equal(
      """
      SELECT *
      FROM Department
      WHERE
      Id > 1 AND Name = '2'
      """,
      sql);
  }
}
