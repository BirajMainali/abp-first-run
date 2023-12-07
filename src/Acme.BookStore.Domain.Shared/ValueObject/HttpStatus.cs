using System.Net;

namespace Acme.BookStore.ValueObject;

public class HttpStatus
{
    public dynamic Data { get; set; }
    public string Message { get; set; }
    public HttpStatusCode StatusCode { get; set; }
}