using System.Text.Json.Serialization;

namespace ProjectTwo.Entities.Response
{
    public class OperationResult<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ErrorMessage { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ErrorType{ get; set; }

        public OperationResult<T> Ok(T data) => new OperationResult<T>
        {
            Success = true,
            Data = data,
        };

        public OperationResult<T> Fail(string errorMessage, string errorType) => new OperationResult<T>
        {
            Success = false,
            ErrorMessage = errorMessage,
            ErrorType = errorType
        };
    }
}
