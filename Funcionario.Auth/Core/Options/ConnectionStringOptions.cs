namespace Core.Options;

public class ConnectionStringOptions
{
    public const string SectionName = "ConnectionString";
    public string DefaultConnection { get; set; } = string.Empty;
}