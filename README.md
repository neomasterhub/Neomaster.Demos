# DEMOS

* [*Threads*](#threads)
* [*Threads: Synchronization*](#threads-sync)
* [*Threads: Event Synchronization*](#threads-event-sync)
* [*Threads: Atomic Operations*](#threads-atomic-operations)
* [*Threads: Features*](#threads-features)
* [*Tasks*](#tasks)

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
16. [Semaphore.WaitOne - Release][threads-sync-16]
17. [Semaphore.Release max slots][threads-sync-17]
18. [Semaphore: named for processes][threads-sync-18]
19. [Semaphore: named created new][threads-sync-19]
20. [Mutex as Monitor][threads-sync-20]
21. [Mutex for singleton thread][threads-sync-21]
22. [Mutex for singleton app][threads-sync-22]
23. [Interrupt][threads-sync-23]
24. [Deadlock: recursive locking][threads-sync-24]
25. [Deadlock: mutual waiting, break by Interrupt][threads-sync-25]
26. [Deadlock: mutual waiting, break by Join with timeout][threads-sync-26]

[threads-sync-1]:Neomaster.Demos.Tests/Threads/ThreadsSyncUnitDemos.cs#L12
[threads-sync-2]:Neomaster.Demos.Tests/Threads/ThreadsSyncUnitDemos.cs#L46
[threads-sync-3]:Neomaster.Demos.Tests/Threads/ThreadsSyncUnitDemos.cs#L86
[threads-sync-4]:Neomaster.Demos.Tests/Threads/ThreadsSyncUnitDemos.cs#L115
[threads-sync-5]:Neomaster.Demos.Tests/Threads/ThreadsSyncUnitDemos.cs#L162
[threads-sync-6]:Neomaster.Demos.Tests/Threads/ThreadsSyncUnitDemos.cs#L211
[threads-sync-7]:Neomaster.Demos.Tests/Threads/ThreadsSyncUnitDemos.cs#L271
[threads-sync-8]:Neomaster.Demos.Tests/Threads/ThreadsSyncUnitDemos.cs#L317
[threads-sync-9]:Neomaster.Demos.Tests/Threads/ThreadsSyncUnitDemos.cs#L365
[threads-sync-10]:Neomaster.Demos.Tests/Threads/ThreadsSyncUnitDemos.cs#L427
[threads-sync-11]:Neomaster.Demos.Tests/Threads/ThreadsSyncUnitDemos.cs#L459
[threads-sync-12]:Neomaster.Demos.Tests/Threads/ThreadsSyncUnitDemos.cs#L491
[threads-sync-13]:Neomaster.Demos.Tests/Threads/ThreadsSyncUnitDemos.cs#L577
[threads-sync-14]:Neomaster.Demos.Tests/Threads/ThreadsSyncUnitDemos.cs#L665
[threads-sync-15]:Neomaster.Demos.Tests/Threads/ThreadsSyncUnitDemos.cs#L681
[threads-sync-16]:Neomaster.Demos.Tests/Threads/ThreadsSyncUnitDemos.cs#L700
[threads-sync-17]:Neomaster.Demos.Tests/Threads/ThreadsSyncUnitDemos.cs#L733
[threads-sync-18]:Neomaster.Demos.Tests/Threads/ThreadsSyncUnitDemos.cs#L768
[threads-sync-19]:Neomaster.Demos.Tests/Threads/ThreadsSyncUnitDemos.cs#L820
[threads-sync-20]:Neomaster.Demos.Tests/Threads/ThreadsSyncUnitDemos.cs#L845
[threads-sync-21]:Neomaster.Demos.Tests/Threads/ThreadsSyncUnitDemos.cs#L875
[threads-sync-22]:Neomaster.Demos.Tests/Threads/ThreadsSyncUnitDemos.cs#L917
[threads-sync-23]:Neomaster.Demos.Tests/Threads/ThreadsSyncUnitDemos.cs#L963
[threads-sync-24]:Neomaster.Demos.Tests/Threads/ThreadsSyncUnitDemos.cs#L1007
[threads-sync-25]:Neomaster.Demos.Tests/Threads/ThreadsSyncUnitDemos.cs#L1039
[threads-sync-26]:Neomaster.Demos.Tests/Threads/ThreadsSyncUnitDemos.cs#L1096

### Threads: Event Synchronization <a name="threads-event-sync"></a>
1. [EventWaitHandle.Set][threads-event-sync-1]
2. [EventWaitHandle.Reset][threads-event-sync-2]
3. [EventWaitHandle.Set with auto-reset][threads-event-sync-3]
4. [AutoResetEvent.Set][threads-event-sync-4]
5. [ManualResetEvent.Set][threads-event-sync-5]
6. [ManualResetEventSlim.Set][threads-event-sync-6]
7. [ManualResetEventSlim.Wait with timeout][threads-event-sync-7]
8. [CountdownEvent.Wait as Join][threads-event-sync-8]
9. [CountdownEvent.AddCount][threads-event-sync-9]
10. [CountdownEvent.TryAddCount][threads-event-sync-10]
11. [CountdownEvent.IsSet][threads-event-sync-11]
12. [CountdownEvent.Reset][threads-event-sync-12]
13. [CountdownEvent.Reset with arg][threads-event-sync-13]
14. [Barrier: Phases][threads-event-sync-14]
15. [CancellationToken: create token][threads-event-sync-15]
16. [CancellationToken: cancellation request][threads-event-sync-16]
17. [CancellationToken: cancellation callback sequence][threads-event-sync-17]
18. [CancellationToken: cancellation callback sequence: before first exception][threads-event-sync-18]
19. [CancellationToken: cancellation callback sequence: all, ignoring exceptions][threads-event-sync-19]
20. [CancellationToken.ThrowIfCancellationRequested][threads-event-sync-20]
21. [CancellationToken.None usage][threads-event-sync-21]
22. [CancellationToken.None variants][threads-event-sync-22]
23. [CancellationTokenSource.CreateLinkedTokenSource][threads-event-sync-23]

[threads-event-sync-1]:Neomaster.Demos.Tests/Threads/ThreadsEventSyncUnitDemos.cs#L9
[threads-event-sync-2]:Neomaster.Demos.Tests/Threads/ThreadsEventSyncUnitDemos.cs#L74
[threads-event-sync-3]:Neomaster.Demos.Tests/Threads/ThreadsEventSyncUnitDemos.cs#L140
[threads-event-sync-4]:Neomaster.Demos.Tests/Threads/ThreadsEventSyncUnitDemos.cs#L196
[threads-event-sync-5]:Neomaster.Demos.Tests/Threads/ThreadsEventSyncUnitDemos.cs#L252
[threads-event-sync-6]:Neomaster.Demos.Tests/Threads/ThreadsEventSyncUnitDemos.cs#L301
[threads-event-sync-7]:Neomaster.Demos.Tests/Threads/ThreadsEventSyncUnitDemos.cs#L350
[threads-event-sync-8]:Neomaster.Demos.Tests/Threads/ThreadsEventSyncUnitDemos.cs#L374
[threads-event-sync-9]:Neomaster.Demos.Tests/Threads/ThreadsEventSyncUnitDemos.cs#L400
[threads-event-sync-10]:Neomaster.Demos.Tests/Threads/ThreadsEventSyncUnitDemos.cs#L434
[threads-event-sync-11]:Neomaster.Demos.Tests/Threads/ThreadsEventSyncUnitDemos.cs#L447
[threads-event-sync-12]:Neomaster.Demos.Tests/Threads/ThreadsEventSyncUnitDemos.cs#L463
[threads-event-sync-13]:Neomaster.Demos.Tests/Threads/ThreadsEventSyncUnitDemos.cs#L478
[threads-event-sync-14]:Neomaster.Demos.Tests/Threads/ThreadsEventSyncUnitDemos.cs#L493
[threads-event-sync-15]:Neomaster.Demos.Tests/Threads/ThreadsEventSyncUnitDemos.cs#L538
[threads-event-sync-16]:Neomaster.Demos.Tests/Threads/ThreadsEventSyncUnitDemos.cs#L549
[threads-event-sync-17]:Neomaster.Demos.Tests/Threads/ThreadsEventSyncUnitDemos.cs#L576
[threads-event-sync-18]:Neomaster.Demos.Tests/Threads/ThreadsEventSyncUnitDemos.cs#L607
[threads-event-sync-19]:Neomaster.Demos.Tests/Threads/ThreadsEventSyncUnitDemos.cs#L654
[threads-event-sync-20]:Neomaster.Demos.Tests/Threads/ThreadsEventSyncUnitDemos.cs#L701
[threads-event-sync-21]:Neomaster.Demos.Tests/Threads/ThreadsEventSyncUnitDemos.cs#L736
[threads-event-sync-22]:Neomaster.Demos.Tests/Threads/ThreadsEventSyncUnitDemos.cs#L758
[threads-event-sync-23]:Neomaster.Demos.Tests/Threads/ThreadsEventSyncUnitDemos.cs#L775

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
1. [Synchronized method: instance][threads-features-1]
2. [Synchronized method: static][threads-features-2]
3. [Synchronized method: instance in different threads][threads-features-3]
4. [Synchronized method: static in different threads][threads-features-4]
5. [ThreadLocal<T>: counters][threads-features-5]

[threads-features-1]:Neomaster.Demos.Tests/Threads/ThreadsFeaturesUnitDemos.cs#L10
[threads-features-2]:Neomaster.Demos.Tests/Threads/ThreadsFeaturesUnitDemos.cs#L26
[threads-features-3]:Neomaster.Demos.Tests/Threads/ThreadsFeaturesUnitDemos.cs#L41
[threads-features-4]:Neomaster.Demos.Tests/Threads/ThreadsFeaturesUnitDemos.cs#L86
[threads-features-5]:Neomaster.Demos.Tests/Threads/ThreadsFeaturesUnitDemos.cs#L129

### Tasks <a name="tasks"></a>
1. [Thread Pool: QueueUserWorkItem][tasks-1]
2. [Thread Pool: QueueUserWorkItem with state][tasks-2]
3. [Thread Pool: QueueUserWorkItem Join][tasks-3]

[tasks-1]:Neomaster.Demos.Tests/Tasks/TasksUnitDemos.cs#L8
[tasks-2]:Neomaster.Demos.Tests/Tasks/TasksUnitDemos.cs#L25
[tasks-2]:Neomaster.Demos.Tests/Tasks/TasksUnitDemos.cs#L44
