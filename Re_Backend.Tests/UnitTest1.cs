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

            // ���÷��񼯺�
            var services = new ServiceCollection();

            // ģ������
            var configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .Build();

            // ע�� JsonSettings
            services.AddSingleton(new JsonSettings(configuration));

            // ���������ṩ��
            var serviceProvider = services.BuildServiceProvider();

            // ��ȡ JsonSettings ʵ��
            var jsonSettings = serviceProvider.GetRequiredService<JsonSettings>();

            // ʹ�� AutofacConfig ��� ConfigureContainer ������ע�����
            AutofacConfig.ConfigureContainer(builder,configuration, "Re_Backend.Domain");

            // ��������
            var container = builder.Build();

            // �������н����� ITestService ʵ��
            testService = container.Resolve<ITestService>();
            testDbService = container.Resolve<ITestDbService>();
            userService = container.Resolve<ITestUserService>();
            testRedisCache = container.Resolve<ITestRedisCacheService>();
            
            this.testOutput = testOutput;
        }

        [Fact]
        public void Test1()
        {
            // ���� DoSomething ����
            testOutput.WriteLine(testService.DoSomething());
            testOutput.WriteLine(testDbService.DoSomething());

            // ���������Ӹ�����Ķ��ԣ�������֤�Ƿ��������Ϣ��
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
            // ִ�д�������ķ���
            userService.DoSomethingWithTransaction();

            // ��֤���ݿ�����Ƿ�ɹ�
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