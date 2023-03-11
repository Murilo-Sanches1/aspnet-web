internal class Program
{
    private static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        // + builder.Environment;
        // + builder.Configuration;

        WebApplication app = builder.Build();

        // + MapGet só retorna um valor
        // app.MapGet("/", () => "Olá vindo do C# !");

        app.Run(async (HttpContext ctx) =>
        {
            ctx.Response.Headers["MeuHeader"] = "Headers = Dictionary";

            // + para alterar o server que por padrão é o Kestrel
            ctx.Response.Headers["Server"] = "Apache";

            ctx.Response.StatusCode = 400;
            // + falando que o conteúdo vai ser html, o browser vai transformar as tags
            // + charset=utf-8 permite que o browser interprete corretamente os acentos
            ctx.Response.Headers["Content-Type"] = "text/html; charset=utf-8";
            await ctx.Response.WriteAsync("<h1>C Sharp é legal</h1>");
            await ctx.Response.WriteAsync("<h2>Tudo que for passado aqui será passado como body na response</h2>");

            PathString path = ctx.Request.Path;
            string method = ctx.Request.Method;
            Console.WriteLine(path);
            Console.WriteLine(method);
        });

        // app.Use();

        app.Run();
    }
}