using Accolite.Bank.API.Configuration;
using Accolite.Bank.Data.MsSql.Configuration;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;

namespace Accolite.Bank.API;

public class Startup
{
    private const string SwaggerPath = "swagger";

    private readonly string _assemblyName;

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
        _assemblyName = Assembly.GetExecutingAssembly().GetName().Name ?? GetType().Namespace!;
    }

    private IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.RegisterRepositories(Configuration.GetConnectionString("AccoliteBank")!);
        services.AddControllers()
            .AddJsonOptions(options => options.JsonSerializerOptions.ConfigureJsonSerializerOptions());

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options => AddSwaggerXml(options, _assemblyName));
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        ConfigureSwaggerPipeline(app);

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }

    private static void AddSwaggerXml(SwaggerGenOptions options, string assemblyName)
    {
        var xmlFileName = $"{assemblyName}.xml";
        var xmlFile = Directory.GetFiles(AppContext.BaseDirectory, xmlFileName).FirstOrDefault();
        if (xmlFile != null)
        {
            options.IncludeXmlComments(xmlFile);
        }
    }

    private void ConfigureSwaggerPipeline(IApplicationBuilder app)
    {
        app.MapWhen(
            ctx => ctx.Request.Method == HttpMethod.Get.Method
                   && ctx.Request.Path.StartsWithSegments($"/{SwaggerPath}", StringComparison.Ordinal),
            builder =>
            {
                builder.UseSwagger(c => c.RouteTemplate = $"{SwaggerPath}/{{documentname}}/swagger.json");
                builder.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint($"/{SwaggerPath}/v1/swagger.json", $"{_assemblyName} V1");
                    c.RoutePrefix = SwaggerPath;
                    c.DocExpansion(DocExpansion.None);
                });
            }
        );
    }
}
