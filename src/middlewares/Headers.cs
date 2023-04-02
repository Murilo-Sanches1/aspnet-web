namespace Ultimate.Middlewares
{
    class Headers : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            // + falando que o conteúdo vai ser html, o browser vai transformar as tags
            // + charset=utf-8 permite que o browser interprete corretamente os acentos
            context.Response.Headers["Content-Type"] = "text/html; charset=utf-8";

            // + Headers são do tipo Dictionary
            // context.Response.Headers["MeuHeader"] = "Headers = Dictionary";

            // + para alterar o server que por padrão é o Kestrel
            // context.Response.Headers["Server"] = "Apache";

            // context.Response.StatusCode = 400;

            await next(context);
        }

        // private void LoopHeaders(HttpContext context)
        // {
        //     // + ver todas as propriedades de um Dictionary 
        //     foreach (System.ComponentModel.PropertyDescriptor descriptor in System.ComponentModel.TypeDescriptor.GetProperties(context.Request.Headers))
        //     {
        //         string name = descriptor.Name;
        //         object? value = descriptor.GetValue(context.Request.Headers);
        //         Console.WriteLine("{0}={1}", name, value);
        //     }
        // }
    }

    public static class HeadersExtension
    {
        public static IApplicationBuilder UseHeaders(this IApplicationBuilder app)
        {
            return app.UseMiddleware<Headers>();
        }
    }
}