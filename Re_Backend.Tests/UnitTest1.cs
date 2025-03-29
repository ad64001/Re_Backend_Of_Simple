using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Re_Backend.Common;
using Re_Backend.Common.AutoConfiguration;
using Re_Backend.Domain;
using System.Reflection;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Re_Backend.Tests
{
    public class UnitTest1
    {
        private readonly ITestService testService;
        private readonly ITestOutputHelper testOutput;

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
            AutofacConfig.ConfigureContainer(builder, "Re_Backend.Domain");

            // ��������
            var container = builder.Build();

            // �������н����� ITestService ʵ��
            testService = container.Resolve<ITestService>();

            
            this.testOutput = testOutput;
        }

        [Fact]
        public void Test1()
        {


            // ���� DoSomething ����
            testOutput.WriteLine(testService.DoSomething());

            // ���������Ӹ�����Ķ��ԣ�������֤�Ƿ��������Ϣ��
            Assert.NotNull(testService);
        }

       

    }
}