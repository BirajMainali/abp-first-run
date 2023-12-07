using System.Net;
using System.Xml.Schema;
using Acme.BookStore.UseCase;
using Acme.BookStore.ValueObject;
using Volo.Abp.Application.Services;

namespace Acme.BookStore.Extensions;

public static class HttpResponseExtension
{
    public static HttpStatus ResolveResponse<T>(this ApplicationService service, Result<T> result) where T : class
    {
        var status = GetStatus(result);
        return status;
    }

    public static HttpStatus ResolveResponse(this ApplicationService service, object data)
    {
        return new HttpStatus
        {
            Data = data,
            Message = "Success",
            StatusCode = HttpStatusCode.OK
        };
    }

    private static HttpStatus GetStatus<T>(Result<T> result) where T : class
    {
        return new HttpStatus
        {
            Data = result.Value,
            Message = result.Message,
            StatusCode = result.IsSuccess ? HttpStatusCode.OK : HttpStatusCode.BadGateway
        };
    }
}