using System.Text.RegularExpressions;

namespace RadencyTestTasks.Task1.Global;

public static class GlobalVariables
{
    public static string SettingFileName { get; } = "appsettings.json";
    public static Regex ValidationRegex = new (
        "[A-Za-z]+, [A-Za-z]+, “[A-Za-z]+,\\s[A-Za-z]+ [0-9]+, [0-9]+”, " +
        "([+-]?(?=\\.\\d|\\d)(?:\\d+)?(?:\\.?\\d*))(?:[Ee]([+-]?\\d+))?, " +
        "[0-9]{4}-[0-9]{2}-[0-9]{2}, [0-9]+, [A-Za-z]+", 
        RegexOptions.IgnoreCase
    );
    public static uint ParsedFiles = 0;
    public static uint ParsedLines = 0;
    public static uint FoundErrors = 0;
    public static List<string> InvalidFileNames = new();

    public new static string ToString() => $"parsed_files: {ParsedFiles}\n" +
                                           $"parsed_lines: {ParsedLines}\n" +
                                           $"found_errors: {FoundErrors}\n" +
                                           $"invalid_files: [{string.Join(' ', InvalidFileNames)}]";
}