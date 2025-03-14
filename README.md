# DEMOS

* [*Threads*](#threads)
* [*Threads: Synchronization*](#threads-sync)
* [*Threads: Event Synchronization*](#threads-event-sync)
* [*Threads: Atomic Operations*](#threads-atomic-operations)
* [*Threads: Features*](#threads-features)

### Threads <a name="threads"></a>
1. [Create, Sleep, Join][threads-1]
2. [Create with arg][threads-2]
3. [Foreground][threads-3]
4. [Background][threads-4]
5. [Info, current thread instance][threads-5]
6. [Is alive][threads-6]
7. [Join with timeout][threads-7]
8. [Abort][threads-8]
9. [Affinity (run parameter)][threads-9]
10. [Affinity (programmatically)][threads-10]
11. [Suspend, Resume][threads-11]
12. [Suspend, Resume: Tick Tock][threads-12]
13. [Abort in Core via `CancellationTokenSource`][threads-13]
14. [Suspend-Resume in Core via `ManualResetEventSlim`][threads-14]
15. [Tick Tock in Core][threads-15]

[threads-1]:Neomaster.Demos.Tests/Threads/ThreadsUnitDemos.cs#L17
[threads-2]:Neomaster.Demos.Tests/Threads/ThreadsUnitDemos.cs#L35
[threads-3]:Neomaster.Demos.Apps/Neomaster.Demos.Apps.Threads.Foreground/Program.cs
[threads-4]:Neomaster.Demos.Apps/Neomaster.Demos.Apps.Threads.Background/Program.cs
[threads-5]:Neomaster.Demos.Tests/Threads/ThreadsUnitDemos.cs#L54
[threads-6]:Neomaster.Demos.Tests/Threads/ThreadsUnitDemos.cs#L80
[threads-7]:Neomaster.Demos.Tests/Threads/ThreadsUnitDemos.cs#L103
[threads-8]:Neomaster.Demos.Apps/Neomaster.Demos.Apps.Threads.AbortThread/Program.cs
[threads-9]:Neomaster.Demos.Apps/Neomaster.Demos.Apps.Threads.AffinityParameterized/Program.cs
[threads-10]:Neomaster.Demos.Apps/Neomaster.Demos.Apps.Threads.AffinityProgrammed/Program.cs
[threads-11]:Neomaster.Demos.Apps/Neomaster.Demos.Apps.Threads.SuspendResume/Program.cs
[threads-12]:Neomaster.Demos.Apps/Neomaster.Demos.Apps.Threads.SuspendResumeTickTock/Program.cs
[threads-13]:Neomaster.Demos.Tests/Threads/ThreadsUnitDemos.cs#L120
[threads-14]:Neomaster.Demos.Tests/Threads/ThreadsUnitDemos.cs#L147
[threads-15]:Neomaster.Demos.Tests/Threads/ThreadsUnitDemos.cs#L168

### Threads: Synchronization <a name="threads-sync"></a>
1. [`lock()`][threads-sync-1]
2. [Monitor.Enter - Monitor.Exit][threads-sync-2]
3. [Wait Queue][threads-sync-3]
4. [Monitor.PulseAll][threads-sync-4]
5. [Monitor.Pulse][threads-sync-5]
6. [Tick Tock via Monitor.Pulse - Monitor.Wait][threads-sync-6]
7. [Monitor.Wait with timeout as Sleep][threads-sync-7]
8. [`SpinLock` vs `lock()`][threads-sync-8]
9. [Spin lock as Sleep][threads-sync-9]
10. [Spin lock for fast logging][threads-sync-10]
11. [Spin lock throwing `SynchronizationLockException`][threads-sync-11]
12. [Thread.Yield: fast cycle][threads-sync-12]
13. [Thread.SpinWait: fast cycle][threads-sync-13]
14. [SpinWait.SpinOnce][threads-sync-14]
15. [SpinWait.SpinUntil][threads-sync-15]

[threads-sync-1]:Neomaster.Demos.Tests/Threads/ThreadsSyncUnitDemos.cs#L11
[threads-sync-2]:Neomaster.Demos.Tests/Threads/ThreadsSyncUnitDemos.cs#L45
[threads-sync-3]:Neomaster.Demos.Tests/Threads/ThreadsSyncUnitDemos.cs#L85
[threads-sync-4]:Neomaster.Demos.Tests/Threads/ThreadsSyncUnitDemos.cs#L114
[threads-sync-5]:Neomaster.Demos.Tests/Threads/ThreadsSyncUnitDemos.cs#L161
[threads-sync-6]:Neomaster.Demos.Tests/Threads/ThreadsSyncUnitDemos.cs#L210
[threads-sync-7]:Neomaster.Demos.Tests/Threads/ThreadsSyncUnitDemos.cs#L270
[threads-sync-8]:Neomaster.Demos.Tests/Threads/ThreadsSyncUnitDemos.cs#L316
[threads-sync-9]:Neomaster.Demos.Tests/Threads/ThreadsSyncUnitDemos.cs#L364
[threads-sync-10]:Neomaster.Demos.Tests/Threads/ThreadsSyncUnitDemos.cs#L426
[threads-sync-11]:Neomaster.Demos.Tests/Threads/ThreadsSyncUnitDemos.cs#L458
[threads-sync-12]:Neomaster.Demos.Tests/Threads/ThreadsSyncUnitDemos.cs#L490
[threads-sync-13]:Neomaster.Demos.Tests/Threads/ThreadsSyncUnitDemos.cs#L576
[threads-sync-14]:Neomaster.Demos.Tests/Threads/ThreadsSyncUnitDemos.cs#L664
[threads-sync-15]:Neomaster.Demos.Tests/Threads/ThreadsSyncUnitDemos.cs#L680

### Threads: Event Synchronization <a name="threads-event-sync"></a>
1. [EventWaitHandle.Set][threads-event-sync-1]
2. [EventWaitHandle.Reset][threads-event-sync-2]
3. [EventWaitHandle.Set with auto-reset][threads-event-sync-3]
4. [AutoResetEvent.Set][threads-event-sync-4]
5. [ManualResetEvent.Set][threads-event-sync-5]
6. [ManualResetEventSlim.Set][threads-event-sync-6]
7. [ManualResetEventSlim.Wait with timeout][threads-event-sync-7]

[threads-event-sync-1]:Neomaster.Demos.Tests/Threads/ThreadsEventSyncUnitDemos.cs#L9
[threads-event-sync-2]:Neomaster.Demos.Tests/Threads/ThreadsEventSyncUnitDemos.cs#L74
[threads-event-sync-3]:Neomaster.Demos.Tests/Threads/ThreadsEventSyncUnitDemos.cs#L140
[threads-event-sync-4]:Neomaster.Demos.Tests/Threads/ThreadsEventSyncUnitDemos.cs#L196
[threads-event-sync-5]:Neomaster.Demos.Tests/Threads/ThreadsEventSyncUnitDemos.cs#L252
[threads-event-sync-6]:Neomaster.Demos.Tests/Threads/ThreadsEventSyncUnitDemos.cs#L301
[threads-event-sync-7]:Neomaster.Demos.Tests/Threads/ThreadsEventSyncUnitDemos.cs#L350

### Threads: Atomic Operations <a name="threads-atomic-operations"></a>
1. [Volatile class][threads-atomic-operations-1]
2. [Lazy&lt;T&gt;: lazy initialization][threads-atomic-operations-2]
3. [Lazy&lt;T&gt;: single initialization][threads-atomic-operations-3]
4. [Lazy&lt;T&gt;: multiple initialization][threads-atomic-operations-4]
5. [Lazy&lt;T&gt;: unsafe initialization][threads-atomic-operations-5]

[threads-atomic-operations-1]:Neomaster.Demos.Tests/Threads/ThreadsAtomicOperationsUnitDemos.cs#L8
[threads-atomic-operations-2]:Neomaster.Demos.Tests/Threads/ThreadsAtomicOperationsUnitDemos.cs#L43
[threads-atomic-operations-3]:Neomaster.Demos.Tests/Threads/ThreadsAtomicOperationsUnitDemos.cs#L55
[threads-atomic-operations-4]:Neomaster.Demos.Tests/Threads/ThreadsAtomicOperationsUnitDemos.cs#L89
[threads-atomic-operations-5]:Neomaster.Demos.Tests/Threads/ThreadsAtomicOperationsUnitDemos.cs#L132

### Threads: Features <a name="threads-features"></a>
1. [Synchronized method][threads-features-1]
2. [Synchronized method of different objects][threads-features-2]

[threads-features-1]:Neomaster.Demos.Tests/Threads/ThreadsFeaturesUnitDemos.cs#L10
[threads-features-2]:Neomaster.Demos.Tests/Threads/ThreadsFeaturesUnitDemos.cs#L26
