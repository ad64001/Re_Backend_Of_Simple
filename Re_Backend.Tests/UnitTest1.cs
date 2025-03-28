using Autofac;
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