namespace Middlewares
{
    public class Hello
    {
        private readonly RequestDelegate _next;

        public Hello(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Query.ContainsKey("firstname") && context.Request.Query.ContainsKey("firstname"))
            {
                string fullName = $"{context.Request.Query["firstname"]} {context.Request.Query["lastname"]}";
                await context.Response.WriteAsync(fullName);
            }
            else
            {
                await context.Response.WriteAsync("Sem nome :/");
            }

            await _next(context);
        }
    }

    // * Extension method used to add the middleware to the HTTP request pipeline
    public static class HelloExtension
    {
        public static IApplicationBuilder UseHello(this IApplicationBuilder app)
        {
            return app.UseMiddleware<Hello>();
        }
    }
}