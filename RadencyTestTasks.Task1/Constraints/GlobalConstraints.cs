using System.Text.RegularExpressions;

namespace RadencyTestTasks.Task1.Constraints;

public static class GlobalConstraints
{
    public static readonly Regex RowValidationRegex = new Regex(
        "[A-Za-z]+, [A-Za-z]+, “[A-Za-z]+,\\s[A-Za-z]+ [0-9]+, [0-9]+”, " +
        "([+-]?(?=\\.\\d|\\d)(?:\\d+)?(?:\\.?\\d*))(?:[Ee]([+-]?\\d+))?, " +
        "[0-9]{4}-[0-9]{2}-[0-9]{2}, [0-9]+, [A-Za-z]+", 
        RegexOptions.IgnoreCase
    );
}