using Autofac;
using Re_Backend.Common.Attributes;
using Re_Backend.Common.SqlConfig;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Re_Backend.Common.AutoConfiguration
{
    public static class AutofacConfig
    {
        public static void ConfigureContainer(ContainerBuilder containerBuilder, params string[] assemblyNames)
        {
            

            var validAssemblies = new List<Assembly>();
            foreach (var assemblyName in assemblyNames)
            {
                try
                {
                    var assembly = Assembly.Load(assemblyName);
                    validAssemblies.Add(assembly);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"加载程序集 {assemblyName} 时出错: {ex.Message}");
                }
            }
            var types = validAssemblies.SelectMany(a => a.GetTypes())
                                       .Where(t => t.GetCustomAttributes(typeof(InjectableAttribute), true).Length > 0);
            foreach (var type in types)
            {
                var registration = containerBuilder.RegisterType(type).AsImplementedInterfaces();
                // 假设 InjectableAttribute 有一个 IsSingleton 属性来控制是否为单例
                var attribute = type.GetCustomAttribute<InjectableAttribute>();
                if (attribute != null && attribute.IsSingleton)
                {
                    registration.SingleInstance();
                }
                else
                {
                    registration.InstancePerDependency();
                }
            }

            // 注册 SqlSugarClient
            try
            {
                var configs = SqlSugarSetup.GetConnectionConfigs();
                Console.WriteLine($"数据库连接字符串: {configs[0].ConnectionString}");
                Console.WriteLine($"数据库类型: {configs[0].DbType}");
                containerBuilder.Register(c => new SqlSugarClient(configs))
                                .SingleInstance();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"注册 SqlSugarClient 时出错: {ex.Message}");
            }

            // 注册 DbContext
            containerBuilder.RegisterType<DbContext>()
                            .AsSelf()
                            .InstancePerLifetimeScope();

        }



    }
}
