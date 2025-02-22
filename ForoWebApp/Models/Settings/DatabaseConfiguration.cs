namespace ForoWebApp.Models.Settings;

public class DatabaseConfiguration(string? connectionString = null, string? databaseName = null)
{
    public string ConnectionString { get; set; } = connectionString ?? string.Empty;
    public string DatabaseName { get; set; } = databaseName ?? string.Empty;
}
