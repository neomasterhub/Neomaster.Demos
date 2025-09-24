namespace Neomaster.Demos.Tests.Tasks;

public class AlarmInfo
{
  public string Name { get; set; }
  public bool Enabled { get; set; }
  public TimeOnly Time { get; set; }
  public DateOnly? LastTriggeredDate { get; set; }
}
