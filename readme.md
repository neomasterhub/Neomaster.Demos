<div align="center">
  <img src="title.png">
</div>

# 🧪 DEMOS

## TL;DR
Examples, Experiments, Research<br>
Look. Try. Add.

## About

👋 Welcome! **This is the personal repository of a Full-Stack developer (.NET, C++, Angular)**, where I collect everything I’ve learned or discovered in real projects.

🔬 I don’t just read theory – I want to see **how it works in practice**! Here you’ll find hands-on examples of basic tools and practical solutions to complex problems you can try yourself.

💡 Even simple concepts are easy to forget, and revisiting them often means writing small console projects. To save time and stay organized, I keep everything in one structured place.

🤝 I’m open to collaboration!

📦 This repository is my personal experience vault, which will continue to grow. New examples, ideas, and practices appear as I learn and work on real projects.

**Small for now 🌱 but growing with each step toward something greater 🌳!**

## .NET

> **Note:** Tests containing `spin` or `yield` may fail depending on the CPU.

<details>

<summary><b>📦 Archives</b></summary>

#### Zip

1. [Create 1 root file][Neomaster.Demos.Tests_Archives_ArchivesZipUnitDemos.cs-1]
2. [Create N root files][Neomaster.Demos.Tests_Archives_ArchivesZipUnitDemos.cs-2]
3. [Create N folder files][Neomaster.Demos.Tests_Archives_ArchivesZipUnitDemos.cs-3]
4. [Create Word doc][Neomaster.Demos.Tests_Archives_ArchivesZipUnitDemos.cs-4]

[Neomaster.Demos.Tests_Archives_ArchivesZipUnitDemos.cs-1]:Neomaster.Demos.Tests/Archives/ArchivesZipUnitDemos.cs#L17
[Neomaster.Demos.Tests_Archives_ArchivesZipUnitDemos.cs-2]:Neomaster.Demos.Tests/Archives/ArchivesZipUnitDemos.cs#L29
[Neomaster.Demos.Tests_Archives_ArchivesZipUnitDemos.cs-3]:Neomaster.Demos.Tests/Archives/ArchivesZipUnitDemos.cs#L45
[Neomaster.Demos.Tests_Archives_ArchivesZipUnitDemos.cs-4]:Neomaster.Demos.Tests/Archives/ArchivesZipUnitDemos.cs#L61


</details>

<details>

<summary><b>🔀 Threads</b></summary>

#### 1. Fundamentals

1. [Create, Sleep, Join][Neomaster.Demos.Tests_Threads_ThreadsUnitDemos.cs-1]
2. [Create with arg][Neomaster.Demos.Tests_Threads_ThreadsUnitDemos.cs-2]
3. [Foreground][Neomaster.Demos.Tests_Threads_ThreadsUnitDemos.cs-3]
4. [Background][Neomaster.Demos.Tests_Threads_ThreadsUnitDemos.cs-4]
5. [Info, current thread instance][Neomaster.Demos.Tests_Threads_ThreadsUnitDemos.cs-5]
6. [Is alive][Neomaster.Demos.Tests_Threads_ThreadsUnitDemos.cs-6]
7. [Join with timeout][Neomaster.Demos.Tests_Threads_ThreadsUnitDemos.cs-7]
8. [Abort][Neomaster.Demos.Tests_Threads_ThreadsUnitDemos.cs-8]
9. [Affinity (run parameter)][Neomaster.Demos.Tests_Threads_ThreadsUnitDemos.cs-9]
10. [Affinity (programmatically)][Neomaster.Demos.Tests_Threads_ThreadsUnitDemos.cs-10]
11. [Suspend, Resume][Neomaster.Demos.Tests_Threads_ThreadsUnitDemos.cs-11]
12. [Suspend, Resume: Tick Tock][Neomaster.Demos.Tests_Threads_ThreadsUnitDemos.cs-12]
13. [Abort in Core via `CancellationTokenSource`][Neomaster.Demos.Tests_Threads_ThreadsUnitDemos.cs-13]
14. [Suspend-Resume in Core via `ManualResetEventSlim`][Neomaster.Demos.Tests_Threads_ThreadsUnitDemos.cs-14]
15. [Tick Tock in Core][Neomaster.Demos.Tests_Threads_ThreadsUnitDemos.cs-15]

[Neomaster.Demos.Tests_Threads_ThreadsUnitDemos.cs-1]:Neomaster.Demos.Tests/Threads/ThreadsUnitDemos.cs#L20
[Neomaster.Demos.Tests_Threads_ThreadsUnitDemos.cs-2]:Neomaster.Demos.Tests/Threads/ThreadsUnitDemos.cs#L38
[Neomaster.Demos.Tests_Threads_ThreadsUnitDemos.cs-3]:Neomaster.Demos.Apps/Neomaster.Demos.Apps.Threads.Foreground/Program.cs
[Neomaster.Demos.Tests_Threads_ThreadsUnitDemos.cs-4]:Neomaster.Demos.Apps/Neomaster.Demos.Apps.Threads.Background/Program.cs
[Neomaster.Demos.Tests_Threads_ThreadsUnitDemos.cs-5]:Neomaster.Demos.Tests/Threads/ThreadsUnitDemos.cs#L60
[Neomaster.Demos.Tests_Threads_ThreadsUnitDemos.cs-6]:Neomaster.Demos.Tests/Threads/ThreadsUnitDemos.cs#L86
[Neomaster.Demos.Tests_Threads_ThreadsUnitDemos.cs-7]:Neomaster.Demos.Tests/Threads/ThreadsUnitDemos.cs#L109
[Neomaster.Demos.Tests_Threads_ThreadsUnitDemos.cs-8]:Neomaster.Demos.Apps/Neomaster.Demos.Apps.Threads.AbortThread/Program.cs
[Neomaster.Demos.Tests_Threads_ThreadsUnitDemos.cs-9]:Neomaster.Demos.Apps/Neomaster.Demos.Apps.Threads.AffinityParameterized/Program.cs
[Neomaster.Demos.Tests_Threads_ThreadsUnitDemos.cs-10]:Neomaster.Demos.Apps/Neomaster.Demos.Apps.Threads.AffinityProgrammed/Program.cs
[Neomaster.Demos.Tests_Threads_ThreadsUnitDemos.cs-11]:Neomaster.Demos.Apps/Neomaster.Demos.Apps.Threads.SuspendResume/Program.cs
[Neomaster.Demos.Tests_Threads_ThreadsUnitDemos.cs-12]:Neomaster.Demos.Apps/Neomaster.Demos.Apps.Threads.SuspendResumeTickTock/Program.cs
[Neomaster.Demos.Tests_Threads_ThreadsUnitDemos.cs-13]:Neomaster.Demos.Tests/Threads/ThreadsUnitDemos.cs#L132
[Neomaster.Demos.Tests_Threads_ThreadsUnitDemos.cs-14]:Neomaster.Demos.Tests/Threads/ThreadsUnitDemos.cs#L159
[Neomaster.Demos.Tests_Threads_ThreadsUnitDemos.cs-15]:Neomaster.Demos.Tests/Threads/ThreadsUnitDemos.cs#L180


#### 2. Synchronization

1. [`lock()`][Neomaster.Demos.Tests_Threads_ThreadsSyncUnitDemos.cs-1]
2. [Monitor.Enter - Monitor.Exit][Neomaster.Demos.Tests_Threads_ThreadsSyncUnitDemos.cs-2]
3. [Wait Queue][Neomaster.Demos.Tests_Threads_ThreadsSyncUnitDemos.cs-3]
4. [Monitor.PulseAll][Neomaster.Demos.Tests_Threads_ThreadsSyncUnitDemos.cs-4]
5. [Monitor.Pulse][Neomaster.Demos.Tests_Threads_ThreadsSyncUnitDemos.cs-5]
6. [Tick Tock via Monitor.Pulse - Monitor.Wait][Neomaster.Demos.Tests_Threads_ThreadsSyncUnitDemos.cs-6]
7. [Monitor.Wait with timeout as Sleep][Neomaster.Demos.Tests_Threads_ThreadsSyncUnitDemos.cs-7]
8. [`SpinLock` vs `lock()`][Neomaster.Demos.Tests_Threads_ThreadsSyncUnitDemos.cs-8]
9. [Spin lock as Sleep][Neomaster.Demos.Tests_Threads_ThreadsSyncUnitDemos.cs-9]
10. [Spin lock for fast logging][Neomaster.Demos.Tests_Threads_ThreadsSyncUnitDemos.cs-10]
11. [Spin lock throwing `SynchronizationLockException`][Neomaster.Demos.Tests_Threads_ThreadsSyncUnitDemos.cs-11]
12. [Thread.Yield: fast cycle][Neomaster.Demos.Tests_Threads_ThreadsSyncUnitDemos.cs-12]
13. [Thread.SpinWait: fast cycle][Neomaster.Demos.Tests_Threads_ThreadsSyncUnitDemos.cs-13]
14. [SpinWait.SpinOnce][Neomaster.Demos.Tests_Threads_ThreadsSyncUnitDemos.cs-14]
15. [SpinWait.SpinUntil][Neomaster.Demos.Tests_Threads_ThreadsSyncUnitDemos.cs-15]
16. [Semaphore.WaitOne - Release][Neomaster.Demos.Tests_Threads_ThreadsSyncUnitDemos.cs-16]
17. [Semaphore.Release max slots][Neomaster.Demos.Tests_Threads_ThreadsSyncUnitDemos.cs-17]
18. [Semaphore: named for processes][Neomaster.Demos.Tests_Threads_ThreadsSyncUnitDemos.cs-18]
19. [Semaphore: named created new][Neomaster.Demos.Tests_Threads_ThreadsSyncUnitDemos.cs-19]
20. [Mutex as Monitor][Neomaster.Demos.Tests_Threads_ThreadsSyncUnitDemos.cs-20]
21. [Mutex for singleton thread][Neomaster.Demos.Tests_Threads_ThreadsSyncUnitDemos.cs-21]
22. [Mutex for singleton app][Neomaster.Demos.Tests_Threads_ThreadsSyncUnitDemos.cs-22]
23. [Interrupt][Neomaster.Demos.Tests_Threads_ThreadsSyncUnitDemos.cs-23]
24. [Deadlock: recursive locking][Neomaster.Demos.Tests_Threads_ThreadsSyncUnitDemos.cs-24]
25. [Deadlock: mutual waiting, break by Interrupt][Neomaster.Demos.Tests_Threads_ThreadsSyncUnitDemos.cs-25]
26. [Deadlock: mutual waiting, break by Join with timeout][Neomaster.Demos.Tests_Threads_ThreadsSyncUnitDemos.cs-26]

[Neomaster.Demos.Tests_Threads_ThreadsSyncUnitDemos.cs-1]:Neomaster.Demos.Tests/Threads/ThreadsSyncUnitDemos.cs#L14
[Neomaster.Demos.Tests_Threads_ThreadsSyncUnitDemos.cs-2]:Neomaster.Demos.Tests/Threads/ThreadsSyncUnitDemos.cs#L48
[Neomaster.Demos.Tests_Threads_ThreadsSyncUnitDemos.cs-3]:Neomaster.Demos.Tests/Threads/ThreadsSyncUnitDemos.cs#L88
[Neomaster.Demos.Tests_Threads_ThreadsSyncUnitDemos.cs-4]:Neomaster.Demos.Tests/Threads/ThreadsSyncUnitDemos.cs#L117
[Neomaster.Demos.Tests_Threads_ThreadsSyncUnitDemos.cs-5]:Neomaster.Demos.Tests/Threads/ThreadsSyncUnitDemos.cs#L164
[Neomaster.Demos.Tests_Threads_ThreadsSyncUnitDemos.cs-6]:Neomaster.Demos.Tests/Threads/ThreadsSyncUnitDemos.cs#L213
[Neomaster.Demos.Tests_Threads_ThreadsSyncUnitDemos.cs-7]:Neomaster.Demos.Tests/Threads/ThreadsSyncUnitDemos.cs#L273
[Neomaster.Demos.Tests_Threads_ThreadsSyncUnitDemos.cs-8]:Neomaster.Demos.Tests/Threads/ThreadsSyncUnitDemos.cs#L319
[Neomaster.Demos.Tests_Threads_ThreadsSyncUnitDemos.cs-9]:Neomaster.Demos.Tests/Threads/ThreadsSyncUnitDemos.cs#L367
[Neomaster.Demos.Tests_Threads_ThreadsSyncUnitDemos.cs-10]:Neomaster.Demos.Tests/Threads/ThreadsSyncUnitDemos.cs#L429
[Neomaster.Demos.Tests_Threads_ThreadsSyncUnitDemos.cs-11]:Neomaster.Demos.Tests/Threads/ThreadsSyncUnitDemos.cs#L461
[Neomaster.Demos.Tests_Threads_ThreadsSyncUnitDemos.cs-12]:Neomaster.Demos.Tests/Threads/ThreadsSyncUnitDemos.cs#L493
[Neomaster.Demos.Tests_Threads_ThreadsSyncUnitDemos.cs-13]:Neomaster.Demos.Tests/Threads/ThreadsSyncUnitDemos.cs#L579
[Neomaster.Demos.Tests_Threads_ThreadsSyncUnitDemos.cs-14]:Neomaster.Demos.Tests/Threads/ThreadsSyncUnitDemos.cs#L667
[Neomaster.Demos.Tests_Threads_ThreadsSyncUnitDemos.cs-15]:Neomaster.Demos.Tests/Threads/ThreadsSyncUnitDemos.cs#L683
[Neomaster.Demos.Tests_Threads_ThreadsSyncUnitDemos.cs-16]:Neomaster.Demos.Tests/Threads/ThreadsSyncUnitDemos.cs#L702
[Neomaster.Demos.Tests_Threads_ThreadsSyncUnitDemos.cs-17]:Neomaster.Demos.Tests/Threads/ThreadsSyncUnitDemos.cs#L735
[Neomaster.Demos.Tests_Threads_ThreadsSyncUnitDemos.cs-18]:Neomaster.Demos.Tests/Threads/ThreadsSyncUnitDemos.cs#L770
[Neomaster.Demos.Tests_Threads_ThreadsSyncUnitDemos.cs-19]:Neomaster.Demos.Tests/Threads/ThreadsSyncUnitDemos.cs#L822
[Neomaster.Demos.Tests_Threads_ThreadsSyncUnitDemos.cs-20]:Neomaster.Demos.Tests/Threads/ThreadsSyncUnitDemos.cs#L847
[Neomaster.Demos.Tests_Threads_ThreadsSyncUnitDemos.cs-21]:Neomaster.Demos.Tests/Threads/ThreadsSyncUnitDemos.cs#L877
[Neomaster.Demos.Tests_Threads_ThreadsSyncUnitDemos.cs-22]:Neomaster.Demos.Tests/Threads/ThreadsSyncUnitDemos.cs#L919
[Neomaster.Demos.Tests_Threads_ThreadsSyncUnitDemos.cs-23]:Neomaster.Demos.Tests/Threads/ThreadsSyncUnitDemos.cs#L965
[Neomaster.Demos.Tests_Threads_ThreadsSyncUnitDemos.cs-24]:Neomaster.Demos.Tests/Threads/ThreadsSyncUnitDemos.cs#L1009
[Neomaster.Demos.Tests_Threads_ThreadsSyncUnitDemos.cs-25]:Neomaster.Demos.Tests/Threads/ThreadsSyncUnitDemos.cs#L1041
[Neomaster.Demos.Tests_Threads_ThreadsSyncUnitDemos.cs-26]:Neomaster.Demos.Tests/Threads/ThreadsSyncUnitDemos.cs#L1098


#### 3. Event Synchronization

1. [EventWaitHandle.Set][Neomaster.Demos.Tests_Threads_ThreadsEventSyncUnitDemos.cs-1]
2. [EventWaitHandle.Reset][Neomaster.Demos.Tests_Threads_ThreadsEventSyncUnitDemos.cs-2]
3. [EventWaitHandle.Set with auto-reset][Neomaster.Demos.Tests_Threads_ThreadsEventSyncUnitDemos.cs-3]
4. [AutoResetEvent.Set][Neomaster.Demos.Tests_Threads_ThreadsEventSyncUnitDemos.cs-4]
5. [ManualResetEvent.Set][Neomaster.Demos.Tests_Threads_ThreadsEventSyncUnitDemos.cs-5]
6. [ManualResetEventSlim.Set][Neomaster.Demos.Tests_Threads_ThreadsEventSyncUnitDemos.cs-6]
7. [ManualResetEventSlim.Wait with timeout][Neomaster.Demos.Tests_Threads_ThreadsEventSyncUnitDemos.cs-7]
8. [CountdownEvent.Wait as Join][Neomaster.Demos.Tests_Threads_ThreadsEventSyncUnitDemos.cs-8]
9. [CountdownEvent.AddCount][Neomaster.Demos.Tests_Threads_ThreadsEventSyncUnitDemos.cs-9]
10. [CountdownEvent.TryAddCount][Neomaster.Demos.Tests_Threads_ThreadsEventSyncUnitDemos.cs-10]
11. [CountdownEvent.IsSet][Neomaster.Demos.Tests_Threads_ThreadsEventSyncUnitDemos.cs-11]
12. [CountdownEvent.Reset][Neomaster.Demos.Tests_Threads_ThreadsEventSyncUnitDemos.cs-12]
13. [CountdownEvent.Reset with arg][Neomaster.Demos.Tests_Threads_ThreadsEventSyncUnitDemos.cs-13]
14. [Barrier: Phases][Neomaster.Demos.Tests_Threads_ThreadsEventSyncUnitDemos.cs-14]
15. [CancellationToken: create token][Neomaster.Demos.Tests_Threads_ThreadsEventSyncUnitDemos.cs-15]
16. [CancellationToken: cancellation request][Neomaster.Demos.Tests_Threads_ThreadsEventSyncUnitDemos.cs-16]
17. [CancellationToken: cancellation callback sequence][Neomaster.Demos.Tests_Threads_ThreadsEventSyncUnitDemos.cs-17]
18. [CancellationToken: cancellation callback sequence: before first exception][Neomaster.Demos.Tests_Threads_ThreadsEventSyncUnitDemos.cs-18]
19. [CancellationToken: cancellation callback sequence: all, ignoring exceptions][Neomaster.Demos.Tests_Threads_ThreadsEventSyncUnitDemos.cs-19]
20. [CancellationToken.ThrowIfCancellationRequested][Neomaster.Demos.Tests_Threads_ThreadsEventSyncUnitDemos.cs-20]
21. [CancellationToken.None usage][Neomaster.Demos.Tests_Threads_ThreadsEventSyncUnitDemos.cs-21]
22. [CancellationToken.None variants][Neomaster.Demos.Tests_Threads_ThreadsEventSyncUnitDemos.cs-22]
23. [CancellationTokenSource.CreateLinkedTokenSource][Neomaster.Demos.Tests_Threads_ThreadsEventSyncUnitDemos.cs-23]

[Neomaster.Demos.Tests_Threads_ThreadsEventSyncUnitDemos.cs-1]:Neomaster.Demos.Tests/Threads/ThreadsEventSyncUnitDemos.cs#L11
[Neomaster.Demos.Tests_Threads_ThreadsEventSyncUnitDemos.cs-2]:Neomaster.Demos.Tests/Threads/ThreadsEventSyncUnitDemos.cs#L76
[Neomaster.Demos.Tests_Threads_ThreadsEventSyncUnitDemos.cs-3]:Neomaster.Demos.Tests/Threads/ThreadsEventSyncUnitDemos.cs#L142
[Neomaster.Demos.Tests_Threads_ThreadsEventSyncUnitDemos.cs-4]:Neomaster.Demos.Tests/Threads/ThreadsEventSyncUnitDemos.cs#L198
[Neomaster.Demos.Tests_Threads_ThreadsEventSyncUnitDemos.cs-5]:Neomaster.Demos.Tests/Threads/ThreadsEventSyncUnitDemos.cs#L254
[Neomaster.Demos.Tests_Threads_ThreadsEventSyncUnitDemos.cs-6]:Neomaster.Demos.Tests/Threads/ThreadsEventSyncUnitDemos.cs#L303
[Neomaster.Demos.Tests_Threads_ThreadsEventSyncUnitDemos.cs-7]:Neomaster.Demos.Tests/Threads/ThreadsEventSyncUnitDemos.cs#L352
[Neomaster.Demos.Tests_Threads_ThreadsEventSyncUnitDemos.cs-8]:Neomaster.Demos.Tests/Threads/ThreadsEventSyncUnitDemos.cs#L376
[Neomaster.Demos.Tests_Threads_ThreadsEventSyncUnitDemos.cs-9]:Neomaster.Demos.Tests/Threads/ThreadsEventSyncUnitDemos.cs#L402
[Neomaster.Demos.Tests_Threads_ThreadsEventSyncUnitDemos.cs-10]:Neomaster.Demos.Tests/Threads/ThreadsEventSyncUnitDemos.cs#L436
[Neomaster.Demos.Tests_Threads_ThreadsEventSyncUnitDemos.cs-11]:Neomaster.Demos.Tests/Threads/ThreadsEventSyncUnitDemos.cs#L449
[Neomaster.Demos.Tests_Threads_ThreadsEventSyncUnitDemos.cs-12]:Neomaster.Demos.Tests/Threads/ThreadsEventSyncUnitDemos.cs#L465
[Neomaster.Demos.Tests_Threads_ThreadsEventSyncUnitDemos.cs-13]:Neomaster.Demos.Tests/Threads/ThreadsEventSyncUnitDemos.cs#L480
[Neomaster.Demos.Tests_Threads_ThreadsEventSyncUnitDemos.cs-14]:Neomaster.Demos.Tests/Threads/ThreadsEventSyncUnitDemos.cs#L495
[Neomaster.Demos.Tests_Threads_ThreadsEventSyncUnitDemos.cs-15]:Neomaster.Demos.Tests/Threads/ThreadsEventSyncUnitDemos.cs#L540
[Neomaster.Demos.Tests_Threads_ThreadsEventSyncUnitDemos.cs-16]:Neomaster.Demos.Tests/Threads/ThreadsEventSyncUnitDemos.cs#L551
[Neomaster.Demos.Tests_Threads_ThreadsEventSyncUnitDemos.cs-17]:Neomaster.Demos.Tests/Threads/ThreadsEventSyncUnitDemos.cs#L578
[Neomaster.Demos.Tests_Threads_ThreadsEventSyncUnitDemos.cs-18]:Neomaster.Demos.Tests/Threads/ThreadsEventSyncUnitDemos.cs#L609
[Neomaster.Demos.Tests_Threads_ThreadsEventSyncUnitDemos.cs-19]:Neomaster.Demos.Tests/Threads/ThreadsEventSyncUnitDemos.cs#L656
[Neomaster.Demos.Tests_Threads_ThreadsEventSyncUnitDemos.cs-20]:Neomaster.Demos.Tests/Threads/ThreadsEventSyncUnitDemos.cs#L703
[Neomaster.Demos.Tests_Threads_ThreadsEventSyncUnitDemos.cs-21]:Neomaster.Demos.Tests/Threads/ThreadsEventSyncUnitDemos.cs#L738
[Neomaster.Demos.Tests_Threads_ThreadsEventSyncUnitDemos.cs-22]:Neomaster.Demos.Tests/Threads/ThreadsEventSyncUnitDemos.cs#L760
[Neomaster.Demos.Tests_Threads_ThreadsEventSyncUnitDemos.cs-23]:Neomaster.Demos.Tests/Threads/ThreadsEventSyncUnitDemos.cs#L777


#### 4. Thread Pool

1. [QueueUserWorkItem][Neomaster.Demos.Tests_Threads_ThreadsThreadPoolUnitDemos.cs-1]
2. [QueueUserWorkItem with state][Neomaster.Demos.Tests_Threads_ThreadsThreadPoolUnitDemos.cs-2]
3. [QueueUserWorkItem Join][Neomaster.Demos.Tests_Threads_ThreadsThreadPoolUnitDemos.cs-3]
4. [Set pool thread as foreground][Neomaster.Demos.Tests_Threads_ThreadsThreadPoolUnitDemos.cs-4]
5. [Pool restores thread as background][Neomaster.Demos.Tests_Threads_ThreadsThreadPoolUnitDemos.cs-5]

[Neomaster.Demos.Tests_Threads_ThreadsThreadPoolUnitDemos.cs-1]:Neomaster.Demos.Tests/Threads/ThreadsThreadPoolUnitDemos.cs#L11
[Neomaster.Demos.Tests_Threads_ThreadsThreadPoolUnitDemos.cs-2]:Neomaster.Demos.Tests/Threads/ThreadsThreadPoolUnitDemos.cs#L28
[Neomaster.Demos.Tests_Threads_ThreadsThreadPoolUnitDemos.cs-3]:Neomaster.Demos.Tests/Threads/ThreadsThreadPoolUnitDemos.cs#L47
[Neomaster.Demos.Tests_Threads_ThreadsThreadPoolUnitDemos.cs-4]:Neomaster.Demos.Apps/Neomaster.Demos.Apps.Threads.SetPoolThreadAsForeground/Program.cs
[Neomaster.Demos.Tests_Threads_ThreadsThreadPoolUnitDemos.cs-5]:Neomaster.Demos.Tests/Threads/ThreadsThreadPoolUnitDemos.cs#L68


#### Atomic Operations

1. [Volatile class][Neomaster.Demos.Tests_Threads_ThreadsAtomicOperationsUnitDemos.cs-1]
2. [Volatile keyword][Neomaster.Demos.Tests_Threads_ThreadsAtomicOperationsUnitDemos.cs-2]
3. [Interlocked Increment][Neomaster.Demos.Tests_Threads_ThreadsAtomicOperationsUnitDemos.cs-3]

[Neomaster.Demos.Tests_Threads_ThreadsAtomicOperationsUnitDemos.cs-1]:Neomaster.Demos.Tests/Threads/ThreadsAtomicOperationsUnitDemos.cs#L12
[Neomaster.Demos.Tests_Threads_ThreadsAtomicOperationsUnitDemos.cs-2]:Neomaster.Demos.Tests/Threads/ThreadsAtomicOperationsUnitDemos.cs#L47
[Neomaster.Demos.Tests_Threads_ThreadsAtomicOperationsUnitDemos.cs-3]:Neomaster.Demos.Tests/Threads/ThreadsAtomicOperationsUnitDemos.cs#L81


#### Features

1. [Synchronized method: instance][Neomaster.Demos.Tests_Threads_ThreadsFeaturesUnitDemos.cs-1]
2. [Synchronized method: static][Neomaster.Demos.Tests_Threads_ThreadsFeaturesUnitDemos.cs-2]
3. [Synchronized method: instance in different threads][Neomaster.Demos.Tests_Threads_ThreadsFeaturesUnitDemos.cs-3]
4. [Synchronized method: static in different threads][Neomaster.Demos.Tests_Threads_ThreadsFeaturesUnitDemos.cs-4]
5. [ThreadLocal&lt;T&gt;: counters][Neomaster.Demos.Tests_Threads_ThreadsFeaturesUnitDemos.cs-5]
6. [Lazy&lt;T&gt;: lazy initialization][Neomaster.Demos.Tests_Threads_ThreadsFeaturesUnitDemos.cs-6]
7. [Lazy&lt;T&gt;: single initialization][Neomaster.Demos.Tests_Threads_ThreadsFeaturesUnitDemos.cs-7]
8. [Lazy&lt;T&gt;: multiple initialization][Neomaster.Demos.Tests_Threads_ThreadsFeaturesUnitDemos.cs-8]
9. [Lazy&lt;T&gt;: unsafe initialization][Neomaster.Demos.Tests_Threads_ThreadsFeaturesUnitDemos.cs-9]
10. [Note: disposable sync primitives][Neomaster.Demos.Tests_Threads_ThreadsFeaturesUnitDemos.cs-10]

[Neomaster.Demos.Tests_Threads_ThreadsFeaturesUnitDemos.cs-1]:Neomaster.Demos.Tests/Threads/ThreadsFeaturesUnitDemos.cs#L12
[Neomaster.Demos.Tests_Threads_ThreadsFeaturesUnitDemos.cs-2]:Neomaster.Demos.Tests/Threads/ThreadsFeaturesUnitDemos.cs#L28
[Neomaster.Demos.Tests_Threads_ThreadsFeaturesUnitDemos.cs-3]:Neomaster.Demos.Tests/Threads/ThreadsFeaturesUnitDemos.cs#L43
[Neomaster.Demos.Tests_Threads_ThreadsFeaturesUnitDemos.cs-4]:Neomaster.Demos.Tests/Threads/ThreadsFeaturesUnitDemos.cs#L88
[Neomaster.Demos.Tests_Threads_ThreadsFeaturesUnitDemos.cs-5]:Neomaster.Demos.Tests/Threads/ThreadsFeaturesUnitDemos.cs#L131
[Neomaster.Demos.Tests_Threads_ThreadsFeaturesUnitDemos.cs-6]:Neomaster.Demos.Tests/Threads/ThreadsFeaturesUnitDemos.cs#L153
[Neomaster.Demos.Tests_Threads_ThreadsFeaturesUnitDemos.cs-7]:Neomaster.Demos.Tests/Threads/ThreadsFeaturesUnitDemos.cs#L165
[Neomaster.Demos.Tests_Threads_ThreadsFeaturesUnitDemos.cs-8]:Neomaster.Demos.Tests/Threads/ThreadsFeaturesUnitDemos.cs#L199
[Neomaster.Demos.Tests_Threads_ThreadsFeaturesUnitDemos.cs-9]:Neomaster.Demos.Tests/Threads/ThreadsFeaturesUnitDemos.cs#L242
[Neomaster.Demos.Tests_Threads_ThreadsFeaturesUnitDemos.cs-10]:Neomaster.Demos.Tests/Threads/ThreadsFeaturesUnitDemos.cs#L278


</details>

<details>

<summary><b>📋 Tasks</b></summary>

#### 1. Fundamentals

1. [Create task][Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-1]
2. [Task is in pool thread][Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-2]
3. [Wait task][Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-3]
4. [Wait task with timeout][Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-4]
5. [Task is running after wait timeout][Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-5]
6. [Wait task with cancellation token][Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-6]
7. [Wait blocks thread][Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-7]
8. [Wait wraps task exception into `AggregateException`][Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-8]
9. [Result][Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-9]
10. [Result blocks thread][Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-10]
11. [Result wraps task exception into `AggregateException`][Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-11]
12. [Delay][Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-12]
13. [Delay with cancellation token][Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-13]
14. [Delay is working after wait timeout][Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-14]
15. [`await` releases manual thread][Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-15]
16. [`await` releases pool thread][Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-16]
17. [Method with `async`, without `await` is synchronous][Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-17]
18. [Method with `async`, with `await` is asynchronous][Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-18]
19. [ConfigureAwait][Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-19]
20. [ConfigureAwait: effect on default sync context Post and Send][Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-20]
21. [ConfigureAwait: effect on UI sync context Post and Send][Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-21]
22. [Throwing task exception][Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-22]
23. [ContinueWith: created task status][Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-23]
24. [ContinueWith: task chain][Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-24]
25. [ContinueWith: variable continuation][Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-25]
26. [ContinueWith: continuation options][Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-26]
27. [ContinueWith: SetInterval(][Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-27]
28. [RunSynchronously][Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-28]
29. [RunSynchronously and continuation][Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-29]
30. [RunSynchronously and synchronous continuation][Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-30]
31. [RunSynchronously continuation][Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-31]
32. [Set status to `TaskStatus.Canceled` after cancellation by `Token.ThrowIfCancellationRequested()`][Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-32]
33. [WhenAll][Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-33]
34. [WhenAll: task exceptions][Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-34]
35. [WhenAll with canceled task][Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-35]
36. [WhenAll with incorrect canceled task][Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-36]
37. [WaitAll][Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-37]
38. [WaitAll: task exceptions][Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-38]
39. [WhenAny][Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-39]
40. [WhenAny: timeout][Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-40]
41. [WhenAny: task exception][Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-41]
42. [WaitAny][Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-42]
43. [WaitAny: task exception][Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-43]
44. [WhenEach][Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-44]
45. [Yield][Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-45]
46. [Awaiter: GetResult][Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-46]
47. [Awaiter: OnCompleted][Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-47]
48. [Awaiter: pattern][Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-48]
49. [Awaiter: timespan awaiter][Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-49]
50. [TaskCompletionSource: timeout][Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-50]
51. [TaskCompletionSource: WithTimeout() extension][Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-51]
52. [TaskCompletionSource: external event source adapter][Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-52]
53. [FromResult][Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-53]
54. [FromCanceled][Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-54]
55. [FromException][Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-55]
56. [CompletedTask][Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-56]
57. [ValueTask: cached result][Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-57]
58. [Factory][Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-58]
59. [Factory: continuations][Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-59]
60. [Factory: `TaskCreationOptions.LongRunning`][Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-60]
61. [Factory: child task attachment, `TaskCreationOptions.DenyChildAttach`][Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-61]
62. [Factory: set task schedulers][Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-62]

[Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-1]:Neomaster.Demos.Tests/Tasks/TasksUnitDemos.cs#L11
[Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-2]:Neomaster.Demos.Tests/Tasks/TasksUnitDemos.cs#L38
[Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-3]:Neomaster.Demos.Tests/Tasks/TasksUnitDemos.cs#L53
[Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-4]:Neomaster.Demos.Tests/Tasks/TasksUnitDemos.cs#L69
[Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-5]:Neomaster.Demos.Tests/Tasks/TasksUnitDemos.cs#L82
[Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-6]:Neomaster.Demos.Tests/Tasks/TasksUnitDemos.cs#L104
[Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-7]:Neomaster.Demos.Tests/Tasks/TasksUnitDemos.cs#L122
[Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-8]:Neomaster.Demos.Tests/Tasks/TasksUnitDemos.cs#L146
[Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-9]:Neomaster.Demos.Tests/Tasks/TasksUnitDemos.cs#L163
[Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-10]:Neomaster.Demos.Tests/Tasks/TasksUnitDemos.cs#L171
[Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-11]:Neomaster.Demos.Tests/Tasks/TasksUnitDemos.cs#L194
[Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-12]:Neomaster.Demos.Tests/Tasks/TasksUnitDemos.cs#L212
[Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-13]:Neomaster.Demos.Tests/Tasks/TasksUnitDemos.cs#L224
[Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-14]:Neomaster.Demos.Tests/Tasks/TasksUnitDemos.cs#L237
[Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-15]:Neomaster.Demos.Tests/Tasks/TasksUnitDemos.cs#L247
[Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-16]:Neomaster.Demos.Tests/Tasks/TasksUnitDemos.cs#L284
[Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-17]:Neomaster.Demos.Tests/Tasks/TasksUnitDemos.cs#L329
[Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-18]:Neomaster.Demos.Tests/Tasks/TasksUnitDemos.cs#L356
[Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-19]:Neomaster.Demos.Apps/Neomaster.Demos.Apps.Tasks.TaskConfigureAwait/Form1.cs
[Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-20]:Neomaster.Demos.Tests/Tasks/TasksUnitDemos.cs#L387
[Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-21]:Neomaster.Demos.Tests/Tasks/TasksUnitDemos.cs#L413
[Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-22]:Neomaster.Demos.Tests/Tasks/TasksUnitDemos.cs#L437
[Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-23]:Neomaster.Demos.Tests/Tasks/TasksUnitDemos.cs#L508
[Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-24]:Neomaster.Demos.Tests/Tasks/TasksUnitDemos.cs#L515
[Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-25]:Neomaster.Demos.Tests/Tasks/TasksUnitDemos.cs#L525
[Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-26]:Neomaster.Demos.Tests/Tasks/TasksUnitDemos.cs#L558
[Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-27]:Neomaster.Demos.Tests/Tasks/TasksUnitDemos.cs#L574
[Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-28]:Neomaster.Demos.Tests/Tasks/TasksUnitDemos.cs#L621
[Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-29]:Neomaster.Demos.Tests/Tasks/TasksUnitDemos.cs#L643
[Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-30]:Neomaster.Demos.Tests/Tasks/TasksUnitDemos.cs#L679
[Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-31]:Neomaster.Demos.Tests/Tasks/TasksUnitDemos.cs#L717
[Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-32]:Neomaster.Demos.Tests/Tasks/TasksUnitDemos.cs#L736
[Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-33]:Neomaster.Demos.Tests/Tasks/TasksUnitDemos.cs#L764
[Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-34]:Neomaster.Demos.Tests/Tasks/TasksUnitDemos.cs#L785
[Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-35]:Neomaster.Demos.Tests/Tasks/TasksUnitDemos.cs#L845
[Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-36]:Neomaster.Demos.Tests/Tasks/TasksUnitDemos.cs#L869
[Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-37]:Neomaster.Demos.Tests/Tasks/TasksUnitDemos.cs#L904
[Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-38]:Neomaster.Demos.Tests/Tasks/TasksUnitDemos.cs#L926
[Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-39]:Neomaster.Demos.Tests/Tasks/TasksUnitDemos.cs#L973
[Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-40]:Neomaster.Demos.Tests/Tasks/TasksUnitDemos.cs#L994
[Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-41]:Neomaster.Demos.Tests/Tasks/TasksUnitDemos.cs#L1019
[Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-42]:Neomaster.Demos.Tests/Tasks/TasksUnitDemos.cs#L1038
[Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-43]:Neomaster.Demos.Tests/Tasks/TasksUnitDemos.cs#L1057
[Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-44]:Neomaster.Demos.Tests/Tasks/TasksUnitDemos.cs#L1076
[Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-45]:Neomaster.Demos.Tests/Tasks/TasksUnitDemos.cs#L1115
[Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-46]:Neomaster.Demos.Tests/Tasks/TasksUnitDemos.cs#L1173
[Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-47]:Neomaster.Demos.Tests/Tasks/TasksUnitDemos.cs#L1185
[Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-48]:Neomaster.Demos.Tests/Tasks/TasksUnitDemos.cs#L1212
[Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-49]:Neomaster.Demos.Tests/Tasks/TasksUnitDemos.cs#L1264
[Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-50]:Neomaster.Demos.Tests/Tasks/TasksUnitDemos.cs#L1275
[Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-51]:Neomaster.Demos.Tests/Tasks/TasksUnitDemos.cs#L1300
[Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-52]:Neomaster.Demos.Tests/Tasks/TasksUnitDemos.cs#L1309
[Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-53]:Neomaster.Demos.Tests/Tasks/TasksUnitDemos.cs#L1325
[Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-54]:Neomaster.Demos.Tests/Tasks/TasksUnitDemos.cs#L1334
[Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-55]:Neomaster.Demos.Tests/Tasks/TasksUnitDemos.cs#L1356
[Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-56]:Neomaster.Demos.Tests/Tasks/TasksUnitDemos.cs#L1378
[Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-57]:Neomaster.Demos.Tests/Tasks/TasksUnitDemos.cs#L1385
[Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-58]:Neomaster.Demos.Tests/Tasks/TasksUnitDemos.cs#L1421
[Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-59]:Neomaster.Demos.Tests/Tasks/TasksUnitDemos.cs#L1437
[Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-60]:Neomaster.Demos.Tests/Tasks/TasksUnitDemos.cs#L1465
[Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-61]:Neomaster.Demos.Tests/Tasks/TasksUnitDemos.cs#L1478
[Neomaster.Demos.Tests_Tasks_TasksUnitDemos.cs-62]:Neomaster.Demos.Tests/Tasks/TasksUnitDemos.cs#L1497


#### 2. Synchronization

1. [`lock` within task][Neomaster.Demos.Tests_Tasks_TasksSyncUnitDemos.cs-1]
2. [Task within `lock`][Neomaster.Demos.Tests_Tasks_TasksSyncUnitDemos.cs-2]
3. [Lock via semaphore][Neomaster.Demos.Tests_Tasks_TasksSyncUnitDemos.cs-3]

[Neomaster.Demos.Tests_Tasks_TasksSyncUnitDemos.cs-1]:Neomaster.Demos.Tests/Tasks/TasksSyncUnitDemos.cs#L10
[Neomaster.Demos.Tests_Tasks_TasksSyncUnitDemos.cs-2]:Neomaster.Demos.Tests/Tasks/TasksSyncUnitDemos.cs#L55
[Neomaster.Demos.Tests_Tasks_TasksSyncUnitDemos.cs-3]:Neomaster.Demos.Tests/Tasks/TasksSyncUnitDemos.cs#L65


#### Features

1. [Timer: Callback][Neomaster.Demos.Tests_Tasks_TasksFeaturesUnitDemos.cs-1]
2. [Timer: AutoReset: false][Neomaster.Demos.Tests_Tasks_TasksFeaturesUnitDemos.cs-2]
3. [Timer: Alarms][Neomaster.Demos.Tests_Tasks_TasksFeaturesUnitDemos.cs-3]
4. [Parallel: `For()`][Neomaster.Demos.Tests_Tasks_TasksFeaturesUnitDemos.cs-4]
5. [Parallel: `Stop()`][Neomaster.Demos.Tests_Tasks_TasksFeaturesUnitDemos.cs-5]
6. [Parallel: `Break()`][Neomaster.Demos.Tests_Tasks_TasksFeaturesUnitDemos.cs-6]
7. [Parallel: Local Var][Neomaster.Demos.Tests_Tasks_TasksFeaturesUnitDemos.cs-7]
8. [Parallel: Exception][Neomaster.Demos.Tests_Tasks_TasksFeaturesUnitDemos.cs-8]
9. [Parallel: `ParallelOptions`][Neomaster.Demos.Tests_Tasks_TasksFeaturesUnitDemos.cs-9]
10. [Parallel: State Checks][Neomaster.Demos.Tests_Tasks_TasksFeaturesUnitDemos.cs-10]
11. [Parallel: `Foreach()`][Neomaster.Demos.Tests_Tasks_TasksFeaturesUnitDemos.cs-11]
12. [Parallel: `Invoke()`][Neomaster.Demos.Tests_Tasks_TasksFeaturesUnitDemos.cs-12]

[Neomaster.Demos.Tests_Tasks_TasksFeaturesUnitDemos.cs-1]:Neomaster.Demos.Tests/Tasks/TasksFeaturesUnitDemos.cs#L12
[Neomaster.Demos.Tests_Tasks_TasksFeaturesUnitDemos.cs-2]:Neomaster.Demos.Tests/Tasks/TasksFeaturesUnitDemos.cs#L39
[Neomaster.Demos.Tests_Tasks_TasksFeaturesUnitDemos.cs-3]:Neomaster.Demos.Tests/Tasks/TasksFeaturesUnitDemos.cs#L63
[Neomaster.Demos.Tests_Tasks_TasksFeaturesUnitDemos.cs-4]:Neomaster.Demos.Tests/Tasks/TasksFeaturesUnitDemos.cs#L112
[Neomaster.Demos.Tests_Tasks_TasksFeaturesUnitDemos.cs-5]:Neomaster.Demos.Tests/Tasks/TasksFeaturesUnitDemos.cs#L136
[Neomaster.Demos.Tests_Tasks_TasksFeaturesUnitDemos.cs-6]:Neomaster.Demos.Tests/Tasks/TasksFeaturesUnitDemos.cs#L172
[Neomaster.Demos.Tests_Tasks_TasksFeaturesUnitDemos.cs-7]:Neomaster.Demos.Tests/Tasks/TasksFeaturesUnitDemos.cs#L209
[Neomaster.Demos.Tests_Tasks_TasksFeaturesUnitDemos.cs-8]:Neomaster.Demos.Tests/Tasks/TasksFeaturesUnitDemos.cs#L238
[Neomaster.Demos.Tests_Tasks_TasksFeaturesUnitDemos.cs-9]:Neomaster.Demos.Tests/Tasks/TasksFeaturesUnitDemos.cs#L282
[Neomaster.Demos.Tests_Tasks_TasksFeaturesUnitDemos.cs-10]:Neomaster.Demos.Tests/Tasks/TasksFeaturesUnitDemos.cs#L300
[Neomaster.Demos.Tests_Tasks_TasksFeaturesUnitDemos.cs-11]:Neomaster.Demos.Tests/Tasks/TasksFeaturesUnitDemos.cs#L327
[Neomaster.Demos.Tests_Tasks_TasksFeaturesUnitDemos.cs-12]:Neomaster.Demos.Tests/Tasks/TasksFeaturesUnitDemos.cs#L362


</details>

<details>

<summary><b>🔗 LINQ</b></summary>

#### Expressions

1. [Tree structure: view][Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-1]
2. [Tree structure: create: left operand via `Expression.MakeMemberAccess`][Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-2]
3. [Tree structure: create: left operand via `Expression.Property`][Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-3]
4. [Tree structure: create: 3 levels][Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-4]
5. [Lambda: create `Func1`][Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-5]
6. [Lambda: create `Func2`][Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-6]
7. [Lambda: parameter order][Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-7]
8. [Lambda: info][Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-8]
9. [Lambda: view with named parameters][Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-9]
10. [Lambda: typed vs untyped][Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-10]
11. [Lambda: `DynamicInvoke()`: dynamic `Func`][Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-11]
12. [Lambda: `DynamicInvoke()`: dynamic Add][Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-12]
13. [`ExpressionType`: list of all][Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-13]
14. [Debug view][Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-14]
15. [Debug view: lambda][Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-15]
16. [`Reduce()`][Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-16]
17. [`ReduceAndCheck()`: reducible][Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-17]
18. [`ReduceAndCheck()`: not reducible][Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-18]
19. [`ReduceAndCheck()`: prevent return `null` and `this`][Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-19]
20. [`ReduceExtensions()`: builtin root][Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-20]
21. [`ReduceExtensions()`: custom root: reducible][Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-21]
22. [`ReduceExtensions()`: custom root: not reducible][Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-22]
23. [`IsByRef`: struct parameter: with `ref` modifier][Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-23]
24. [`IsByRef`: reference parameter: without `ref` modifier][Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-24]
25. [`IsByRef`: reference parameter: with `ref` modifier][Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-25]
26. [Visitor: root][Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-26]
27. [Visitor: tree: immutable][Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-27]
28. [Visitor: tree: with mutable child node][Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-28]
29. [`Expression.Invoke`][Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-29]
30. [`Expression.Call`: instance method][Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-30]
31. [`Expression.Call`: static method][Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-31]
32. [`Expression.New`][Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-32]
33. [`Expression.MemberInit`, `Expression.Bind`][Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-33]
34. [`Expression.MemberBind`][Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-34]
35. [`Expression.Quote`: lambda returns lambda][Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-35]
36. [`Expression.Quote`: lambda returns lambda: in DB provider][Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-36]
37. [`Expression.Assign`][Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-37]
38. [`Expression.Block`, `Expression.Variable`: `Swap(x, y)`][Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-38]
39. [`Expression.Block` returns last expression result][Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-39]
40. [Conditional operators][Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-40]
41. [`Expression.Throw`][Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-41]
42. [`Expression.Goto`, `Expression.Label`: empty][Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-42]
43. [`Expression.Goto`, `Expression.Label`: instruction][Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-43]
44. [`Expression.Goto`, `Expression.Label`: return value][Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-44]
45. [`Expression.Return` like `Expression.Goto`][Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-45]
46. [`Expression.Return` vs `Expression.Goto`: semantic difference][Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-46]
47. [`Expression.Return` vs `Expression.Goto`: call via kind][Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-47]
48. [`Expression.Loop`, `Expression.Break`: power to 10][Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-48]
49. [`Expression.Loop`, `Expression.Break`: select even][Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-49]
50. [`GotoExpressionKind`: list of all][Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-50]
51. [`Expression.TryCatchFinally`][Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-51]
52. [Reverse Polish Notation][Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-52]
53. [Auto-mapper][Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-53]
54. [SQL generation][Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-54]

[Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-1]:Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L18
[Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-2]:Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L47
[Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-3]:Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L84
[Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-4]:Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L120
[Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-5]:Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L157
[Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-6]:Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L172
[Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-7]:Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L197
[Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-8]:Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L212
[Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-9]:Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L249
[Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-10]:Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L279
[Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-11]:Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L295
[Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-12]:Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L314
[Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-13]:Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L333
[Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-14]:Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L343
[Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-15]:Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L382
[Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-16]:Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L411
[Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-17]:Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L449
[Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-18]:Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L477
[Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-19]:Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L499
[Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-20]:Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L506
[Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-21]:Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L520
[Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-22]:Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L534
[Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-23]:Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L547
[Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-24]:Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L562
[Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-25]:Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L577
[Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-26]:Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L592
[Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-27]:Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L632
[Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-28]:Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L652
[Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-29]:Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L673
[Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-30]:Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L698
[Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-31]:Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L710
[Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-32]:Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L726
[Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-33]:Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L755
[Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-34]:Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L776
[Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-35]:Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L807
[Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-36]:Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L842
[Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-37]:Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L880
[Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-38]:Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L900
[Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-39]:Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L923
[Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-40]:Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L937
[Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-41]:Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L991
[Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-42]:Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L1025
[Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-43]:Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L1048
[Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-44]:Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L1070
[Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-45]:Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L1087
[Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-46]:Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L1103
[Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-47]:Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L1118
[Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-48]:Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L1145
[Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-49]:Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L1174
[Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-50]:Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L1213
[Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-51]:Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L1234
[Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-52]:Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L1258
[Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-53]:Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L1288
[Neomaster.Demos.Tests_LinqExpr_LinqExprUnitDemos.cs-54]:Neomaster.Demos.Tests/LinqExpr/LinqExprUnitDemos.cs#L1329


#### Methods

1. [`Enumerable` and `Queryable` method names][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-1]
2. [`Enumerable.Range()`][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-2]
3. [`Enumerable.Repeat()`][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-3]
4. [`Enumerable.Empty()`][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-4]
5. [`Aggregate()`][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-5]
6. [`AggregateBy()`][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-6]
7. [`All()`][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-7]
8. [`Any()`][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-8]
9. [All with `Any()`][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-9]
10. [Any with `All()`][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-10]
11. [`Append()`][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-11]
12. [`Average()`][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-12]
13. [`Cast()`: numbers][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-13]
14. [`Cast()`: classes][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-14]
15. [`Chunk()`][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-15]
16. [`Concat()`][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-16]
17. [`Contains()`][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-17]
18. [`Count()`][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-18]
19. [`CountBy()`][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-19]
20. [`DefaultIfEmpty()`][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-20]
21. [`Distinct()`][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-21]
22. [`DistinctBy()`][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-22]
23. [`ElementAt()`][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-23]
24. [`ElementAtOrDefault()`][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-24]
25. [`Except()`][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-25]
26. [`ExceptBy()`][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-26]
27. [`First()`][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-27]
28. [`FirstOrDefault()`][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-28]
29. [`GroupBy()`][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-29]
30. [`GroupBy()`: element selector][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-30]
31. [`GroupBy()`: result selector][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-31]
32. [`GroupJoin()`][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-32]
33. [`Index()`][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-33]
34. [`Intersect()`][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-34]
35. [`IntersectBy()`][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-35]
36. [`Join()`][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-36]
37. [`Last()`][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-37]
38. [`LastOrDefault()`][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-38]
39. [`Max()`][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-39]
40. [`Max(): exceptions`][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-40]
41. [`MaxBy()`][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-41]
42. [`MaxBy(): exceptions`][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-42]
43. [`OfType()`][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-43]
44. [`Order()`][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-44]
45. [`OrderBy()`][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-45]
46. [`OrderDescending()`][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-46]
47. [`OrderByDescending()`][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-47]
48. [`Prepend()`][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-48]
49. [`Reverse()`][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-49]
50. [`Select()`: indexing][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-50]
51. [`SelectMany()`][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-51]
52. [`SequenceEqual()`][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-52]
53. [`Single()`][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-53]
54. [`SingleOrDefault()`][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-54]
55. [`Skip()`][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-55]
56. [`SkipLast()`][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-56]
57. [`SkipWhile()`][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-57]
58. [`Sum()`][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-58]
59. [`Take()`][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-59]
60. [`TakeLast()`][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-60]
61. [`TakeWhile()`][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-61]
62. [`ThenBy()`][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-62]
63. [`ThenByDescending()`][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-63]
64. [`ToDictionary()`][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-64]
65. [`ToLookup()`][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-65]
66. [`TryGetNonEnumeratedCount()`][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-66]
67. [`Union()`][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-67]
68. [`UnionBy()`][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-68]
69. [`Zip()`][Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-69]

[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-1]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L12
[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-2]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L137
[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-3]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L145
[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-4]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L156
[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-5]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L162
[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-6]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L169
[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-7]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L188
[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-8]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L197
[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-9]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L209
[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-10]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L223
[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-11]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L232
[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-12]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L243
[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-13]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L262
[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-14]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L278
[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-15]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L312
[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-16]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L328
[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-17]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L342
[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-18]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L360
[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-19]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L368
[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-20]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L390
[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-21]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L402
[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-22]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L463
[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-23]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L485
[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-24]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L500
[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-25]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L516
[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-26]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L531
[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-27]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L557
[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-28]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L572
[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-29]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L587
[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-30]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L623
[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-31]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L664
[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-32]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L702
[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-33]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L757
[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-34]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L775
[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-35]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L804
[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-36]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L823
[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-37]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L858
[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-38]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L873
[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-39]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L888
[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-40]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L911
[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-41]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L925
[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-42]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L947
[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-43]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L961
[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-44]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L983
[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-45]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L1006
[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-46]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L1035
[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-47]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L1058
[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-48]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L1087
[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-49]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L1098
[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-50]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L1106
[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-51]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L1114
[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-52]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L1136
[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-53]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L1159
[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-54]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L1166
[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-55]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L1174
[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-56]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L1182
[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-57]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L1190
[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-58]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L1198
[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-59]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L1205
[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-60]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L1213
[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-61]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L1221
[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-62]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L1229
[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-63]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L1250
[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-64]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L1271
[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-65]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L1295
[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-66]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L1321
[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-67]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L1338
[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-68]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L1372
[Neomaster.Demos.Tests_LinqExpr_LinqMethodsUnitDemos.cs-69]:Neomaster.Demos.Tests/LinqExpr/LinqMethodsUnitDemos.cs#L1406


</details>

## C++

<details>
  
<summary><b>🧱 Fundamentals</b></summary>

<br>

1. [Hello World! 123][Neomaster.Demos.Cpp_Fundamentals.cpp-1]

[Neomaster.Demos.Cpp_Fundamentals.cpp-1]:Neomaster.Demos.Cpp/Fundamentals.cpp#L7


</details>

