namespace Ultimate.Middlewares
{
    class FirstMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await context.Response.WriteAsync("<h1>Class Middleware</h1>");
            await next(context);
        }
    }

    public static class MiddlewareExtension
    {
        public static IApplicationBuilder UseFirstMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<FirstMiddleware>();
        }
    }
}