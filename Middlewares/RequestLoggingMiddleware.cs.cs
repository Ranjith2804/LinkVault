namespace LinkVault.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate next;
        public RequestLoggingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
                var start= DateTime.UtcNow;
            Console.WriteLine($"[{start:HH:mm:ss.fff}] --> {context.Request.Method} {context.Request.Path}");
            await next(context);
            var duration = (DateTime.UtcNow - start).TotalMilliseconds;
            Console.WriteLine($"[{DateTime.UtcNow:HH:mm:ss.fff}] <-- {context.Response.StatusCode} in {duration:F1}ms");
            

        }
    }
}
