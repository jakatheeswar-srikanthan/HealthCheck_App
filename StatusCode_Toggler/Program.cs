
using System.Collections.Concurrent;

namespace StatusCode_Toggler
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularApp",
                    policy => policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //var statusCode = new ConcurrentDictionary<string, int>();
            //statusCode["current"] = 200;

            //app.MapGet("/api/status", () => 
            //Results.Json(new 
            //{ 
            //    status = statusCode["current"] 
            //}));

            //app.MapPost("/api/set-status/{code:int}", (int code) =>
            //{
            //    statusCode["current"] = code;
            //    return Results.Json(new { message = $"Status code set to {code}" });
            //});

            //app.MapGet("/api/check-status", (HttpContext context) =>
            //{
            //    context.Response.StatusCode = statusCode["current"];
            //    return Results.Json(new { status = statusCode["current"], message = "Response status set dynamically" });
            //});

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors("AllowAngularApp");

            app.MapControllers();

            app.Run();
        }
    }
}
