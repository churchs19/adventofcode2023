public static class Utilities
{
    public static string[] ReadFile(string filename)
    {
        var projectFolder = Directory.GetCurrentDirectory();
        return File.ReadAllText(Path.Combine(projectFolder, filename))
            .Split("\n")
            .Where(it => !string.IsNullOrWhiteSpace(it))
            .ToArray();
    }
}