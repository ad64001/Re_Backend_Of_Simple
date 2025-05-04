using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Re_Backend.Common;
using Re_Backend.Common.AutoConfiguration;
using Re_Backend.Domain.CommonDomain.IRespository;
using Re_Backend.Domain.CommonDomain.IServices;
using Re_Backend.Domain.Other;
using Re_Backend.Domain.UserDomain.IRespository;
using Re_Backend.Domain.UserDomain.IServices;
using System.Text;

namespace Re_Backend.Tests
{
    //夹具一式
    public class TestFixture : IDisposable
    {
        public IContainer Container { get; private set; }
        public ITestService TestService { get; private set; }
        public ITestDbService TestDbService { get; private set; }
        public ITestUserService TestUserService { get; private set; }
        public ITestRedisCacheService TestRedisCache { get; private set; }
        public IUserRespository UserRespository { get; private set; }
        public IRolesRespository RoleRespository { get; private set; }
        public ILoginService LoginService { get; private set; }

        public IUserService UserService { get; private set; }
        public IPagesRespository PagesRespository { get; private set; }
        public IPageService PageService { get; private set; }
        public TestFixture()
        {
            var builder = new ContainerBuilder();

            // Load configuration
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Configure service collection
            var services = new ServiceCollection();

            // 添加 JWT 配置
            services.Configure<JwtSettings>(configuration.GetSection("Jwt"));

            // 添加认证服务
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var jwtSettings = configuration.GetSection("Jwt").Get<JwtSettings>();
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

            // Register JsonSettings
            services.AddSingleton(new JsonSettings(configuration));

            // 将 IServiceCollection 中的服务注册转移到 Autofac
            builder.Populate(services);

            // Register services using AutofacConfig
            AutofacConfig.ConfigureContainer(builder, configuration, "Re_Backend.Infrastructure", "Re_Backend.Domain", "Re_Backend.Application", "Re_Backend.Common");

            // Build the container
            Container = builder.Build();

            // Resolve services
            TestService = Container.Resolve<ITestService>();
            TestDbService = Container.Resolve<ITestDbService>();
            TestUserService = Container.Resolve<ITestUserService>();
            TestRedisCache = Container.Resolve<ITestRedisCacheService>();
            UserRespository = Container.Resolve<IUserRespository>();
            RoleRespository = Container.Resolve<IRolesRespository>();
            LoginService = Container.Resolve<ILoginService>();
            UserService = Container.Resolve<IUserService>();
            PagesRespository = Container.Resolve<IPagesRespository>();
            PageService = Container.Resolve<IPageService>();
        }

        public void Dispose()
        {
            Container?.Dispose();
        }
    }
}
