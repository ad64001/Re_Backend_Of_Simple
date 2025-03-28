using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Re_Backend.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Re_Backend.Tests
{
    public class JsonSettingsTests
    {
        private readonly ITestOutputHelper testOutput;

        public JsonSettingsTests(ITestOutputHelper testOutput)
        {
            this.testOutput = testOutput;
        }

        [Fact]
        public void TestJsonSettingsConfiguration()
        {
            // 配置服务集合
            var services = new ServiceCollection();

            // 模拟配置
            var configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .Build();

            // 注册 JsonSettings
            services.AddSingleton(new JsonSettings(configuration));

            // 构建服务提供者
            var serviceProvider = services.BuildServiceProvider();

            // 获取 JsonSettings 实例
            var jsonSettings = serviceProvider.GetRequiredService<JsonSettings>();

            // 验证配置是否正确加载
            Assert.NotNull(jsonSettings);
        }

        [Fact]
        public void TestGetValue()
        {
            // 配置服务集合
            var services = new ServiceCollection();

            // 模拟配置
            var configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .Build();

            // 注册 JsonSettings
            services.AddSingleton(new JsonSettings(configuration));

            // 构建服务提供者
            var serviceProvider = services.BuildServiceProvider();

            // 获取 JsonSettings 实例
            var jsonSettings = serviceProvider.GetRequiredService<JsonSettings>();

            // 假设 appsettings.json 中有一个 AllowedHosts 配置项
            var allowedHosts = JsonSettings.GetValue("Logging:LogLevel:Microsoft.AspNetCore");

            testOutput.WriteLine(allowedHosts);

            // 验证获取的值是否正确
            Assert.NotNull(allowedHosts);
            
        }
    }
}
