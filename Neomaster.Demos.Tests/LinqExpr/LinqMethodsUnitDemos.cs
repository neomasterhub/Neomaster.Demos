using System.Collections;
using System.ComponentModel;
using System.Reflection;
using Xunit;

namespace Neomaster.Demos.Tests.LinqExpr;

[Description("Methods")]
public class LinqMethodsUnitDemos(ITestOutputHelper output)
{
  [Fact(DisplayName = "`Enumerable` and `Queryable` method names")]
  public void EnumerableAndQueryableMethodNames()
  {
    static string[] GetMethodNames(Type type)
    {
      return type
        .GetMethods(BindingFlags.Static | BindingFlags.Public)
        .Select(m => m.Name)
        .Distinct()
        .Order()
        .ToArray();
    }

    var eMethodNames = GetMethodNames(typeof(Enumerable));
    var qMethodNames = GetMethodNames(typeof(Queryable));
    var allMethodNames = eMethodNames
      .Concat(qMethodNames)
      .Order()
      .Distinct()
      .ToArray();

    output.WriteLine("Enumerable               | Queryable");
    output.WriteLine("-------------------------|----------");

    var ei = 0;
    var qi = 0;
    for (var i = 0; i < allMethodNames.Length; i++)
    {
      string row = null;
      var eMethodName = eMethodNames[ei];
      var qMethodName = qMethodNames[qi];

      if (eMethodName == allMethodNames[i])
      {
        row = $"{eMethodName,-25}|";
        ei++;
      }
      else
      {
        row = new string(' ', 25) + '|';
      }

      if (qMethodName == allMethodNames[i])
      {
        row += $" {qMethodName}";
        qi++;
      }

      output.WriteLine(row);
    }

    // Enumerable               | Queryable
    // -------------------------|----------
    // Aggregate                | Aggregate
    // AggregateBy              | AggregateBy
    // All                      | All
    // Any                      | Any
    // Append                   | Append
    // AsEnumerable             |
    //                          | AsQueryable
    // Average                  | Average
    // Cast                     | Cast
    // Chunk                    | Chunk
    // Concat                   | Concat
    // Contains                 | Contains
    // Count                    | Count
    // CountBy                  | CountBy
    // DefaultIfEmpty           | DefaultIfEmpty
    // Distinct                 | Distinct
    // DistinctBy               | DistinctBy
    // ElementAt                | ElementAt
    // ElementAtOrDefault       | ElementAtOrDefault
    // Empty                    |
    // Except                   | Except
    // ExceptBy                 | ExceptBy
    // First                    | First
    // FirstOrDefault           | FirstOrDefault
    // GroupBy                  | GroupBy
    // GroupJoin                | GroupJoin
    // Index                    | Index
    // Intersect                | Intersect
    // IntersectBy              | IntersectBy
    // Join                     | Join
    // Last                     | Last
    // LastOrDefault            | LastOrDefault
    // LongCount                | LongCount
    // Max                      | Max
    // MaxBy                    | MaxBy
    // Min                      | Min
    // MinBy                    | MinBy
    // OfType                   | OfType
    // Order                    | Order
    // OrderBy                  | OrderBy
    // OrderByDescending        | OrderByDescending
    // OrderDescending          | OrderDescending
    // Prepend                  | Prepend
    // Range                    |
    // Repeat                   |
    // Reverse                  | Reverse
    // Select                   | Select
    // SelectMany               | SelectMany
    // SequenceEqual            | SequenceEqual
    // Single                   | Single
    // SingleOrDefault          | SingleOrDefault
    // Skip                     | Skip
    // SkipLast                 | SkipLast
    // SkipWhile                | SkipWhile
    // Sum                      | Sum
    // Take                     | Take
    // TakeLast                 | TakeLast
    // TakeWhile                | TakeWhile
    // ThenBy                   | ThenBy
    // ThenByDescending         | ThenByDescending
    // ToArray                  |
    // ToDictionary             |
    // ToHashSet                |
    // ToList                   |
    // ToLookup                 |
    // TryGetNonEnumeratedCount |
    // Union                    | Union
    // UnionBy                  | UnionBy
    // Where                    | Where
    // Zip                      | Zip
  }

  [Fact(DisplayName = "`Enumerable.Range()`")]
  public void EnumerableRange()
  {
    Assert.Equal(
      [-1, 0, 1],
      Enumerable.Range(-1, 3));
  }

  [Fact(DisplayName = "`Enumerable.Repeat()`")]
  public void EnumerableRepeat()
  {
    var e = new User();
    var expected = new User[] { e, e, e };

    var actual = Enumerable.Repeat(e, 3);

    Assert.Equal(expected, actual);
  }

  [Fact(DisplayName = "`Enumerable.Empty()`")]
  public void EnumerableEmpty()
  {
    Assert.Empty(Enumerable.Empty<User>());
  }

  [Fact(DisplayName = "`Aggregate()`")]
  public void Aggregate()
  {
    var sum = Enumerable.Repeat(1, 100).Aggregate((sum, i) => sum + i);
    Assert.Equal(100, sum);
  }

  [Fact(DisplayName = "`AggregateBy()`")]
  public void AggregateBy()
  {
    var expected = new Dictionary<string, int>
    {
      ["odd"] = 1000 + 1 + 3,
      ["even"] = 2000 + 2 + 4,
    };

    var actual = Enumerable.Range(1, 4)
      .AggregateBy(
        k => k % 2 == 0 ? "even" : "odd",
        s => s == "odd" ? 1000 : 2000,
        (a, v) => a + v)
      .ToDictionary();

    Assert.Equal(expected, actual);
  }

  [Fact(DisplayName = "`All()`")]
  public void All()
  {
    var range = Enumerable.Range(1, 10);

    Assert.True(range.All(x => x > 0));
    Assert.False(range.All(x => x < 0));
  }

  [Fact(DisplayName = "`Any()`")]
  public void Any()
  {
    var range = Enumerable.Range(1, 10);

    Assert.True(range.Any());
    Assert.False(Enumerable.Empty<User>().Any());

    Assert.True(range.Any(x => x > 5));
    Assert.False(range.Any(x => x > 10));
  }

  [Fact(DisplayName = "All with `Any()`")]
  public void AllWithAny()
  {
    Assert.True(
      Enumerable.Range(1, 2)
        .Select(x => Enumerable.Repeat(x, 2))
        .All(x => x.Any()));

    Assert.True(
      Enumerable.Range(1, 2)
        .Select(x => Enumerable.Empty<User>())
        .All(x => !x.Any()));
  }

  [Fact(DisplayName = "Any with `All()`")]
  public void AnyWithAll()
  {
    Assert.True(
      Enumerable.Range(1, 2)
        .Select(x => Enumerable.Repeat(x, 2))
        .Any(x => x.All(y => y == 1)));
  }

  [Fact(DisplayName = "`Append()`")]
  public void Append()
  {
    var x = Enumerable.Range(1, 2);
    x.Append(3);
    var y = x.Append(3);

    Assert.Equal([1, 2], x);
    Assert.Equal([1, 2, 3], y);
  }

  [Fact(DisplayName = "`Average()`")]
  public void Average()
  {
    Assert.Equal(
      2,
      new float?[] { 1, null, 3 }.Average());

    Assert.Equal(
      15.15m,
      Enumerable.Range(1, 10)
        .Select(x => new
        {
          Price = x < 3
            ? (decimal?)(x * 10.10m)
            : null,
        })
        .Average(x => x.Price));
  }

  [Fact(DisplayName = "`Cast()`: numbers")]
  public void Cast_Numbers()
  {
    var al = new ArrayList() { 1, 2, 3 };

    Assert.Equal(
      [1, 2, 3],
      al.Cast<object>().ToArray());

    Assert.Equal(
      [1, 2, 3],
      al.Cast<int?>().ToArray());

    Assert.Throws<InvalidCastException>(() => al.Cast<float>().ToArray());
  }

  [Fact(DisplayName = "`Cast()`: classes")]
  public void Cast_Classes()
  {
    // SystemUser : User

    // SystemUser -> User
    Assert.IsType<User[]>(
      Enumerable.Range(1, 3)
        .Select(_ => new SystemUser())
        .Cast<User>()
        .ToArray());

    // User -> object
    Assert.IsType<object[]>(
      Enumerable.Range(1, 3)
        .Select(_ => new User())
        .Cast<object>()
        .ToArray());

    // User -> SystemUser
    Assert.Throws<InvalidCastException>(() =>
      Enumerable.Range(1, 3)
        .Select(_ => new User())
        .Cast<SystemUser>()
        .ToArray());

    // Anonymous type -> User
    Assert.Throws<InvalidCastException>(() =>
      Enumerable.Range(1, 3)
        .Select(_ => new { Email = "1" })
        .Cast<User>()
        .ToArray());
  }

  [Fact(DisplayName = "`Chunk()`")]
  public void Chunk()
  {
    var expected = new List<int[]>
    {
      new int[] { 1, 2 },
      new int[] { 3, 4 },
    };

    var actual = Enumerable.Range(1, 4)
      .Chunk(2)
      .ToList();

    Assert.Equal(expected, actual);
  }

  [Fact(DisplayName = "`Concat()`")]
  public void Concat()
  {
    Assert.Equal(
      [1, 2, 3, 4],
      Enumerable.Range(1, 2).Concat(Enumerable.Range(3, 2)));

    var u1 = new User();
    var u2 = new User();
    Assert.Equal(
      [u1, u2],
      new[] { u1 }.Concat([u2]));
  }

  [Fact(DisplayName = "`Contains()`")]
  public void Contains()
  {
    var colors = new[] { "red" };

    Assert.True(colors.Contains("red"));
    Assert.True(colors.Contains("RED", StringComparer.OrdinalIgnoreCase));
    Assert.False(colors.Contains("RED"));

    var u1 = new User();
    var u2 = new User();
    var u3 = new User();
    var users = new[] { u1, u2 };

    Assert.True(users.Contains(u1));
    Assert.False(users.Contains(u3));
  }

  [Fact(DisplayName = "`Count()`")]
  public void Count()
  {
    Assert.Equal(
      3,
      Enumerable.Range(1, 3).Count());
  }

  [Fact(DisplayName = "`CountBy()`")]
  public void CountBy()
  {
    var expected = new Dictionary<ConsoleColor, int>
    {
      { ConsoleColor.Red, 1 },
      { ConsoleColor.Blue, 2 },
    };

    var actual = Enumerable.Range(1, 3)
      .Select(x => new
      {
        Color = x % 2 == 0
          ? ConsoleColor.Red
          : ConsoleColor.Blue,
      })
      .CountBy(x => x.Color)
      .ToDictionary();

    Assert.Equal(expected, actual);
  }

  [Fact(DisplayName = "`DefaultIfEmpty()`")]
  public void DefaultIfEmpty()
  {
    Assert.Equal(
      [ConsoleColor.Black],
      Array.Empty<ConsoleColor>().DefaultIfEmpty());

    Assert.Equal(
      [ConsoleColor.Red],
      Array.Empty<ConsoleColor>().DefaultIfEmpty(ConsoleColor.Red));
  }

  [Fact(DisplayName = "`Distinct()`")]
  public void Distinct()
  {
    {
      var src = Enumerable.Range(1, 3)
        .Select(n =>
          n % 2 != 0
          ? ConsoleColor.Red
          : ConsoleColor.Blue);
      var expected = new ConsoleColor[]
      {
        ConsoleColor.Red,
        ConsoleColor.Blue,
      };

      var actual = src.Distinct();

      Assert.Equal(expected, actual);
    }

    {
      var src = Enumerable.Range(1, 3)
        .Select(n => new
        {
          Color = n % 2 != 0
            ? ConsoleColor.Red
            : ConsoleColor.Blue,
        });
      var expected = new ConsoleColor[]
      {
        ConsoleColor.Red,
        ConsoleColor.Blue,
      };

      var actual = src.Distinct().Select(x => x.Color);

      Assert.Equal(expected, actual);
    }

    {
      var src = Enumerable.Range(1, 3)
        .Select(n => new
        {
          Id = n,
          Color = n % 2 != 0
            ? ConsoleColor.Red
            : ConsoleColor.Blue,
        });
      var expected = new ConsoleColor[]
      {
        ConsoleColor.Red,
        ConsoleColor.Blue,
        ConsoleColor.Red,
      };

      var actual = src.Distinct().Select(x => x.Color);

      Assert.Equal(expected, actual);
    }
  }

  [Fact(DisplayName = "`DistinctBy()`")]
  public void DistinctBy()
  {
    var src = Enumerable.Range(1, 3)
      .Select(n => new
      {
        Id = n,
        Color = n % 2 != 0
          ? ConsoleColor.Red
          : ConsoleColor.Blue,
      });
    var expected = new object[]
    {
      new { Id = 1, Color = ConsoleColor.Red },
      new { Id = 2, Color = ConsoleColor.Blue },
    };

    var actual = src.DistinctBy(x => x.Color).ToArray();

    Assert.Equal(expected, actual);
  }

  [Fact(DisplayName = "`ElementAt()`")]
  public void ElementAt()
  {
    var users = Enumerable.Range(1, 3)
      .Select(_ => new User())
      .ToArray();

    for (var i = 0; i < users.Length; i++)
    {
      Assert.Equal(users[i], users.ElementAt(i));
    }

    Assert.Throws<ArgumentOutOfRangeException>(() => users.ElementAt(-1));
  }

  [Fact(DisplayName = "`ElementAtOrDefault()`")]
  public void ElementAtOrDefault()
  {
    var users = Enumerable.Range(1, 3)
      .Select(_ => new User())
      .ToArray();

    for (var i = 0; i < users.Length; i++)
    {
      Assert.Equal(users[i], users.ElementAtOrDefault(i));
    }

    Assert.Null(users.ElementAtOrDefault(-1));
    Assert.Equal(0, Enumerable.Range(1, 3).ElementAtOrDefault(-1));
  }

  [Fact(DisplayName = "`Except()`")]
  public void Except()
  {
    var s1 = new int[] { 1, 2, 3, 4 };
    var s2 = new int[] { 3, 4, 5, 6 };
    var expected1 = new int[] { 1, 2 };
    var expected2 = new int[] { 5, 6 };

    var actual1 = Enumerable.Except(s1, s2);
    var actual2 = Enumerable.Except(s2, s1);

    Assert.Equal(expected1, actual1);
    Assert.Equal(expected2, actual2);
  }

  [Fact(DisplayName = "`ExceptBy()`")]
  public void ExceptBy()
  {
    var s1 = new Ball[]
    {
      new() { Color = ConsoleColor.Red },
      new() { Color = ConsoleColor.Green },
      new() { Color = ConsoleColor.Blue },
    };
    var s2 = new Ball[]
    {
      new() { Color = ConsoleColor.Green },
      new() { Color = ConsoleColor.Blue },
      new() { Color = ConsoleColor.Yellow },
    };

    var expected1 = new Ball[] { s1.First() };
    var expected2 = new Ball[] { s2.Last() };

    var actual1 = s1.ExceptBy(s2.Select(s => s.Color), k => k.Color);
    var actual2 = s2.ExceptBy(s1.Select(s => s.Color), k => k.Color);

    Assert.Equal(expected1, actual1);
    Assert.Equal(expected2, actual2);
  }

  [Fact(DisplayName = "`First()`")]
  public void First()
  {
    var r = new Ball { Color = ConsoleColor.Red };
    var g = new Ball { Color = ConsoleColor.Green };
    var b = new Ball { Color = ConsoleColor.Blue };
    var balls = new[] { r, g, b };

    Assert.Equal(r, balls.First());
    Assert.Equal(b, balls.First(x => x.Color == ConsoleColor.Blue));

    Assert.Throws<InvalidOperationException>(() => Enumerable.Empty<Ball>().First());
    Assert.Throws<InvalidOperationException>(() => balls.First(x => x.Color == ConsoleColor.Magenta));
  }

  [Fact(DisplayName = "`FirstOrDefault()`")]
  public void FirstOrDefault()
  {
    Assert.Equal(0, Enumerable.Empty<int>().FirstOrDefault());
    Assert.Equal(1, Enumerable.Empty<int>().FirstOrDefault(1));
    Assert.Equal(0, Enumerable.Empty<int>().FirstOrDefault(x => true));
    Assert.Equal(1, Enumerable.Empty<int>().FirstOrDefault(x => true, 1));

    var user = new User();
    Assert.Equal(null, Enumerable.Empty<User>().FirstOrDefault());
    Assert.Equal(user, Enumerable.Empty<User>().FirstOrDefault(user));
    Assert.Equal(null, Enumerable.Empty<User>().FirstOrDefault(x => true));
    Assert.Equal(user, Enumerable.Empty<User>().FirstOrDefault(x => true, user));
  }

  [Fact(DisplayName = "`GroupBy()`")]
  public void GroupBy()
  {
    var deps = new List<Department>
    {
      new() { Id = 1 },
      new() { Id = 2 },
    };
    var users = new List<User>
    {
      new() { Id = "guid-0001", Department = deps[0] },
      new() { Id = "guid-0002", Department = deps[0] },
      new() { Id = "guid-0003", Department = deps[1] },
      new() { Id = "guid-0004", Department = deps[1] },
    };

    var groups = users.GroupBy(u => u.Department);

    Assert.Equal(2, groups.Count());
    Assert.All(groups, (group, i) =>
    {
      Assert.Equal(deps[i], group.Key);
      Assert.Equal(2, group.Count());
      Assert.Equal(users.Where(u => u.Department == group.Key), group);
    });

    foreach (var group in groups)
    {
      output.WriteLine($"group id: {group.Key.Id}");
      foreach (var groupItem in group)
      {
        output.WriteLine($"  item id: {groupItem.Id}");
      }
    }
  }

  [Fact(DisplayName = "`GroupBy()`: element selector")]
  public void GroupBy_ElementSelector()
  {
    var deps = new List<Department>
    {
      new() { Id = 1 },
      new() { Id = 2 },
    };
    var users = new List<User>
    {
      new() { Id = "guid-0001", Department = deps[0] },
      new() { Id = "guid-0002", Department = deps[0] },
      new() { Id = "guid-0003", Department = deps[1] },
      new() { Id = "guid-0004", Department = deps[1] },
    };

    var groups = users.GroupBy(u => u.Department, u => u.Id);

    Assert.Equal(2, groups.Count());
    Assert.All(groups, (group, i) =>
    {
      Assert.Equal(deps[i], group.Key);
      Assert.Equal(2, group.Count());

      Assert.Equal(
        users
          .Where(u => u.Department == group.Key)
          .Select(u => u.Id),
        group);
    });

    foreach (var group in groups)
    {
      output.WriteLine($"group id: {group.Key.Id}");
      foreach (var groupItem in group)
      {
        output.WriteLine($"  item: {groupItem}");
      }
    }
  }

  [Fact(DisplayName = "`GroupBy()`: result selector")]
  public void GroupBy_ResultSelector()
  {
    var deps = new List<Department>
    {
      new() { Id = 1 },
      new() { Id = 2 },
    };
    var users = new List<User>
    {
      new() { Id = "guid-0001", Department = deps[0] },
      new() { Id = "guid-0002", Department = deps[0] },
      new() { Id = "guid-0003", Department = deps[1] },
      new() { Id = "guid-0004", Department = deps[1] },
      new() { Id = "guid-0005", Department = deps[1] },
    };

    var groups = users.GroupBy(u => u.Department, u => u.Id, (k, v) => new
    {
      DepId = k.Id,
      UserCount = v.Count(),
    });

    Assert.Equal(2, groups.Count());
    Assert.All(groups, (group, i) =>
    {
      Assert.Equal(deps[i].Id, group.DepId);
      Assert.Equal(
        users.Count(u => u.Department.Id == group.DepId),
        group.UserCount);
    });

    foreach (var group in groups)
    {
      output.WriteLine($"Dep ID: {group.DepId}, User Count: {group.UserCount}");
    }
  }

  [Fact(DisplayName = "`GroupJoin()`")]
  public void GroupJoin()
  {
    var deps = new List<Department>
    {
      new() { Id = 1 },
      new() { Id = 2 },
      new() { Id = 3 },
    };
    var users = new List<User>
    {
      new() { Id = "guid-0001", Department = deps[0] },
      new() { Id = "guid-0002", Department = deps[0] },
      new() { Id = "guid-0003", Department = deps[1] },
      new() { Id = "guid-0004", Department = deps[1] },
      new() { Id = "guid-0005", Department = deps[1] },
    };
    var expectedDepUserCount1 = new[]
    {
      new { DepId = 1, UserCount = 2 },
      new { DepId = 2, UserCount = 3 },
    };
    var expectedDepUserCount2 = new[]
    {
      new { DepId = 1, UserCount = 2 },
      new { DepId = 2, UserCount = 3 },
      new { DepId = 3, UserCount = 0 },
    };

    var depUserCount1 = deps
      .Join(users, d => d, u => u.Department, (d, u) => new { d, u }) // inner join
      .GroupBy(x => x.d, x => x.u, (d, u) => new
      {
        DepId = d.Id,
        UserCount = u.Count(),
      })
      .ToArray();

    var depUserCount2 = deps
      .GroupJoin(users, d => d, u => u.Department, (d, u) => new // left join
      {
        DepId = d.Id,
        UserCount = u.Count(),
      })
      .ToArray();

    Assert.Equal(expectedDepUserCount1, depUserCount1);
    Assert.Equal(expectedDepUserCount2, depUserCount2);

    foreach (var item in depUserCount2)
    {
      output.WriteLine($"DepId: {item.DepId}, User Count: {item.UserCount}");
    }
  }

  [Fact(DisplayName = "`Index()`")]
  public void Index()
  {
    var users = Enumerable
      .Repeat(1, 3)
      .Select(_ => new User())
      .ToArray();

    var indexedUsers = users.Index().ToArray();

    Assert.Equal(3, indexedUsers.Length);
    Assert.All(indexedUsers, (pair, i) =>
    {
      Assert.Equal(i, pair.Index);
      Assert.Equal(users[i], pair.Item);
    });
  }

  [Fact(DisplayName = "`Intersect()`")]
  public void Intersect()
  {
    var balls1 = new Ball[]
    {
      new() { Color = ConsoleColor.Red },
      new() { Color = ConsoleColor.Green },
    };
    var balls2 = new Ball[]
    {
      new() { Color = ConsoleColor.Blue },
      new() { Color = ConsoleColor.Green },
    };

    Assert.Equal(
      new[] { 2 },
      new[] { 1, 2 }.Intersect(new[] { 2, 3 }));

    Assert.Empty(balls1.Intersect(balls2));

    Assert.Equal(
      [balls1[1]],
      balls1.Intersect(balls2, new BallColorEqualityComparer()));

    Assert.Equal(
      [balls1[1]],
      balls1.Intersect(balls2, new BallPayloadEqualityComparer()));
  }

  [Fact(DisplayName = "`IntersectBy()`")]
  public void IntersectBy()
  {
    var balls1 = new Ball[]
    {
      new() { Color = ConsoleColor.Red },
      new() { Color = ConsoleColor.Green },
    };
    var balls2 = new Ball[]
    {
      new() { Color = ConsoleColor.Blue },
      new() { Color = ConsoleColor.Green },
    };

    Assert.Equal(
      [balls1[1]],
      balls1.IntersectBy(balls2.Select(b => b.Color), b => b.Color));
  }

  [Fact(DisplayName = "`Join()`")]
  public void Join()
  {
    var deps = new List<Department>
    {
      new() { Id = 1 },
      new() { Id = 2 },
      new() { Id = 3 },
    };
    var users = new List<User>
    {
      new() { Id = "guid-0001", Department = deps[0] },
      new() { Id = "guid-0002", Department = deps[0] },
      new() { Id = "guid-0003", Department = deps[1] },
      new() { Id = "guid-0004", Department = deps[1] },
      new() { Id = "guid-0005", Department = deps[1] },
    };

    var depUserIdPairs = deps.Join(
      users,
      d => d,
      u => u.Department,
      (d, u) => $"{d.Id}.{u.Id}");

    Assert.Equal(
      [
        "1.guid-0001",
        "1.guid-0002",
        "2.guid-0003",
        "2.guid-0004",
        "2.guid-0005",
      ],
      depUserIdPairs);
  }

  [Fact(DisplayName = "`Last()`")]
  public void Last()
  {
    var r = new Ball { Color = ConsoleColor.Red };
    var g = new Ball { Color = ConsoleColor.Green };
    var b = new Ball { Color = ConsoleColor.Blue };
    var balls = new[] { r, g, b };

    Assert.Equal(b, balls.Last());
    Assert.Equal(g, balls.Last(x => x.Color == ConsoleColor.Green));

    Assert.Throws<InvalidOperationException>(() => Enumerable.Empty<Ball>().Last());
    Assert.Throws<InvalidOperationException>(() => balls.Last(x => x.Color == ConsoleColor.Magenta));
  }

  [Fact(DisplayName = "`LastOrDefault()`")]
  public void LastOrDefault()
  {
    Assert.Equal(0, Enumerable.Empty<int>().LastOrDefault());
    Assert.Equal(1, Enumerable.Empty<int>().LastOrDefault(1));
    Assert.Equal(0, Enumerable.Empty<int>().LastOrDefault(x => true));
    Assert.Equal(1, Enumerable.Empty<int>().LastOrDefault(x => true, 1));

    var user = new User();
    Assert.Equal(null, Enumerable.Empty<User>().LastOrDefault());
    Assert.Equal(user, Enumerable.Empty<User>().LastOrDefault(user));
    Assert.Equal(null, Enumerable.Empty<User>().LastOrDefault(x => true));
    Assert.Equal(user, Enumerable.Empty<User>().LastOrDefault(x => true, user));
  }

  [Fact(DisplayName = "`Max()`")]
  public void Max()
  {
    var balls = new Ball[] // Ball : IComparable<Ball>
    {
      new() { Color = ConsoleColor.Red },
      new() { Color = ConsoleColor.Green },
      new() { Color = ConsoleColor.Blue },
    };

    Assert.Equal(
      balls[0],
      balls.Max());

    Assert.Equal(
      balls[0].Color,
      balls.Max(b => b.Color));

    Assert.Equal(
      balls[0],
      balls.Max(new BallColorStringComparer()));
  }

  [Fact(DisplayName = "`Max(): exceptions`")]
  public void MaxExceptions()
  {
    var ex1 = Assert.Throws<InvalidOperationException>(() => Enumerable.Empty<int>().Max());
    Assert.Equal("Sequence contains no elements", ex1.Message);

    var u1 = new User();
    var u2 = new User();
    var ex2 = Assert.Throws<ArgumentException>(() => new User[] { u1, u2 }.Max());
    Assert.Equal("At least one object must implement IComparable.", ex2.Message);
    Assert.Null(Enumerable.Empty<User>().Max());
    Assert.Equal(u1, new User[] { u1 }.Max());
  }

  [Fact(DisplayName = "`MaxBy()`")]
  public void MaxBy()
  {
    var balls = new Ball[] // Ball : IComparable<Ball>
    {
      new() { Color = ConsoleColor.Red },
      new() { Color = ConsoleColor.Green },
      new() { Color = ConsoleColor.Blue },
    };
    var users = balls
      .Select(b => new User { Ball = b })
      .ToArray();

    Assert.Equal(
      users[0],
      users.MaxBy(u => u.Ball));

    Assert.Equal(
      users[0],
      users.MaxBy(u => u.Ball, new BallColorStringComparer()));
  }

  [Fact(DisplayName = "`MaxBy(): exceptions`")]
  public void MaxByExceptions()
  {
    var ex1 = Assert.Throws<InvalidOperationException>(() => Enumerable.Empty<int>().MaxBy(x => x % 2 == 0));
    Assert.Equal("Sequence contains no elements", ex1.Message);

    var u1 = new User() { Department = new() };
    var u2 = new User() { Department = new() };
    var ex2 = Assert.Throws<ArgumentException>(() => new User[] { u1, u2 }.MaxBy(u => u.Department));
    Assert.Equal("At least one object must implement IComparable.", ex2.Message);
    Assert.Null(Enumerable.Empty<User>().Max());
    Assert.Equal(u1, new User[] { u1 }.Max());
  }

  [Fact(DisplayName = "`OfType()`")]
  public void OfType()
  {
    var al = new ArrayList() { 1, 2f, "3" };

    Assert.Equal(
      [1, 2f, "3"],
      al.OfType<object>());

    Assert.Equal(
      [1],
      al.OfType<int?>());

    Assert.Equal(
      [2f],
      al.OfType<float>());

    Assert.Equal(
      ["3"],
      al.OfType<string>());
  }

  [Fact(DisplayName = "`Order()`")]
  public void Order()
  {
    Assert.Equal(
      [1, 2, 3],
      new[] { 3, 1, 2 }.Order());

    var src = new Ball[] // Ball : IComparer<Ball>
    {
      new() { Color = ConsoleColor.Red }, // 12
      new() { Color = ConsoleColor.Green }, // 10
      new() { Color = ConsoleColor.Blue }, // 9
    };
    var expected = src.Reverse().ToArray();

    Assert.Equal(
      expected,
      src.Order());
    Assert.Equal(
      expected,
      src.Order(new BallColorStringComparer()));
  }

  [Fact(DisplayName = "`OrderBy()`")]
  public void OrderBy()
  {
    var src = new Ball[]
    {
      new() { Color = ConsoleColor.Red },
      new() { Color = ConsoleColor.Green },
      new() { Color = ConsoleColor.Blue },
      new() { Color = ConsoleColor.Red },
      new() { Color = ConsoleColor.Green },
      new() { Color = ConsoleColor.Blue },
    };
    var expected = new Ball[]
    {
      new() { Color = ConsoleColor.Blue },
      new() { Color = ConsoleColor.Blue },
      new() { Color = ConsoleColor.Green },
      new() { Color = ConsoleColor.Green },
      new() { Color = ConsoleColor.Red },
      new() { Color = ConsoleColor.Red },
    };

    var actual = src.OrderBy(b => b.Color);

    Assert.Equal(
      expected.Select(b => b.Color),
      actual.Select(b => b.Color));
  }

  [Fact(DisplayName = "`OrderDescending()`")]
  public void OrderDescending()
  {
    Assert.Equal(
      [3, 2, 1],
      new[] { 2, 1, 3 }.OrderDescending());

    var src = new Ball[] // Ball : IComparer<Ball>
    {
      new() { Color = ConsoleColor.Blue }, // 9
      new() { Color = ConsoleColor.Green }, // 10
      new() { Color = ConsoleColor.Red }, // 12
    };
    var expected = src.Reverse().ToArray();

    Assert.Equal(
      expected,
      src.OrderDescending());
    Assert.Equal(
      expected,
      src.OrderDescending(new BallColorStringComparer()));
  }

  [Fact(DisplayName = "`OrderByDescending()`")]
  public void OrderByDescending()
  {
    var src = new Ball[]
    {
      new() { Color = ConsoleColor.Red },
      new() { Color = ConsoleColor.Green },
      new() { Color = ConsoleColor.Blue },
      new() { Color = ConsoleColor.Red },
      new() { Color = ConsoleColor.Green },
      new() { Color = ConsoleColor.Blue },
    };
    var expected = new Ball[]
    {
      new() { Color = ConsoleColor.Red },
      new() { Color = ConsoleColor.Red },
      new() { Color = ConsoleColor.Green },
      new() { Color = ConsoleColor.Green },
      new() { Color = ConsoleColor.Blue },
      new() { Color = ConsoleColor.Blue },
    };

    var actual = src.OrderByDescending(b => b.Color);

    Assert.Equal(
      expected.Select(b => b.Color),
      actual.Select(b => b.Color));
  }

  [Fact(DisplayName = "`Prepend()`")]
  public void Prepend()
  {
    var x = Enumerable.Range(1, 2);
    x.Prepend(0);
    var y = x.Prepend(0);

    Assert.Equal([1, 2], x);
    Assert.Equal([0, 1, 2], y);
  }

  [Fact(DisplayName = "`Reverse()`")]
  public void Reverse()
  {
    Assert.Equal(
      [3, 2, 1],
      new[] { 1, 2, 3 }.Reverse());
  }

  [Fact(DisplayName = "`Select()`: indexing")]
  public void SelectIndexing()
  {
    Assert.Equal(
      [0, 1, 2],
      Enumerable.Repeat(new Ball(), 3).Select((b, i) => i));
  }
}
