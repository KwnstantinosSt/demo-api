using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Net;
using System.Text.Json;
using demo_api.Dtos.Responses;

namespace demo_api.Middlewares;

public class CustomErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public CustomErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            switch (error)
            {
                case ValidationException e:
                    response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                    break;
                case DbException e:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case KeyNotFoundException e:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                default:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
            }

            var result = new ApiResponser(response.StatusCode.ToString(), error.Message);
            var res = JsonSerializer.Serialize(result);
            await response.WriteAsync(res);
        }
    }
}

public static class ErrorHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseCustomErrorHandlerMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomErrorHandlerMiddleware>();
    }
}