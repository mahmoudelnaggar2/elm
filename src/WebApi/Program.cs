using Application;
using Infrastructure;
using Presentation;
using Serilog;

namespace WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services
            .AddApplication()
            .AddInfrastructure()
            .AddPresentation();

        builder.Host.UseSerilog((context, configuration) =>
        {
            configuration.ReadFrom.Configuration(context.Configuration);
        });

        const string AllowOrigins_Testing = "AllowOrigins_Testing";
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(
                name: AllowOrigins_Testing,
                builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                }
                );
        });

        builder.Services.AddControllers().AddApplicationPart(AssemblyReference.Assembly);

        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }


        app.UseSerilogRequestLogging();

        app.UseHttpsRedirection();

        app.UseCors(AllowOrigins_Testing);

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}