namespace demo_api.Middlewares;

public class CorsConfiguration
{
    private readonly RequestDelegate _next;
    private const string Origin = "Origin";
    private const string VaryHeader = "Vary";
    private const string AccessControlRequestMethod = "Access-Control-Request-Method";
    private const string AccessControlRequestHeaders = "Access-Control-Request-Headers";
    private const string AccessControlAllowOrigin = "Access-Control-Allow-Origin";
    private const string AccessControlAllowMethods = "Access-Control-Allow-Methods";
    private const string AccessControlAllowHeaders = "Access-Control-Allow-Headers";
    private const string AccessControlAllowCredentials = "Access-Control-Allow-Credentials";

    public CorsConfiguration(RequestDelegate next)
    {
        _next = next;
    }

    public Task Invoke(HttpContext context)
    {
        var isCorsRequest = context.Request.Headers.ContainsKey(Origin);

        context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
        context.Response.Headers.Add(VaryHeader, "Origin");

        return isCorsRequest ? GenerateCorsRequest(context) : _next(context);
    }

    private Task GenerateCorsRequest(HttpContext context)
    {
        var isPreflightRequest = context.Request.Method == HttpMethod.Options.Method;
        var requestOrigin = context.Request.Headers[Origin];

        context.Response.Headers.Add(AccessControlAllowOrigin, requestOrigin);
        context.Response.Headers.Add(AccessControlAllowCredentials, "true");
        context.Response.Headers.Add("Access-Control-Expose-Headers", "*");

        if (isPreflightRequest)
        {
            GenerateAccessControlHeaders(context);
            return Task.CompletedTask;
        }

        return _next(context);
    }

    private void GenerateAccessControlHeaders(HttpContext context)
    {
        var hasAccessControlRequest = context.Request.Headers.ContainsKey(AccessControlRequestMethod);
        if (hasAccessControlRequest)
        {
            var accessControlRequestMethod = context.Request.Headers[AccessControlRequestMethod];
            context.Response.Headers.Add(AccessControlAllowMethods, accessControlRequestMethod);
        }

        var hasAccessControlRequestHeaders = context.Request.Headers.ContainsKey(AccessControlRequestHeaders);

        if (hasAccessControlRequestHeaders)
        {
            var accessControlRequestHeaders = context.Request.Headers[AccessControlRequestHeaders];
            context.Response.Headers.Add(AccessControlAllowHeaders, accessControlRequestHeaders);
        }
    }
}

public static class CorsRequestExtensions
{
    public static IApplicationBuilder UseCorsRequest(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CorsConfiguration>();
    }

}