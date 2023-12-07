using System.Net;
using Acme.BookStore.UseCase;
using Acme.BookStore.ValueObject;
using Volo.Abp.Application.Services;

namespace Acme.BookStore.Extensions;

public static class HttpResponseExtension
{
    public static HttpStatus ResolveResponse<T>(this ApplicationService service, Result<T> result) where T : class
    {
        var status = new HttpStatus
        {
            Data = result.Value,
            Message = result.Message,
            StatusCode = result.IsSuccess ? HttpStatusCode.Created : HttpStatusCode.BadGateway
        };
        return status;
    }
}