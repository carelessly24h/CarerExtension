namespace CarerConsole;

public class AppSettings : JsonIO<AppSettings>
{
    public string ConnectionString { get; set; } = null!;
}
