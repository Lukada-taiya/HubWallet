
namespace HubWally.Application
{ 
    public class ApiResponse
    {
        public int? Id { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public object? Body { get; set; }
    }
}
