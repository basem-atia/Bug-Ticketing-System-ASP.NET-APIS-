using BugTicketingSystem.BL;

namespace BugTicketingSystem;

public static class Functions
{
    public static GeneralResult<T> Success<T>(T data) where T : class
    {
        return new GeneralResult<T>
        {
            Success = true,
            Errors = [],
            Data = data
        };
    }
    public static GeneralResult Fail(ResultError[] errors)
    {
        return new GeneralResult
        {
            Success = false,
            Errors = errors
        };
    }
    public static GeneralResult Error(string code, string message)
    {
        return new GeneralResult
        {
            Success = false,
            Errors = [new ResultError { Code = code, Message = message }]
        };
    }
}
