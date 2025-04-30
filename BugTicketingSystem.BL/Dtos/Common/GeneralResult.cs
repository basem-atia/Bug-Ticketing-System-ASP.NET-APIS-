namespace BugTicketingSystem.BL;

public class GeneralResult
{
    public bool Success { get; set; }
    public ResultError[] Errors { get; set; } = [];
}
public class GeneralResult<T> : GeneralResult where T : class
{
    public T? Data { get; set; }
}
public class ResultError
{
    public string Code { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string? PropertyName { get; set; } = string.Empty;
    public string? AttemptedValue { get; set; } = string.Empty;
}
