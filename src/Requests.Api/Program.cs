using Requests.Api.Extensions;
using Requests.Api.Middlewares;

namespace Requests.Api
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddCors();
            builder.Services.AddMvc();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                var xmlFilename = $"doc.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
            builder.Services.AddApiVersioning();
            builder.Services.RegisterLayers(builder.Configuration);
            builder.Services.RegisterOptions(builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            await app.AutoMigrate();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(cpb => cpb.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseAuthorization();
            app.MapControllers();

            app
                .UsePathBase("/api")
                .UseRouting();

            await app.RunAsync();
        }
    }
}
