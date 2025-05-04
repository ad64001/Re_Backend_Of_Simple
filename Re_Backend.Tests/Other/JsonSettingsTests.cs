using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Re_Backend.Common;
using Xunit.Abstractions;

namespace Re_Backend.Tests.Other
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

            //testOutput.WriteLine(allowedHosts);

            //// 验证获取的值是否正确
            //Assert.NotNull(allowedHosts);

            //try
            //{
            //    // 创建 SqlDefault 类的实例
            //    SqlDefault sqlDefault = new SqlDefault();

            //    // 获取 ConnectionString 和 DbType
            //    string connectionString = sqlDefault.ConnectionString;
            //    int dbType = sqlDefault.DbType;

            //    // 输出结果
            //    testOutput.WriteLine($"Default ConnectionString: {connectionString}");
            //    testOutput.WriteLine($"Default DbType: {dbType}");

            //    // 你也可以在这里添加断言来验证结果
            //    Assert.NotNull(connectionString);
            //    Assert.True(dbType >= 0);
            //}
            //catch (Exception ex)
            //{
            //    // 处理异常，例如记录日志或输出错误信息
            //    testOutput.WriteLine($"An error occurred: {ex.Message}");
            //    throw;
            //}
        }
    }
}
