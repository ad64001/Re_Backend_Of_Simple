using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Re_Backend.Common.AutoConfiguration;
using Re_Backend.Common;
using Re_Backend.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Re_Backend.Domain.UserDomain.IRespository;

namespace Re_Backend.Tests
{
    //夹具一式
    public class TestFixture : IDisposable
    {
        public IContainer Container { get; private set; }
        public ITestService TestService { get; private set; }
        public ITestDbService TestDbService { get; private set; }
        public ITestUserService UserService { get; private set; }
        public ITestRedisCacheService TestRedisCache { get; private set; }
        public IUserRespository UserRespository { get; private set; }

        public TestFixture()
        {
            var builder = new ContainerBuilder();

            // Configure service collection
            var services = new ServiceCollection();

            // Load configuration
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Register JsonSettings
            services.AddSingleton(new JsonSettings(configuration));

            // Build service provider
            var serviceProvider = services.BuildServiceProvider();

            // Get JsonSettings instance
            var jsonSettings = serviceProvider.GetRequiredService<JsonSettings>();

            // Register services using AutofacConfig
            AutofacConfig.ConfigureContainer(builder, configuration, "Re_Backend.Domain");

            // Build the container
            Container = builder.Build();

            // Resolve services
            TestService = Container.Resolve<ITestService>();
            TestDbService = Container.Resolve<ITestDbService>();
            UserService = Container.Resolve<ITestUserService>();
            TestRedisCache = Container.Resolve<ITestRedisCacheService>();
            UserRespository = Container.Resolve<IUserRespository>();
        }

        public void Dispose()
        {
            Container?.Dispose();
        }
    }
}
