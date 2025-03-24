using Infrastructure;
using Application;
using Scalar.AspNetCore;
using Web.Extensions;

namespace Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.ConfigureInfrastructure(builder.Configuration);
            builder.Services.ConfigureApplication();

            builder.Services.ConfigureApiBehavior();
            builder.Services.ConfigureCorsPolicy();

            builder.Services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger(opt =>
                {
                    opt.RouteTemplate = "openapi/{documentName}.json";
                });
                app.MapScalarApiReference(opt =>
                {
                    opt.Title = "Scalar Example";
                    opt.Theme = ScalarTheme.DeepSpace;
                    opt.DefaultHttpClient = new(ScalarTarget.Http, ScalarClient.Http11);
                });
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseErrorHandler();
            app.UseCors();
            app.MapControllers();
            app.Run();
        }
    }
}
