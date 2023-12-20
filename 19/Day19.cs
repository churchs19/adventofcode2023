using System.Text.RegularExpressions;

public static class Day19
{
    public static void Part1()
    {
        var input = Utilities.ReadFileKeepBlankLines("19/input.txt");

        (var workflows, var parts) = ParseInput(input);

        int acceptedPartsSum = parts.Sum(it => workflows["in"].IsPartAccepted(it, workflows) ? it.Sum(item => item.Value) : 0);

        Console.WriteLine($"Day 19 - Part 1: {acceptedPartsSum}");
    }

    public static void Part2()
    {
        var input = Utilities.ReadFileKeepBlankLines("19/input.txt");

        (var workflows, var parts) = ParseInput(input);

        var ranges = new Dictionary<char, Range>() {
            { 'x', new Range() },
            { 'm', new Range() },
            { 'a', new Range() },
            { 's', new Range() }
        };

        
    }

    private static (Dictionary<string, Workflow>, List<Dictionary<char, int>>) ParseInput(string[] input)
    {
        var rules = input.TakeWhile(it => !string.IsNullOrWhiteSpace(it));

        Regex re = new("^(?'key'.+){(?'steps'.*),(?'defaultTarget'.+)}");

        Dictionary<string, Workflow> workflows = [];

        foreach (var rule in rules)
        {
            var workflowParts = re.Match(rule);
            var key = workflowParts.Groups["key"].Value;
            var defaultTarget = workflowParts.Groups["defaultTarget"].Value;

            List<Command> commands = [];
            foreach (var step in workflowParts.Groups["steps"].Value.Split(','))
            {
                char section = step[0];
                char comparator = step[1];
                int comparisonValue = int.Parse(step.Split(['<', '>', ':'])[1]);
                string target = step.Split(":")[^1];

                commands.Add(new Command(section, comparator, comparisonValue, target));
            }

            workflows[key] = new Workflow(commands, defaultTarget);
        }

        List<Dictionary<char, int>> parts = [];
        foreach (var partString in input.Skip(rules.Count() + 1).Select(it => it))
        {
            var pieces = partString.Split(['{', '}', ',']).Where(it => !string.IsNullOrWhiteSpace(it)).Select(it =>
            {
                var values = it.Split('=');
                return new { key = values[0], value = int.Parse(values[1]) };
            }).ToDictionary(it => it.key[0], it => it.value);
            parts.Add(pieces);
        }

        return (workflows, parts);
    }

    internal class Range(int start = 1, int end = 4000)
    {
        internal int Start { get; private set; } = start;
        internal int End { get; private set; } = end;
    }

    internal class Workflow(List<Command> commands, string defaultTarget)
    {
        internal List<Command> Commands { get; private set; } = commands;
        internal string DefaultTarget { get; private set; } = defaultTarget;

        internal bool IsPartAccepted(Dictionary<char, int> part, Dictionary<string, Workflow> workflows)
        {
            foreach (var command in Commands)
            {
                int checkValue = part[command.Section];

                bool comparisonResult = command.Comparator switch
                {
                    '<' => checkValue < command.ComparisonValue,
                    '>' => checkValue > command.ComparisonValue,
                    _ => throw new ArgumentException(nameof(command.Comparator))
                };

                if (!comparisonResult)
                {
                    continue;
                }

                return command.Target switch
                {
                    "A" => true,
                    "R" => false,
                    _ => workflows[command.Target].IsPartAccepted(part, workflows)
                };
            }

            return DefaultTarget switch
            {
                "A" => true,
                "R" => false,
                _ => workflows[DefaultTarget].IsPartAccepted(part, workflows)
            };
        }
    }

    internal class Command(char section, char comparator, int comparisonValue, string target)
    {
        internal char Section { get; private set; } = section;
        internal char Comparator { get; private set; } = comparator;
        internal int ComparisonValue { get; private set; } = comparisonValue;
        internal string Target { get; private set; } = target;
    }
}