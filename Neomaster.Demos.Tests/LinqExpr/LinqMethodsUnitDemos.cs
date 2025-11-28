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
}
