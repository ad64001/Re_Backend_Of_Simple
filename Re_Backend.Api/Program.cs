using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Re_Backend.Common;
using Re_Backend.Common.AutoConfiguration;
using Re_Backend.Common.Jwt;
using System.Text;

namespace Re_Backend.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 添加控制器服务
            builder.Services.AddControllers(options =>
            {
                // 注册全局异常过滤器
                options.Filters.Add<GlobalExceptionFilter>();
            });
            // 配置缓存
            //这里是Autofac的配置
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
            {
                AutofacConfig.ConfigureContainer(containerBuilder, builder.Configuration, "Re_Backend.Infrastructure", "Re_Backend.Domain", "Re_Backend.Application", "Re_Backend.Common");
            });
            //JsonSetting配置
            // 配置
            builder.Services.AddSingleton(new JsonSettings(builder.Configuration));


            // 添加 JWT 验证服务
            builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

            // 添加认证服务
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings.Key))
                };
            });
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

            // 使用跨域中间件
            app.UseCorsMiddleware();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
