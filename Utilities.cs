public static class Utilities
{
    public static string[] ReadFile(string filename)
    {
        var projectFolder = Directory.GetCurrentDirectory();
        return File.ReadAllText(Path.Combine(projectFolder, filename))
            .TrimEnd()
            .Split("\n")
            .Where(it => !string.IsNullOrWhiteSpace(it))
            .ToArray();
    }

    public static string[] ReadFileKeepBlankLines(string filename)
    {
        var projectFolder = Directory.GetCurrentDirectory();
        return File.ReadAllText(Path.Combine(projectFolder, filename))
            .TrimEnd()
            .Split("\n")
            .ToArray();
    }
}