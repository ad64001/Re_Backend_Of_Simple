using Autofac;
using Autofac.Extensions.DependencyInjection;
using Re_Backend.Common;
using Re_Backend.Common.AutoConfiguration;
using Re_Backend.Infrastructure;

namespace Re_Backend.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // ≈‰÷√ª∫¥Ê
            //’‚¿Ô «Autofacµƒ≈‰÷√
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
            {
                AutofacConfig.ConfigureContainer(containerBuilder, builder.Configuration, "Re_Backend.Infrastructure", "Re_Backend.Domain", "Re_Backend.Application", "Re_Backend.Common");
            });
            //JsonSetting≈‰÷√
            // ≈‰÷√
            builder.Services.AddSingleton(new JsonSettings(builder.Configuration));
            
            // Add services to the container.

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

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
