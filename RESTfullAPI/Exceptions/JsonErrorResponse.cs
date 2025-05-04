namespace RESTfullAPI.Exceptions;

public class JsonErrorResponse
{
    public string Message { get; set; }
    public string? DeveloperMessage { get; set; }
    public Dictionary<string, string[]> Errors { get; set; }

    public JsonErrorResponse() 
    { 
    }

    public JsonErrorResponse(string message)
    {
        Message = message;
    }

    public JsonErrorResponse(string message, Dictionary<string, string[]> errors)
    {
        Message = message;
        Errors = errors;
    }

    public JsonErrorResponse(string message, string? developerMessage)
    {
        Message = message;
        DeveloperMessage = developerMessage;
    }
}
