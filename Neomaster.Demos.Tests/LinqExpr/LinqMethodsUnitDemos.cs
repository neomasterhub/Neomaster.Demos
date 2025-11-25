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

  [Fact(DisplayName = "`Aggregate()`")]
  public void Aggregate()
  {
    var sum = Enumerable.Repeat(1, 100).Aggregate((sum, i) => sum + i);
    Assert.Equal(100, sum);
  }
}
