<div align="center">
  <img src="title.png">
</div>

# 🧪 DEMOS

## TL;DR
Examples, Experiments, Research<br>
Look. Try. Add.

## About

👋 Welcome! **This is the personal repository of a Full-Stack developer (.NET + Angular)**, where I collect everything I’ve learned or discovered in real projects.

🔬 I don’t just read theory – I want to see **how it works in practice**! Here you’ll find hands-on examples of basic tools and practical solutions to complex problems you can try yourself.

💡 Even simple concepts are easy to forget, and revisiting them often means writing small console projects. To save time and stay organized, I keep everything in one structured place.

🤝 I’m open to collaboration!

📦 This repository is my personal experience vault, which will continue to grow. New examples, ideas, and practices appear as I learn and work on real projects.

**Small for now 🌱 but growing with each step toward something greater 🌳!**

## .NET

> **Note:** Tests containing `spin` or `yield` may fail depending on the CPU.


<details>
<summary>Tasks: Synchronization</summary>

1. [`lock` within task][tasks-synchronization-1]
2. [task within `lock`][tasks-synchronization-2]
3. [Lock via semaphore][tasks-synchronization-3]

[tasks-synchronization-1]:Neomaster.Demos.Tests/Tasks/TasksSyncUnitDemos.cs#L8
[tasks-synchronization-2]:Neomaster.Demos.Tests/Tasks/TasksSyncUnitDemos.cs#L53
[tasks-synchronization-3]:Neomaster.Demos.Tests/Tasks/TasksSyncUnitDemos.cs#L63

</details>

<details>
<summary>Tasks: Features</summary>

1. [Timer: Callback][tasks-features-1]
2. [Timer: AutoReset: false][tasks-features-2]
3. [Timer: Alarms][tasks-features-3]
4. [Parallel: `For()`][tasks-features-4]
5. [Parallel: `Stop()`][tasks-features-5]
6. [Parallel: `Break()`][tasks-features-6]
7. [Parallel: Local Var][tasks-features-7]
8. [Parallel: Exception][tasks-features-8]
9. [Parallel: `ParallelOptions`][tasks-features-9]
10. [Parallel: State Checks][tasks-features-10]
11. [Parallel: `Foreach()`][tasks-features-11]
12. [Parallel: `Invoke()`][tasks-features-12]

[tasks-features-1]:Neomaster.Demos.Tests/Tasks/TasksFeaturesUnitDemos.cs#L10
[tasks-features-2]:Neomaster.Demos.Tests/Tasks/TasksFeaturesUnitDemos.cs#L37
[tasks-features-3]:Neomaster.Demos.Tests/Tasks/TasksFeaturesUnitDemos.cs#L61
[tasks-features-4]:Neomaster.Demos.Tests/Tasks/TasksFeaturesUnitDemos.cs#L110
[tasks-features-5]:Neomaster.Demos.Tests/Tasks/TasksFeaturesUnitDemos.cs#L134
[tasks-features-6]:Neomaster.Demos.Tests/Tasks/TasksFeaturesUnitDemos.cs#L170
[tasks-features-7]:Neomaster.Demos.Tests/Tasks/TasksFeaturesUnitDemos.cs#L207
[tasks-features-8]:Neomaster.Demos.Tests/Tasks/TasksFeaturesUnitDemos.cs#L236
[tasks-features-9]:Neomaster.Demos.Tests/Tasks/TasksFeaturesUnitDemos.cs#L280
[tasks-features-10]:Neomaster.Demos.Tests/Tasks/TasksFeaturesUnitDemos.cs#L298
[tasks-features-11]:Neomaster.Demos.Tests/Tasks/TasksFeaturesUnitDemos.cs#L325
[tasks-features-12]:Neomaster.Demos.Tests/Tasks/TasksFeaturesUnitDemos.cs#L360

</details>

<details>
<summary>Tasks: Activity Types</summary>

1. [`AlarmInfo`][tasks-activity-types-1]
2. [`CustomTaskScheduler`][tasks-activity-types-2]
3. [`DefaultSyncCtx`][tasks-activity-types-3]
4. [`ExternalEventSource`][tasks-activity-types-4]
5. [`ExternalEventSourceAdapter`][tasks-activity-types-5]
6. [`TaskExtensions`][tasks-activity-types-6]
7. [`TimeSpanAwaiter`][tasks-activity-types-7]
8. [`UISyncCtx`][tasks-activity-types-8]

[tasks-activity-types-1]:Neomaster.Demos.Tests/Tasks/ActivityTypes/AlarmInfo.cs
[tasks-activity-types-2]:Neomaster.Demos.Tests/Tasks/ActivityTypes/CustomTaskScheduler.cs
[tasks-activity-types-3]:Neomaster.Demos.Tests/Tasks/ActivityTypes/DefaultSyncCtx.cs
[tasks-activity-types-4]:Neomaster.Demos.Tests/Tasks/ActivityTypes/ExternalEventSource.cs
[tasks-activity-types-5]:Neomaster.Demos.Tests/Tasks/ActivityTypes/ExternalEventSourceAdapter.cs
[tasks-activity-types-6]:Neomaster.Demos.Tests/Tasks/ActivityTypes/TaskExtensions.cs
[tasks-activity-types-7]:Neomaster.Demos.Tests/Tasks/ActivityTypes/TimeSpanAwaiter.cs
[tasks-activity-types-8]:Neomaster.Demos.Tests/Tasks/ActivityTypes/UISyncCtx.cs

</details>

<details>
<summary>LINQ: Expression Trees</summary>

1. [Tree structure: view][linq-expression-trees-1]
2. [Tree structure: create: left operand via `Expression.MakeMemberAccess`][linq-expression-trees-2]
3. [Tree structure: create: left operand via `Expression.Property`][linq-expression-trees-3]
4. [Tree structure: create: 3 levels][linq-expression-trees-4]
5. [Lambda: create `Func1`][linq-expression-trees-5]
6. [Lambda: create `Func2`][linq-expression-trees-6]
7. [Lambda: parameter order][linq-expression-trees-7]
8. [Lambda: info][linq-expression-trees-8]
9. [Lambda: view with named parameters][linq-expression-trees-9]
10. [Lambda: typed vs untyped][linq-expression-trees-10]
11. [Lambda: `DynamicInvoke()`: dynamic `Func`][linq-expression-trees-11]
12. [Lambda: `DynamicInvoke()`: dynamic Add][linq-expression-trees-12]
13. [`ExpressionType`: list of all][linq-expression-trees-13]
14. [Debug view][linq-expression-trees-14]
15. [Debug view: lambda][linq-expression-trees-15]
16. [`Reduce()`][linq-expression-trees-16]
17. [`ReduceAndCheck()`: reducible][linq-expression-trees-17]
18. [`ReduceAndCheck()`: not reducible][linq-expression-trees-18]
19. [`ReduceAndCheck()`: prevent return `null` and `this`][linq-expression-trees-19]
20. [`ReduceExtensions()`: builtin root][linq-expression-trees-20]
21. [`ReduceExtensions()`: custom root: reducible][linq-expression-trees-21]
22. [`ReduceExtensions()`: custom root: not reducible][linq-expression-trees-22]
23. [`IsByRef`: struct parameter: with `ref` modifier][linq-expression-trees-23]
24. [`IsByRef`: reference parameter: without `ref` modifier][linq-expression-trees-24]
25. [`IsByRef`: reference parameter: with `ref` modifier][linq-expression-trees-25]
26. [Visitor: root][linq-expression-trees-26]
27. [Visitor: tree: immutable][linq-expression-trees-27]
28. [Visitor: tree: with mutable child node][linq-expression-trees-28]
29. [`Expression.Invoke`][linq-expression-trees-29]
30. [`Expression.Call`: instance method][linq-expression-trees-30]
31. [`Expression.Call`: static method][linq-expression-trees-31]
32. [`Expression.New`][linq-expression-trees-32]
33. [`Expression.MemberInit`, `Expression.Bind`][linq-expression-trees-33]
34. [`Expression.MemberBind`][linq-expression-trees-34]
35. [`Expression.Quote`: lambda returns lambda][linq-expression-trees-35]
36. [`Expression.Quote`: lambda returns lambda: in DB provider][linq-expression-trees-36]
37. [`Expression.Assign`][linq-expression-trees-37]
38. [`Expression.Block`, `Expression.Variable`: `Swap(x, y)`][linq-expression-trees-38]
39. [`Expression.Block` returns last expression result][linq-expression-trees-39]
40. [Conditional operators][linq-expression-trees-40]
41. [`Expression.Throw`][linq-expression-trees-41]
42. [`Expression.Goto`, `Expression.Label`: empty][linq-expression-trees-42]
43. [`Expression.Goto`, `Expression.Label`: instruction][linq-expression-trees-43]
44. [`Expression.Goto`, `Expression.Label`: return value][linq-expression-trees-44]
45. [`Expression.Return` like `Expression.Goto`][linq-expression-trees-45]
46. [`Expression.Return` vs `Expression.Goto`: semantic difference][linq-expression-trees-46]
47. [`Expression.Return` vs `Expression.Goto`: call via kind][linq-expression-trees-47]
48. [`Expression.Loop`, `Expression.Break`: power to 10][linq-expression-trees-48]
49. [`Expression.Loop`, `Expression.Break`: select even][linq-expression-trees-49]
50. [`GotoExpressionKind`: list of all][linq-expression-trees-50]
51. [`Expression.TryCatchFinally`][linq-expression-trees-51]
52. [Reverse Polish Notation][linq-expression-trees-52]
53. [Auto-mapper][linq-expression-trees-53]
54. [SQL generation][linq-expression-trees-54]

[linq-expression-trees-1]: Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L16
[linq-expression-trees-2]: Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L45
[linq-expression-trees-3]: Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L82
[linq-expression-trees-4]: Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L118
[linq-expression-trees-5]: Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L155
[linq-expression-trees-6]: Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L170
[linq-expression-trees-7]: Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L195
[linq-expression-trees-8]: Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L210
[linq-expression-trees-9]: Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L247
[linq-expression-trees-10]: Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L277
[linq-expression-trees-11]: Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L293
[linq-expression-trees-12]: Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L312
[linq-expression-trees-13]: Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L331
[linq-expression-trees-14]: Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L341
[linq-expression-trees-15]: Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L380
[linq-expression-trees-16]: Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L409
[linq-expression-trees-17]: Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L447
[linq-expression-trees-18]: Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L475
[linq-expression-trees-19]: Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L497
[linq-expression-trees-20]: Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L504
[linq-expression-trees-21]: Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L518
[linq-expression-trees-22]: Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L532
[linq-expression-trees-23]: Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L545
[linq-expression-trees-24]: Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L560
[linq-expression-trees-25]: Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L575
[linq-expression-trees-26]: Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L590
[linq-expression-trees-27]: Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L630
[linq-expression-trees-28]: Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L650
[linq-expression-trees-29]: Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L671
[linq-expression-trees-30]: Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L696
[linq-expression-trees-31]: Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L708
[linq-expression-trees-32]: Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L724
[linq-expression-trees-33]: Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L753
[linq-expression-trees-34]: Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L774
[linq-expression-trees-35]: Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L805
[linq-expression-trees-36]: Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L840
[linq-expression-trees-37]: Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L878
[linq-expression-trees-38]: Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L898
[linq-expression-trees-39]: Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L921
[linq-expression-trees-40]: Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L935
[linq-expression-trees-41]: Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L989
[linq-expression-trees-42]: Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L1023
[linq-expression-trees-43]: Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L1046
[linq-expression-trees-44]: Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L1068
[linq-expression-trees-45]: Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L1085
[linq-expression-trees-46]: Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L1101
[linq-expression-trees-47]: Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L1116
[linq-expression-trees-48]: Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L1143
[linq-expression-trees-49]: Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L1172
[linq-expression-trees-50]: Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L1211
[linq-expression-trees-51]: Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L1232
[linq-expression-trees-52]: Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L1256
[linq-expression-trees-53]: Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L1286
[linq-expression-trees-54]: Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L1327

</details>

<details>
<summary>LINQ: Activity Types</summary>

1. [`BrokenReducible`][linq-activity-types-1]
2. [`Department`][linq-activity-types-2]
3. [`ExpressionExtensions`][linq-activity-types-3]
4. [`IntAddVisitor`][linq-activity-types-4]
5. [`LinqExprDemoDbContext`][linq-activity-types-5]
6. [`LinqExprDemoDbContextFixture`][linq-activity-types-6]
7. [`ReducibleIntAdd`][linq-activity-types-7]
8. [`User`][linq-activity-types-8]
9. [`UserDto`][linq-activity-types-9]

[linq-activity-types-1]:Neomaster.Demos.Tests/LinqExpr/ActivityTypes/BrokenReducible.cs
[linq-activity-types-2]:Neomaster.Demos.Tests/LinqExpr/ActivityTypes/Department.cs
[linq-activity-types-3]:Neomaster.Demos.Tests/LinqExpr/ActivityTypes/ExpressionExtensions.cs
[linq-activity-types-4]:Neomaster.Demos.Tests/LinqExpr/ActivityTypes/IntAddVisitor.cs
[linq-activity-types-5]:Neomaster.Demos.Tests/LinqExpr/ActivityTypes/LinqExprDemoDbContext.cs
[linq-activity-types-6]:Neomaster.Demos.Tests/LinqExpr/ActivityTypes/LinqExprDemoDbContextFixture.cs
[linq-activity-types-7]:Neomaster.Demos.Tests/LinqExpr/ActivityTypes/ReducibleIntAdd.cs
[linq-activity-types-8]:Neomaster.Demos.Tests/LinqExpr/ActivityTypes/User.cs
[linq-activity-types-9]:Neomaster.Demos.Tests/LinqExpr/ActivityTypes/UserDto.cs

</details>

<details>
<summary>Archives: ZIP</summary>

1. [Create 1 root file][archives-zip-1]
2. [Create N root files][archives-zip-2]
3. [Create N folder files][archives-zip-3]
4. [Create Word doc][archives-zip-4]

[archives-zip-1]:Neomaster.Demos.Tests/Archives/ArchivesZipUnitDemos.cs#L15
[archives-zip-2]:Neomaster.Demos.Tests/Archives/ArchivesZipUnitDemos.cs#L27
[archives-zip-3]:Neomaster.Demos.Tests/Archives/ArchivesZipUnitDemos.cs#L43
[archives-zip-4]:Neomaster.Demos.Tests/Archives/ArchivesZipUnitDemos.cs#L59

</details>
