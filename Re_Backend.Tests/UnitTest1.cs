using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Re_Backend.Common;
using Re_Backend.Common.AutoConfiguration;
using Re_Backend.Common.SqlConfig;
using Re_Backend.Domain;
using System.Reflection;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Re_Backend.Tests
{
    public class UnitTest1
    {
        private readonly ITestService testService;
        private readonly ITestDbService testDbService;
        private readonly ITestUserService userService;
        private readonly ITestOutputHelper testOutput;
        private readonly ITestRedisCacheService testRedisCache;
        public UnitTest1(ITestOutputHelper testOutput)
        {
            var builder = new ContainerBuilder();

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

            // 使用 AutofacConfig 类的 ConfigureContainer 方法来注册服务
            AutofacConfig.ConfigureContainer(builder,configuration, "Re_Backend.Domain");

            // 构建容器
            var container = builder.Build();

            // 从容器中解析出 ITestService 实例
            testService = container.Resolve<ITestService>();
            testDbService = container.Resolve<ITestDbService>();
            userService = container.Resolve<ITestUserService>();
            testRedisCache = container.Resolve<ITestRedisCacheService>();
            
            this.testOutput = testOutput;
        }

        [Fact]
        public void Test1()
        {
            // 调用 DoSomething 方法
            testOutput.WriteLine(testService.DoSomething());
            testOutput.WriteLine(testDbService.DoSomething());

            // 这里可以添加更具体的断言，例如验证是否有输出信息等
            Assert.NotNull(testService);
        }

        [Fact]
        public void TestAdd()
        {
            var user = new TestUser
            {
                Name = "Test User",
                Age = 25
            };

            // Act
            userService.AddUser(user);

            // Assert
            var allUsers = userService.GetAllUsers();
            Assert.Contains(allUsers, u => u.Name == user.Name && u.Age == user.Age);
            foreach (var item in allUsers)
            {
                testOutput.WriteLine(item.Name);
            }
        }

        [Fact]
        public void TestAll()
        {
            var allUsers = userService.GetAllUsers();
            foreach (var item in allUsers)
            {
                testOutput.WriteLine(item.Name);
            }
        }

        [Fact]
        public void TestTran()
        {
            // 执行带有事务的方法
            userService.DoSomethingWithTransaction();

            // 验证数据库操作是否成功
            var result = userService.GetDbContext().Db.Queryable<dynamic>();
            Assert.NotNull(result);
        }

        [Fact]
        public async Task TestRedisCache()
        {
            // Act
            var result = await testRedisCache.UseCacheAsync();

            // Assert
            Assert.NotNull(result);
            testOutput.WriteLine(result);
        }
    }
}