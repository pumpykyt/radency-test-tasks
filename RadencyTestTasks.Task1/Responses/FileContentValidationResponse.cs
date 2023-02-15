namespace RadencyTestTasks.Task1.Responses;

public class FileContentValidationResponse
{
    public bool Status { get; set; }
    public uint? InvalidLinesCount { get; set; }
}