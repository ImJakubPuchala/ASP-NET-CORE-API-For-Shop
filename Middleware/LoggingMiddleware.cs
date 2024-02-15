namespace ASP_NET_CORE_API_For_Shop.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine($"[Request] {context.Request.Method} {context.Request.Path}");

            await _next(context);

            Console.WriteLine($"[Response] {context.Response.StatusCode}");
        }
    }
}
