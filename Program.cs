internal class Program
{
    private static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder();

        builder.Services.AddControllers();
        builder.Services.AddControllers().AddXmlSerializerFormatters();

        WebApplication app = builder.Build();

        app.UseRouting();
        app.UseStaticFiles();
        
        app.UseEndpoints((endpoints) =>
        {
            endpoints.MapControllers();
        });

        app.Run(async (context) =>
        {
            await context.Response.WriteAsync("404");
        });

        app.Run();
    }
}