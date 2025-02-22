namespace ForoWebApp;

public class DotEnvironment
{
    public static void LoadVariablesFromFileDescriptor(string filepath)
    {
        if (!File.Exists(filepath))
        {
            return;
        }

        foreach (var line in File.ReadAllLines(filepath))
        {
            string[] parts = line.Split('=', 2, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 2)
            {
                continue;
            }

            string key = parts[0].Trim();
            string value = parts[1].Trim();
            Environment.SetEnvironmentVariable(key, value);
        }
    }
}

