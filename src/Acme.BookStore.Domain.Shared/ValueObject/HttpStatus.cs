using System.Net;

namespace Acme.BookStore.ValueObject;

public class HttpStatus
{
    public object Data { get; set; } = new();
    public string Message { get; set; }
    public HttpStatusCode StatusCode { get; set; }
}