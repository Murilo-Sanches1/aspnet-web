namespace Middlewares
{
    public class Logger
    {
        private readonly RequestDelegate _next;

        public Logger(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Logger");
            Console.ResetColor();
            await _next(context);
        }
    }

    public static class LoggerExtension
    {
        public static IApplicationBuilder UseLogger(this IApplicationBuilder app)
        {
            return app.UseMiddleware<Logger>();
        }
    }
}