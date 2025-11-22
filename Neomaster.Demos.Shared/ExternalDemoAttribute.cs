namespace Neomaster.Demos.Shared;

[AttributeUsage(AttributeTargets.All)]
public class ExternalDemoAttribute(string displayName, string src)
  : Attribute
{
  public string DisplayName { get; } = displayName;
  public string Src { get; } = src;
}
