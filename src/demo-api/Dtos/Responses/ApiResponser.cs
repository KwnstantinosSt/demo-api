using Newtonsoft.Json;

namespace demo_api.Dtos.Responses
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class ApiResponser
    {
        public bool Success { get; }
        public string Message { get; }
        public string? ErrorCode { get; }

        public ApiResponser(string code, string message)
        {
            ErrorCode = code.ToString();
            Success = false;
            Message = message;
        }

        public ApiResponser(string message = "Completed")
        {
            Success = true;
            Message = message;
        }
    }

    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class ApiResponser<T> : ApiResponser
    {
        public T? Data { get; }

        public ApiResponser(T data, string message = "Completed") : base(message)
        {
            Data = data;
        }

        public ApiResponser(T data, string code, string message) : base(code, message)
        {
            Data = data;
        }

        public ApiResponser(string code, string message) : base(code, message) { }

    }

}