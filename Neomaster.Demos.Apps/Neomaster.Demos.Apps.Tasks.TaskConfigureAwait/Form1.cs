using Xunit;

namespace Neomaster.Demos.Apps.Tasks.TaskConfigureAwait;

public partial class Form1 : Form
{
  public Form1()
  {
    InitializeComponent();
    textBox_time.Text = "";
  }

  private async void button_ca_true_Click(object sender, EventArgs e)
  {
    textBox_time.Text = "...";

    var th1 = Thread.CurrentThread;
    var text = await GetCurrentTime().ConfigureAwait(true);
    var th2 = Thread.CurrentThread;

    Assert.Equal(th1.ManagedThreadId, th2.ManagedThreadId);
    Assert.NotNull(SynchronizationContext.Current);

    textBox_time.Text = text;
  }

  private async void button_ca_false_Click(object sender, EventArgs e)
  {
    textBox_time.Text = "...";

    var th1 = Thread.CurrentThread;
    var text = await GetCurrentTime().ConfigureAwait(false);
    var th2 = Thread.CurrentThread;

    Assert.NotEqual(th1.ManagedThreadId, th2.ManagedThreadId);
    Assert.Null(SynchronizationContext.Current);

    try
    {
      textBox_time.Text = text;
      // System.InvalidOperationException:
      // 'Недопустимая операция в нескольких потоках:
      // попытка доступа к элементу управления 'textBox_time'
      // не из того потока, в котором он был создан.'
    }
    catch (InvalidOperationException ex)
    {
      textBox_time.Invoke(() => textBox_time.Text = ex.Message);
    }
  }

  private static async Task<string> GetCurrentTime()
  {
    await Task.Delay(1000);

    return DateTime.Now.ToString("HH:mm:ss");
  }
}
