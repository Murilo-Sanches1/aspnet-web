using Middlewares;
using Constraints;

internal class Program
{
    private static void Main(string[] args)
    {
        server(args);
    }

    private static void server(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        // + builder.Environment;
        // + builder.Configuration;

        // + dependency injection, a classe vai estar pronta para extender FirstMiddleware quando precisar
        builder.Services.AddTransient<FirstMiddleware>();
        builder.Services.AddTransient<Headers>();
        builder.Services.AddAuthorization();
        builder.Services.AddAuthentication();
        builder.Services.AddControllers();
        builder.Services.AddRouting(options =>
        {
            options.ConstraintMap.Add("salesReport", typeof(SalesReport));
        });

        WebApplication app = builder.Build();

        // - MapGet só retorna um valor
        // app.MapGet("/", () => "Olá vindo do C# !");

        app.UseExceptionHandler("/Error");
        app.UseHsts();
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        // + endpoint = null
        app.Use(async (context, next) =>
        {
            Endpoint endpoint = context.GetEndpoint();
            Console.WriteLine(endpoint);
            await next(context);
        });
        app.UseRouting();
        app.UseCors();
        app.UseAuthentication();
        app.UseAuthorization();
        // app.UseSession();
        app.MapControllers();

        app.UseHeaders();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGet("/get1", async (context) =>
            {
                await context.Response.WriteAsync("Map-get-1");
            });

            endpoints.MapGet("/files/{filename}.{extension}", async (context) =>
            {
                String? filename = (string?)context.Request.RouteValues["filename"];
                String? extension = (string?)context.Request.RouteValues["extension"];
                foreach (var item in context.Request.RouteValues)
                {
                    Console.WriteLine(item);
                }
                await context.Response.WriteAsync($"Requisição de files pela URL - {filename}.{extension}");
            });

            endpoints.MapGet("/programmers/{name=murilo}", async (context) =>
            {
                await context.Response.WriteAsync($"Programador: {context.Request.RouteValues["name"]}");
            });


            endpoints.MapGet("/product/{id:int?}", async (context) =>
            {
                await context.Response.WriteAsync($"Produto: {context.Request.RouteValues["id"]}");
            });

            // endpoints.MapGet(
            //     "/sales-report/{year:int:min(1900)}/{month:regex(^(apr|jul|oct|jan)$)}",
            //     async (context) =>
            //     {
            //         Int32 year = Convert.ToInt32(context.Request.RouteValues["year"]);
            //         String? month = (string?)context.Request.RouteValues["month"];
            //         await context.Response.WriteAsync($"Pesquisando vendas no ano de {year} mês {month}");
            //     });
            endpoints.MapGet(
               "/sales-report/{year:int:min(1900)}/{month:salesReport}",
               async (context) =>
               {
                   Int32 year = Convert.ToInt32(context.Request.RouteValues["year"]);
                   String? month = (string?)context.Request.RouteValues["month"];
                   await context.Response.WriteAsync($"Pesquisando vendas no ano de {year} mês {month}");
               });
        });

        app.Use(async (HttpContext context, RequestDelegate next) =>
        {
            await context.Response.WriteAsync("<h1>Primeira middleware</h1>");
            await next(context);
        });

        // - app.UseMiddleware<FirstMiddleware>();
        app.UseFirstMiddleware();
        app.UseHello();
        app.UseLogger();
        app.UseWhen((HttpContext context) =>
        {
            return context.Request.Path.ToString().StartsWith("/api");
        }, (IApplicationBuilder app) =>
        {
            app.Use(async (HttpContext context, RequestDelegate next) =>
        {
            await context.Response.WriteAsync("</br> API - middleware branch");
            await next(context);
        });
        });

        // + app.Run((context) => {}) é a última da pipeline porque esse método não tem acesso ao next(context)
        app.Run(async (HttpContext context) =>
        {
            await context.Response.WriteAsync("<h1>C Sharp é legal</h1>");
            await context.Response.WriteAsync("<h2>Tudo que for passado aqui será passado como body na response</h2>");

            PathString path = context.Request.Path;
            string method = context.Request.Method;
            Console.WriteLine(path);
            Console.WriteLine(method);

            if (context.Request.Method == "GET")
            {
                if (context.Request.Query.ContainsKey("id"))
                {
                    Microsoft.Extensions.Primitives.StringValues stringValues = context.Request.Query["id"];
                    await context.Response.WriteAsync($"<h3>{context.Request.Query["id"]}</h3>");
                }
            }

            StreamReader reader = new StreamReader(context.Request.Body);
            string body = await reader.ReadToEndAsync();
            Console.WriteLine(body);
        });


        app.Run();
    }
}