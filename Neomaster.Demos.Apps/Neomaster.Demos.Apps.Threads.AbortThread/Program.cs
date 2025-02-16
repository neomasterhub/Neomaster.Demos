using System;
using System.Threading;

namespace Neomaster.Demos.Apps.Threads.AbortThread
{
  internal class ThreadAbortExceptionInfo
  {
    public int AbortedById { get; set; }
  }

  internal class Program
  {
    static void Main(string[] args)
    {
      var th1 = new Thread(() =>
      {
        while (true)
        {
          Thread.Sleep(10);
          Console.Write("*");
        }
      });

      var th2 = new Thread(() =>
      {
        try
        {
          while (true)
          {
            Thread.Sleep(10);
            Console.Write("@");
          }
        }
        catch (ThreadAbortException e)
        {
          var info = (ThreadAbortExceptionInfo)e.ExceptionState;
          Console.WriteLine($"\nAborted by thread with id = {info.AbortedById}");
        }
      });

      var th3 = new Thread(() =>
      {
        Thread.Sleep(100);
        th1.Abort();
        th2.Abort(new ThreadAbortExceptionInfo
        {
          AbortedById = Thread.CurrentThread.ManagedThreadId,
        });
      });

      th1.Start();
      th2.Start();
      th3.Start();
      th3.Join();

      // *@**@*@@**@@*
      // Aborted by thread with id = 5
    }
  }
}
