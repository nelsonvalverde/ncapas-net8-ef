namespace WebApi.Business.Common.Responses.Base;

public class ResponseBase<T>
{
    public int Status { get; set; }
    public bool Succeeded { get; set; }
    public string Message { get; set; }
    public object? Errors { get; set; }
    public T? Data { get; set; } 

    public ResponseBase(T data, string message = "Success")
    {
        Status = 200;
        Succeeded = true;
        Message = message;
        Data = data;
    }

    public ResponseBase(string message = "Success")
    {
        Status = 200;
        Succeeded = true;
        Message = message;
    }
}