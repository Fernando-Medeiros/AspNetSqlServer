namespace WebAPI;

public sealed class Startup
{
    public void ConfigureServices(
        IServiceCollection services)
    {
        var configuration = new ConfigurationBuilder()
             .AddJsonFile("appsettings.json", optional: true)
             .AddJsonFile("appsettings.development.json", optional: true)
             .AddEnvironmentVariables()
             .Build();

        services
           .AddEndpointsApiExplorer()
           .AddRouting()
           .AddSwaggerGen(opt => opt.EnableAnnotations());

        PersistenceServiceExtension
           .ConfigureDatabase(services, configuration);
    }

    public void Configure(
       IApplicationBuilder app,
       IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseRouting();
        app.UseHttpsRedirection();

        app.UseEndpoints(routeBuilder =>
        {
            RotasAlunos.MapGroup(routeBuilder);
            RotasCursos.MapGroup(routeBuilder);
            RotasUniversidades.MapGroup(routeBuilder);
        });
    }
}
